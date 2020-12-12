using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net.Sockets;
using System.Net;

public class NetworkClient : MonoBehaviour
{

    [SerializeField]

    public UdpClient udp;

    bool populated = false;

    public string clientID;

    Board board;

    // Start is called before the first frame update
    void Start()
    {

        board = FindObjectOfType<Board>();

        udp = new UdpClient();
        // 34.229.252.30
        udp.Connect("3.15.207.80", 12345);

        Byte[] sendBytes = Encoding.ASCII.GetBytes("connect");

        udp.Send(sendBytes, sendBytes.Length);

        udp.BeginReceive(new AsyncCallback(OnReceived), udp);

        InvokeRepeating("HeartBeat", 1, 1);

        Byte[] sendBytes2 = Encoding.ASCII.GetBytes("spawn");
        udp.Send(sendBytes2, sendBytes2.Length);

        //cube1 = Instantiate(cube, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

    }

    void OnDestroy()
    {
        udp.Dispose();
    }


    public enum commands
    {
        SIGN_UP,
        LOG_IN,
        PLAY_GAME,
        CHESS_MOVE,
        DISCONNECT

    };

    [Serializable]
    public class Message
    {
        public commands cmd;

        public int pieceID;
        public int x;
        public int y;

    }

    public class idMessage
    {
        public string id;
    }

    //[Serializable]
    //public class Player
    //{
    //    [Serializable]
    //    public struct receivedColor
    //    {
    //        public float R;
    //        public float G;
    //        public float B;
    //    }

    //    [Serializable]
    //    public struct receivedPosition
    //    {
    //        public float x;
    //        public float y;
    //        public float z;
    //    }

    //    public string id;
    //    public float xCoord;
    //    public receivedColor color;
    //    public receivedPosition position;
    //    public GameObject gameCube = null;
    //    public int spawned;

    //    public int disconnected;

    //}

    [Serializable]
    public class ChessMove
    {
        public int pieceID;
        public int x;
        public int y;
    }

    
    [Serializable]
    public class GameState
    {
        //public Player[] players;
    }

    public Message latestMessage;
    public GameState latestGameState;
    public idMessage idm;
    public ChessMove cm;
    public ChessMove sendMove;

    void OnReceived(IAsyncResult result)
    {
        // this is what had been passed into BeginReceive as the second parameter:
        UdpClient socket = result.AsyncState as UdpClient;

        // points towards whoever had sent the message:
        IPEndPoint source = new IPEndPoint(0, 0);

        // get the actual message and fill out the source:
        byte[] message = socket.EndReceive(result, ref source);

        // do what you'd like with `message` here:
        string returnData = Encoding.ASCII.GetString(message);
        //Debug.Log("Got this: " + returnData);


        latestMessage = JsonUtility.FromJson<Message>(returnData);



        //float R  = message["color"]["R"]

        try
        {
            switch (latestMessage.cmd)
            {
                case commands.SIGN_UP:

                    
                    //np = JsonUtility.FromJson<Player>(returnData);

                    //playersInGame.Add(np);

                    break;
                case commands.LOG_IN:
                    latestGameState = JsonUtility.FromJson<GameState>(returnData);
                    break;
                case commands.PLAY_GAME:

                    break;
                case commands.CHESS_MOVE:
                    Debug.Log("PROCESSING ENEMY MOVE");

                    cm = JsonUtility.FromJson<ChessMove>(returnData);

                    board.MovePiece(cm.pieceID, cm.x, cm.y);

                    Debug.Log(clientID);


                    break;
                case commands.DISCONNECT:
                    Debug.Log("Client disconnected");

                    idm = JsonUtility.FromJson<idMessage>(returnData);              

                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        // schedule the next receive operation once reading is done:
        socket.BeginReceive(new AsyncCallback(OnReceived), socket);
    }


    void HeartBeat()
    {
        Byte[] sendBytes = Encoding.ASCII.GetBytes("heartbeat");
        udp.Send(sendBytes, sendBytes.Length);
    }

    public void SendMove(int _pieceID, int _x, int _y)
    {
        sendMove = new ChessMove();
        sendMove.pieceID = _pieceID;
        sendMove.x = _x;
        sendMove.y = _y;


        string myMove = JsonUtility.ToJson(sendMove);

        Byte[] sendBytes = Encoding.ASCII.GetBytes(myMove);
        udp.Send(sendBytes, sendBytes.Length);
    }

    [Serializable]
    public struct move
    {
        public string moveName;
        public float x;
        public float y;
        public float z;
    }



    void Update()
    {
        //MovementInput();
        //SpawnPlayers();
        //UpdatePlayers();
        //DestroyPlayers();

    }

}
