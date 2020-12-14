using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public GameObject[,] tiles;

    public GameObject whiteTile;
    public GameObject blackTile;

    public GameObject fleet;
    public GameObject fleet2;

    public List<GameObject> yourDead;
    public List<GameObject> theirDead;

    public float yourDeadX;
    public float theirDeadX;

    public Piece[] pieces;

    public bool turn = false;

    public bool flipped = false;

    public string BorW;

    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Begin(string blackOrWhite)
    {
        if (started == false)
        {


            started = true;

            BorW = blackOrWhite;

            tiles = new GameObject[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        tiles[i, j] = Instantiate(whiteTile, transform);
                    }
                    else
                    {
                        tiles[i, j] = Instantiate(blackTile, transform);
                    }

                    tiles[i, j].transform.position = new Vector3(i, j, 0);
                    tiles[i, j].GetComponent<Tile>().x = i;
                    tiles[i, j].GetComponent<Tile>().y = j;
                }
            }

            //transform.Translate(-3.5f, -3.5f, 0);

            fleet.GetComponent<Fleet>().Setup();
            fleet2.GetComponent<Fleet2>().Setup();

            pieces = new Piece[32];

            for (int i = 0; i < 32; i++)
            {
                pieces[i] = GetPieceByID(i);
            }

            if (blackOrWhite == "white")
            {
                Flip();
                MyTurn();
            }
            else
            {
                EndMyTurn();
            }
        }
    }

    public void AddToDead(GameObject p)
    {
        bool iff = p.GetComponent<IFF>().friend;

        if(iff)
        {
            yourDead.Add(p);
            p.transform.position = new Vector3(yourDeadX - (yourDead.Count % 2) * 0.2f, 7 - yourDead.Count * 0.5f, yourDead.Count * -0.1f);
        }
        else
        {
            theirDead.Add(p);
            p.transform.position = new Vector3(theirDeadX + (theirDead.Count % 2) * 0.2f, 7 - theirDead.Count * 0.5f, theirDead.Count * -0.1f);
        }
    }

    public void Flip()
    {
        transform.Rotate(new Vector3(0, 0, 180));

        transform.Translate(new Vector3(-7f, -7f, 0));

        foreach (Piece p in pieces)
        {
            Vector2 newPos = new Vector2(p.gameObject.GetComponent<BoardCoordinates>().xPosition, p.gameObject.GetComponent<BoardCoordinates>().yPosition);
            p.gameObject.transform.position = tiles[(int)(newPos.x + 0.1), (int)(newPos.y)].transform.position;

            flipped = true;

        }
    }

    public void MyTurn()
    {
        if (BorW == "white")
        {
            foreach (GameObject g in fleet.GetComponent<Fleet>().pieces)
            {
                g.GetComponent<Piece>().inPlay = true;
            }
            foreach (GameObject g in fleet2.GetComponent<Fleet2>().pieces)
            {
                g.GetComponent<Piece>().inPlay = false;
                g.GetComponent<Piece>().ClearTargets();
            }
        }
        else if (BorW == "black")
        {
            foreach (GameObject g in fleet.GetComponent<Fleet>().pieces)
            {
                g.GetComponent<Piece>().inPlay = false;
                g.GetComponent<Piece>().ClearTargets();
            }
            foreach (GameObject g in fleet2.GetComponent<Fleet2>().pieces)
            {
                g.GetComponent<Piece>().inPlay = true;
            }
        }
    }

    public void EndMyTurn()
    {

        foreach (GameObject g in fleet2.GetComponent<Fleet2>().pieces)
        {
            g.GetComponent<Piece>().inPlay = false;
            g.GetComponent<Piece>().ClearTargets();
        }

        foreach (GameObject g in fleet.GetComponent<Fleet>().pieces)
        {
            g.GetComponent<Piece>().inPlay = false;
            g.GetComponent<Piece>().ClearTargets();
        }

    }

    public void EndTurn()
    {
        turn = !turn;

        if(turn)
        {
            foreach(GameObject g in fleet.GetComponent<Fleet>().pieces)
            {
                g.GetComponent<Piece>().inPlay = true;
            }
            foreach (GameObject g in fleet2.GetComponent<Fleet2>().pieces)
            {
                g.GetComponent<Piece>().inPlay = false;
                g.GetComponent<Piece>().ClearTargets();
            }
        }
        else
        {

            foreach (GameObject g in fleet.GetComponent<Fleet>().pieces)
            {
                g.GetComponent<Piece>().inPlay = false;
                g.GetComponent<Piece>().ClearTargets();
            }
            foreach (GameObject g in fleet2.GetComponent<Fleet2>().pieces)
            {
                g.GetComponent<Piece>().inPlay = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Piece GetPieceByID(int id)
    {
        foreach (GameObject p in fleet.GetComponent<Fleet>().pieces)
        {
            if(p.GetComponent<Piece>().pieceID == id)
            {
                return p.GetComponent<Piece>();
            }
        }

        foreach (GameObject p in fleet2.GetComponent<Fleet2>().pieces)
        {
            if (p.GetComponent<Piece>().pieceID == id)
            {
                return p.GetComponent<Piece>();
            }
        }

        return null;
    }

    public void MovePiece(int pieceID, int x, int y)
    {
        pieces[pieceID].RemoteMove(x, y);

        MyTurn();
    }

    public GameObject GetNearestTile(Vector3 p)
    {
        float bestDistance = 1000000000000.0f;
        GameObject nearest = null;

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                float distance = Vector3.Distance(p, tiles[i, j].transform.position);
                if(distance < bestDistance)
                {
                    nearest = tiles[i, j];
                    bestDistance = distance;
                }

            }
        }

        return nearest;
    }
}
