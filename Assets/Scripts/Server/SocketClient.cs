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
        Connect(host, port, instance);
        Write("GetKey");
        byte[] bytes = new byte[1024];
        int i = s.Receive(bytes);
        Debug.Log(Encoding.ASCII.GetString(bytes));
        Disconnect();
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
