using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class SocketClient : MonoBehaviour
{
    internal Boolean socketReady = false;
    TcpClient mySocket;
    NetworkStream stream;
    StreamWriter writer;
    StreamReader reader;
    string host = "Airipie.local";
    Int32 port = 60000;


    // Start is called before the first frame update
    void Start()
    {
        SetupSocket();

        Debug.Log(ReadSocket());
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
        if (!socketReady)
            return "";
        try
        {
            return reader.ReadLine();
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
