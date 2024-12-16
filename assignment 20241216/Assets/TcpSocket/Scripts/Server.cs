using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Server : MonoBehaviour
{
    public Button connect;
    public RectTransform textArea;
    public TextMeshProUGUI textprefab;
    private string ipAddress = "127.0.0.1"; //localhost : 내 단말의 ip
    public int port = 9999;
    //0~65535 까지의 숫자중 1개 사용 80번 이전의 port는 이미 대부분 선점이 되어있다.


    private bool isConnected = false;
    private Thread serverMainThread;
    private int clientId = 0;

    private List<ClientHandler> clients = new List<ClientHandler>();

    public static Queue<string> log = new Queue<string>();

    private void Awake()
    {
        connect.onClick.AddListener(ConnecButtonClicK);
    }

    private void Update()
    {
        if (log.Count > 0)
        {
            TextMeshProUGUI logtext = Instantiate(textprefab, textArea);
            logtext.text = log.Dequeue();
        }
    }

    private void ConnecButtonClicK()
    {
        if (false == isConnected)
        {
            serverMainThread = new Thread(ServerThread);
            serverMainThread.IsBackground = true;
            serverMainThread.Start();
            isConnected = true;
        }
        else
        {
            serverMainThread.Abort();
            isConnected = false;
        }
    }

    private void ServerThread()
    {
        try
        {
            TcpListener tcpListner = new TcpListener(IPAddress.Parse(ipAddress), port);
            tcpListner.Start();
            log.Enqueue("서버 시작됨");
            while (true)
            {
                TcpClient tcpclient = tcpListner.AcceptTcpClient();
                ClientHandler handler = new ClientHandler();
                handler.Connect(clientId++, this, tcpclient);
                clients.Add(handler);
                log.Enqueue($"{clientId}가 접속함");
            }
        }
        catch
        {
            log.Enqueue("뭔가... 일어나고 있어");
        }
        finally
        {
            foreach (ClientHandler client in clients)
            {
                client.Disconnected();
            }
            serverMainThread.Abort();
            isConnected = false;
        }
    }

    public void DisConeected(ClientHandler client)
    {
        clients.Remove(client);
    }

    public void BroadcastTOclients(string message)
    {

        log.Enqueue(message);

        foreach (ClientHandler client in clients)
        {
            //client.MessageToclient(message);
            try
            {
                client.MessageToclient(message); // 클라이언트에게 메시지 전송
            }
            catch
            {
                log.Enqueue($"클라이언트 {client.id}에게 메시지 전송 실패");
            }
        }
    }

}

public class ClientHandler
{
    public int id;
    public Server server;
    public TcpClient tcpClient;
    public Thread clientThread;
    public StreamReader reader;
    public StreamWriter writer;

    public void Connect(int id, Server server, TcpClient tcpclient)
    {
        this.id = id;
        this.server = server;
        this.tcpClient = tcpclient;
        reader = new StreamReader(tcpclient.GetStream());
        writer = new StreamWriter(tcpclient.GetStream());
        writer.AutoFlush = true;
        clientThread = new Thread(Run);
        clientThread.IsBackground = true;
        clientThread.Start();
    }

    public void Disconnected()
    {
        clientThread.Abort();
        writer.Close();
        reader.Close();
        tcpClient.Close();
    }

    public void MessageToclient(string message)
    {
        writer.WriteLine(message);
    }
    public void Run()
    {
        try
        {
            while (tcpClient.Connected)
            {
                string receiveMessage = reader.ReadLine();
                if (string.IsNullOrEmpty(receiveMessage))
                {
                    continue;
                }

                if (receiveMessage.StartsWith("SCREEN_POINT:"))
                {
                    string[] data = receiveMessage.Substring("SCREEN_POINT:".Length).Split(',');
                    float x = float.Parse(data[0]);
                    float y = float.Parse(data[1]);

                    ScreenPointMessage screenPointMessage = new ScreenPointMessage(id, x, y);

                    string json_Message = JsonUtility.ToJson(screenPointMessage);

                    server.BroadcastTOclients(json_Message);

                    server.BroadcastTOclients($"클라이언트 {id} 클릭 위치: X={x}, Y={y}");
                }
                else
                {
                    //유요한 메시를 받음
                    server.BroadcastTOclients($"{id}님의 말 : {receiveMessage}");
                }
            }
        }
        finally
        {
            Server.log.Enqueue($"{id}번 클라이언트 연결 종료됨");
            Disconnected();
        }
    }
}
