using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using UnityEngine;

public class SocketClient : MonoBehaviour
{

    private GameManager instance = GameManager.instance;

    [SerializeField] private GameObject invalidAccount;
    [SerializeField] private GameObject existAccount;
    

    string host = "psg551.com";
    int port = 60000;
    Socket s;

    string username;
    string playerName;

    RSACryptoServiceProvider csp;
    string publicKeystr;


    private void Start()
    {
        if (!instance) instance = GameManager.instance;
        invalidAccount.SetActive(false);
        existAccount.SetActive(false);

        UpdatePublicKey();
    }

    private void UpdatePublicKey()
    {
        // If public key doesn't exist, get it
        // If it does exist, verify it
        string path_to_key = @"keys\public_key.pem";
        Connect(host, port, instance);
        s.Send(Encoding.ASCII.GetBytes("GetKey"));

        byte[] sbytes = new byte[1024];
        s.Receive(sbytes);
        string serverKey = Encoding.ASCII.GetString(sbytes);

        //string serverKey = Read();
        Debug.Log(serverKey);

        if (File.Exists(path_to_key))
        {
            StreamReader sReader = new StreamReader(path_to_key);
            publicKeystr = sReader.ReadToEnd();
            Debug.Log(publicKeystr == serverKey);
            if (publicKeystr != serverKey)
            {
                File.WriteAllText(path_to_key, serverKey);
                publicKeystr = serverKey;
            }
            sReader.Close();
        }
        else
        {
            File.WriteAllText(path_to_key, serverKey);
            publicKeystr = serverKey;
        }

        TextReader textReader = new StringReader(publicKeystr);
        PemReader pemReader = new PemReader(textReader, new PasswordFinder("platformgame"));
        RsaKeyParameters pemObject = (RsaKeyParameters)pemReader.ReadObject();
        RSAParameters rSAParameters = DotNetUtilities.ToRSAParameters(pemObject);

        csp = new RSACryptoServiceProvider();
        csp.ImportParameters(rSAParameters);

        string plaintext = "Public key updated";
        byte[] cipher = csp.Encrypt(Encoding.ASCII.GetBytes(plaintext), true);
        s.Send(cipher);

        //byte[] bytes = new byte[1024];
        //int i = s.Receive(bytes);
        //Debug.Log(CheckSignature(sbytes, bytes, pemObject));

        textReader.Close();
        Disconnect();
    }

    private byte[] EncryptMessage(string msg)
    {
        return csp.Encrypt(Encoding.ASCII.GetBytes(msg), RSAEncryptionPadding.OaepSHA1);
    }

    // Socket Operations
    private void Connect(string host, int port, GameManager instance)
    {
        IPAddress[] IPs = Dns.GetHostAddresses(host);

        s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        s.Connect(IPs, port);
    }

    private void Disconnect()
    {
        if (s.Connected) s.Close();
    }

    private void Write(string msg)
    {
        //byte[] _msg = Encoding.ASCII.GetBytes(msg);
        byte[] encrypted = EncryptMessage(msg);

        try
        {
            int i = s.Send(encrypted);
            Debug.Log("Sent " + i + " bytes.");
        }
        catch (SocketException e)
        {
            Debug.Log("Error (Send/Receive): " + e.Message);
        }
    }

    private string Read()
    {
        byte[] bytes = new byte[1024];
        try
        {
            int i = s.Receive(bytes);
            return Encoding.ASCII.GetString(bytes);
        }
        catch (SocketException e)
        {
            Debug.Log("Error (Send/Receive): " + e.Message);
            return "";
        }
    }

    // TODO VERIFY SIG
    //private bool CheckSignature(byte[] msg, byte[] sig, RsaKeyParameters key)
    //{
    //    PssSigner pss = new PssSigner(new RsaEngine(), new Sha1Digest(), 20);
    //    pss.Init(false, key);
    //    pss.BlockUpdate(msg, 0, msg.Length);
    //    Debug.Log(pss.VerifySignature(sig));
    //    return csp.VerifyData(msg, CryptoConfig.MapNameToOID("SHA1"), sig);
    //}


    // Login/Create Account
    private void GetData()
    {
        string _msg = "Open" + username;

        // Standard socket operations
        Connect(host, port, instance);
        Write(_msg);

        // Player Info
        string reply = Read();
        Debug.Log("Player info: " + reply);


        if (!reply.StartsWith("not_found"))
        {
            instance.Player = Player.CreateFromJSON(reply);
            instance.LoadScene("MainMenu");
        }
        else
        {
            invalidAccount.SetActive(true);
        }


        Disconnect();
    }

    private void CreateNew()
    {
        string _msg = "Create";
        instance.player = new Player(username, playerName);
        _msg += JsonUtility.ToJson(instance.player);

        Connect(host, port, instance);
        Write(_msg);
        string reply = Read();

        existAccount.SetActive(reply.StartsWith("already_exists"));

        Disconnect();
    }


    // Setting variables from input fields
    public void SetUsername(string user)
    {
        username = user;
    }

    public void SetName(string name)
    {
        playerName = name;
    }


}

class PasswordFinder : IPasswordFinder
{
    private string password;

    public PasswordFinder(string password)
    {
        this.password = password;
    }

    public char[] GetPassword()
    {
        return password.ToCharArray();
    }
}