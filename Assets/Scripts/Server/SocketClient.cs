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
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject loginCanvas;
    

    string host = "psg551.com";
    int port = 60000;
    Socket s;

    string username;
    string playerName;
    string password;

    RSACryptoServiceProvider csp;
    string publicKeystr;

    private bool isOffline = false;
    public bool IsOffline { get => isOffline; }


    private void Start()
    {
        if (!instance) instance = GameManager.instance;
        invalidAccount.SetActive(false);
        existAccount.SetActive(false);

        try
        {
            UpdatePublicKey();
        }
        catch(SocketException e)
        {
            isOffline = true;
            Debug.Log(e.Message);
        }
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

        //if (File.Exists(path_to_key))
        //{
        //    StreamReader sReader = new StreamReader(path_to_key);
        //    publicKeystr = sReader.ReadToEnd();
        //    Debug.Log(publicKeystr == serverKey);
        //    if (publicKeystr != serverKey)
        //    {
        //        File.WriteAllText(path_to_key, serverKey);
        //        publicKeystr = serverKey;
        //    }
        //    sReader.Close();
        //}
        //else
        //{
        //    File.WriteAllText(path_to_key, serverKey);
        //    publicKeystr = serverKey;
        //}

        PlayerPrefs.SetString("key", serverKey);
        PlayerPrefs.Save();

        publicKeystr = serverKey;

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



    // Server Methods
    public void GetData()
    {
        string _msg = "Open";
        instance.player = new Player(username, null, password);
        _msg += JsonUtility.ToJson(instance.player);

        // Standard socket operations
        Connect(host, port, instance);
        Write(_msg);

        // Player Info
        string reply = Read();


        if (reply.StartsWith("not_found"))
        {
            invalidAccount.SetActive(true);
        }
        else if (reply.StartsWith("wrong_password"))
        {
            invalidAccount.SetActive(true);
        }
        else
        {
            instance.Player = Player.CreateFromJSON(reply);
            LoadMainMenu(1);
        }


        Disconnect();
    }

    public void CreateNew()
    {
        string _msg = "Create";
        instance.player = new Player(username, playerName, password);
        instance.player.hp = 100f;
        _msg += JsonUtility.ToJson(instance.player);
        Debug.Log(_msg);
        Connect(host, port, instance);
        Write(_msg);
        string reply = Read();

        existAccount.SetActive(reply.StartsWith("already_exists"));

        if (reply.StartsWith("player_created"))
        {
            GetData();
        }

        Disconnect();
    }

    public void SaveGame()
    {
        if (!IsOffline)
        {
            string _msg = "Save";
            _msg += JsonUtility.ToJson(instance.player);
            Connect(host, port, instance);
            Write(_msg);
            string reply = Read();
            Debug.Log(reply);
            Disconnect();
        }
        else
        {
            PlayerPrefs.SetInt("Level", instance.player.level);
            PlayerPrefs.SetFloat("Hp", instance.player.hp);
            PlayerPrefs.SetFloat("PositionX", instance.player.position.x);
            PlayerPrefs.SetFloat("PositionY", instance.player.position.y);
            PlayerPrefs.SetFloat("PositionZ", instance.player.position.z);

            // Delete all enemies in playerprefs
            for(int i = 0; PlayerPrefs.HasKey("Enemy" + i); i++)
            {
                PlayerPrefs.DeleteKey("Enemy" + i);
            }

            // Save all current enemies
            for(int i = 0; i < instance.player.enemies.Count; i++)
            {
                PlayerPrefs.SetString("Enemy" + i, instance.player.enemies[i]);
            }

            PlayerPrefs.Save();
        }
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

    public void SetPassword(string password)
    {
        this.password = password;
    }


    public void PlayOffline()
    {
        instance.player = new Player("", "", "");

        if (PlayerPrefs.HasKey("Level"))
        {
            instance.player.level = PlayerPrefs.GetInt("Level");
            instance.player.hp = PlayerPrefs.GetFloat("Hp");
            instance.player.position = new Vector3(
                PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"), PlayerPrefs.GetFloat("PositionZ"));

            for (int i = 0; PlayerPrefs.HasKey("Enemy" + i); i++)
            {
                instance.player.enemies.Add(PlayerPrefs.GetString("Enemy" + i));
            }
        }
        else
        {
            instance.player.level = 1;
            instance.player.hp = 100f;

        }

        LoadMainMenu(0);
    }

    private void LoadMainMenu(int status)
    {
        mainMenu.SetActive(true);
        loginCanvas.SetActive(false);
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