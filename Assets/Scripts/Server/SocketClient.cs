using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SocketClient : MonoBehaviour
{
    internal Boolean socketReady = false;
    TcpClient mySocket;
    NetworkStream stream;
    StreamWriter writer;
    StreamReader reader;
    string host = "psg551.com";
    Int32 port = 60000;

    string username;
    string playerData;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetUsername(string user)
    {
        username = user;
    }

    public void GetData()
    {
        SetupSocket();
        WriteSocket(username);
        playerData = ReadSocket();
        player = Player.CreateFromJSON(playerData);
        Debug.Log(player.name);
        CloseSocket();
    }

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
