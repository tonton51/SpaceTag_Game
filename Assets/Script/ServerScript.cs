using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class ServerScript : MonoBehaviour
{
    // 取得したい値のオブジェクトを設定
    public GameObject Player1;
    public GameObject Player2;

    public GameObject GameDirector;
    
    // 値の初期化
    private float kumax;
    private float kumay;
    private float rocketx;
    private float rockety;

    private float gametime;

    // 配列の個数を設定
    static int numVars = 5;
    private float[] txFloatArray = new float[numVars];
    private byte[] txByteArray = new byte[numVars * 4];

    TcpListener server = null;
    TcpClient client = null;
    NetworkStream stream = null;
    Thread thread;

    // データを送る速度などサーバー設定
    private void Start()
    {
        thread = new Thread(new ThreadStart(SetupServer));
        thread.Start();

        InvokeRepeating("SendData", 0.0f, 0.03f);

    }

    // ゲーム内の値を取得
    private void Update()
    {
        gametime=GameDirector.GetComponent<GameDirector>().delta;
        kumax = Input.GetAxis("HorizontalL");
        kumay = Input.GetAxis("VerticalL");
        rocketx = Input.GetAxis("HorizontalR");
        rockety = Input.GetAxis("VerticalR");

    }

    private void SendData()
    {
        // サーバーに送る値を格納
        txFloatArray[0] = gametime;
        txFloatArray[1] = kumax;
        txFloatArray[2] = kumay;
        txFloatArray[3] = rocketx;
        txFloatArray[4] = rockety;

        if (stream != null)
        {
            Buffer.BlockCopy(txFloatArray, 0, txByteArray, 0, txByteArray.Length);
            SendDataToClient(txByteArray);

            // SendMessageToClient($"speed = {speed}, distance = {distance}");
            // Debug.Log($"speed = {speed}, distance = {distance}");
        }
    }


    private void SetupServer()
    {
        try
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(localAddr, 65432);
            server.Start();

            byte[] buffer = new byte[1024];
            string data = null;

            while (true)
            {
                Debug.Log("Waiting for connection...");
                client = server.AcceptTcpClient();
                Debug.Log("Connected!");


                data = null;
                stream = client.GetStream();

                int i;
                while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    data = Encoding.UTF8.GetString(buffer, 0, i);
                    Debug.Log("Received: " + data);

                    string[] cmd = data.Split(",");


                    // string response = "Server response: " + data.ToString();
                    // SendMessageToClient(message: response);
                }
                client.Close();
            }
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
        }
        finally
        {
            server.Stop();
        }
    }

    private void OnApplicationQuit()
    {
        stream.Close();
        client.Close();
        server.Stop();
        thread.Abort();
    }

    public void SendMessageToClient(string message)
    {
        byte[] msg = Encoding.UTF8.GetBytes(message);


        stream.Write(msg, 0, msg.Length);
        Debug.Log("Sent: " + message);
    }

    public void SendDataToClient(byte[] data)
    {
        byte end = Encoding.UTF8.GetBytes("\n")[0];

        foreach (byte b in data)
        {
            stream.WriteByte(b);

        }
        stream.WriteByte(end);

    }
}