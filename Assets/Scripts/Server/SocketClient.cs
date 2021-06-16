using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class SocketClient : MonoBehaviour
{
    private GameManager instance = GameManager.instance;

    [SerializeField] private GameObject invalidAccount;
    [SerializeField] private GameObject existAccount;

    internal Boolean socketReady = false;
    TcpClient mySocket;
    NetworkStream stream;
    StreamWriter writer;
    StreamReader reader;
    //string host = "psg551.com";
    //Int32 port = 60000;

    string username;
    string playerName;
    string playerData;

    public string PlayerData { get => playerData; set => playerData = value; }

    // Start is called before the first frame update
    void Start()
    {
        invalidAccount.SetActive(false);
        existAccount.SetActive(false);
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
    


    // Create new player data and save to server
    public void CreateNew()
    {
        SetupSocket();
        instance.player = new Player(username, playerName);
        playerData = JsonUtility.ToJson(instance.player);
        WriteSocket("Create" + playerData);
        playerData = ReadSocket();
        if (playerData == "already_exists")
        {
            existAccount.SetActive(true);
        }
        CloseSocket();
    }

    // Get existing player data from server
    public void GetData()
    {
        SetupSocket();
        WriteSocket(username);
        playerData = ReadSocket();

        try
        {
            instance.Player = Player.CreateFromJSON(playerData);
            Debug.Log(instance.Player.name);
            instance.LoadScene("MainMenu");
        }
        catch (Exception e)
        {
            Debug.Log("Player does not exist: " + e.ToString());
            invalidAccount.SetActive(true);
        }

        CloseSocket();
    }


    // All Socket Operations
    public void SetupSocket()
    {
        try
        {
            mySocket = new TcpClient("psg551.com", 60000);
            stream = mySocket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
            Debug.Log("Connect Status: " + mySocket.Connected);
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }

    public void WriteSocket(string line)
    {
        if (!socketReady)
            return;
        string foo = line + "\r\n";
        writer.Write(foo);
        writer.Flush();
    }

    public string ReadSocket()
    {
        string recd;
        if (!socketReady)
            return "";
        try
        {
            recd = reader.ReadLine();
            return recd;
        }
        catch (Exception e)
        {
            return e.ToString();
        }
    }

    public void CloseSocket()
    {
        if (!socketReady)
            return;
        writer.Close();
        reader.Close();
        mySocket.Close();
        socketReady = false;
    }

}
