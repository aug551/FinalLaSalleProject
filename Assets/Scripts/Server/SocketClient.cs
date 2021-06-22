using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
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


    private void Start()
    {
        if (!instance) instance = GameManager.instance;
        invalidAccount.SetActive(false);
        existAccount.SetActive(false);

        // Try grabbing the public key as bytes

        StreamReader sReader = new StreamReader("keys/public_key.pem");
        string s_publicKey = sReader.ReadToEnd();
        Debug.Log(s_publicKey);
        PemReader pr = new PemReader(new StringReader(s_publicKey));
        AsymmetricKeyParameter publicKey = (AsymmetricKeyParameter)pr.ReadObject();
        RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaKeyParameters)publicKey);
        

        csp = new RSACryptoServiceProvider();
        csp.ImportParameters(rsaParams);

        Debug.Log(csp.SignatureAlgorithm);
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
        byte[] _msg = Encoding.ASCII.GetBytes(msg);

        try
        {
            int i = s.Send(_msg);
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


    // Login/Create Account
    private void GetData()
    {
        string _msg = "Open" + username;

        // Standard socket operations
        Connect(host, port, instance);
        Write(_msg);

        // Player Info
        //string reply = Read();
        //Debug.Log("Player info: " + reply);
        byte[] player_bytes = new byte[1024];
        int i = s.Receive(player_bytes);


        // Signature
        Write("await_sig");
        byte[] sig_bytes = new byte[1024];
        i = s.Receive(sig_bytes);

        Debug.Log(sig_bytes);
        bool good = csp.VerifyData(player_bytes, "SHA256", sig_bytes);
        Debug.Log(good);

        //if (!reply.StartsWith("not_found"))
        //{
        //    instance.Player = Player.CreateFromJSON(reply);
        //    instance.LoadScene("MainMenu");
        //}
        //else
        //{
        //    invalidAccount.SetActive(true);
        //}




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
