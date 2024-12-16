using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    public class Client : MonoBehaviour
    {
        [Header("IP Input")]
        public TMP_InputField ip;
        public TMP_InputField port;
        public Button connect;

        [Header("Message Input")]
        public TMP_InputField Message;
        public Button send;

        [Header("Text Area")]
        public RectTransform textAtrea;
        public TextMeshProUGUI textprefab;

        private Thread clientThread;//쓰레드
        private StreamReader reader; //스트림리더
        private StreamWriter writer; //스트림라이터

        private bool isConnected;

        public static Queue<string> log = new Queue<string>();
        private void Awake()
        {
            connect.onClick.AddListener(ConnectButtonClick);
            send.onClick.AddListener(() => SendSubmit(Message.text));
            Message.onEndEdit.AddListener(SendSubmit);
        }

        private void Update()
        {
            /*if(log.Count > 0)
            {
                TextMeshProUGUI logText = Instantiate(textprefab, textAtrea);
                logText.text = log.Dequeue();
            }*/

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenPoint = Input.mousePosition;
                SendScreenPointToServer(screenPoint);
            }

            if (log.Count > 0)
            {
                string message = log.Dequeue();
                log.Enqueue($"받은 메시지: {message}");

                // JSON 메시지 처리
                if (message.StartsWith("{") && message.EndsWith("}"))
                {
                    try
                    {
                        // JSON 메시지를 ScreenPointMessage 객체로 역직렬화
                        ScreenPointMessage receivedMessage = JsonUtility.FromJson<ScreenPointMessage>(message);

                        if (receivedMessage.type == "SCREEN_POINT")
                        {
                            Debug.Log($"클라이언트 {receivedMessage.id} 클릭 좌표: X={receivedMessage.x}, Y={receivedMessage.y}");
                            DisplayClickpoint(receivedMessage);
                        }
                    }
                    catch
                    {
                        Debug.LogWarning("JSON 메시지 처리 실패: " + message);
                    }
                }
                else
                {
                    // 일반 텍스트 메시지 처리
                    TextMeshProUGUI logText = Instantiate(textprefab, textAtrea);
                    logText.text = message;
                }
            }
        }

        private void DisplayClickpoint(ScreenPointMessage message)
        {
            TextMeshProUGUI logtext = Instantiate(textprefab,textAtrea);
            logtext.text = $"유저 {message.id} 클릭 위치";

            Vector2 uiposition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                textAtrea,
                new Vector2(message.x, message.y),
                Camera.main,
                out uiposition );
            logtext.rectTransform.anchoredPosition = uiposition;    
        }

        private void SendScreenPointToServer(Vector2 screenPoint)
        {
            if (writer != null)
            {
                string message = $"SCREEN_POINT:{screenPoint.x},{screenPoint.y}";
                writer.WriteLine(message);
                writer.AutoFlush = true;
                log.Enqueue($"Screen point sent to server: {message}");
            }
            else
            {
                log.Enqueue("서버에 연결되지 않았습니다.");
            }
        }

        private void ClientThread()
        {
            TcpClient tcpClient = new TcpClient(); //클라이언트 객체 생성
            IPAddress serverAddress = IPAddress.Parse(ip.text); // ip 입력란의 텍스트를 ip 주소로 파싱
                                                                //0~65535 까지 번호를 씀. ushort으로 쓰면 효율적이겠지만, C#에서 주로 쓰이는 정수 자료형이 int이므로
            int portNum = int.Parse(port.text);

            IPEndPoint endpoint = new IPEndPoint(serverAddress, portNum);

            tcpClient.Connect(endpoint); //서버로 연결 시도.

            //여기까지 코드가 실행되었으면 서버에 접속 성공.
            log.Enqueue("서버접속 성공 ㅋㅋㅋㅋ");

            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());
            writer.AutoFlush = true;

            while (tcpClient.Connected)
            {
                string receiveMessage = reader.ReadLine();
                log.Enqueue($"앗사~~~ 메세지를 받았습니다{receiveMessage}");
            }
        }

        private void ConnectButtonClick()
        {
            if (false == isConnected) //접속중이 아니므로 접속 시도
            {
                clientThread = new Thread(ClientThread);
                clientThread.IsBackground = true;
                clientThread.Start();
                isConnected = true;
            }
            else//접속 끊기
            {
                clientThread.Abort();
                isConnected = false;
            }
        }

        private void SendSubmit(string message)
        {
            writer.WriteLine(message);
            this.Message.text = "";

        }

    }
}
