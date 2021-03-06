﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    
    [SerializeField]
    public Board board;

    public List<GameObject> possibleTargets;

    public bool selected = false;

    public Tile currentTile;

    public bool alive = true;
    public bool inPlay = true;

    public int pieceID;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Kill()
    {
        board.AddToDead(gameObject);
        GetComponent<BoardCoordinates>().xPosition = 666;
        GetComponent<BoardCoordinates>().yPosition = 666;

        alive = false;
        inPlay = false;
    }

    public virtual void Move(bool placeholder)
    {
        ClearTargets();

        //if (board.flipped == false)
        //{
        //    GetComponent<BoardCoordinates>().xPosition = (int)(transform.position.x + 0.1);
        //    GetComponent<BoardCoordinates>().yPosition = (int)(transform.position.y + 0.1);
        //}
        //else
        //{
        //    GetComponent<BoardCoordinates>().xPosition = (int)(transform.position.x + 3.6f);
        //    GetComponent<BoardCoordinates>().yPosition = (int)(transform.position.y + 3.6f);
        //}

        GameObject NewTile = board.GetNearestTile(transform.position);

        GetComponent<BoardCoordinates>().xPosition = NewTile.GetComponent<Tile>().x;
        GetComponent<BoardCoordinates>().yPosition = NewTile.GetComponent<Tile>().y;

        //NewTile.SetActive(false);

        currentTile.occupier = null;

        currentTile = board.tiles[GetComponent<BoardCoordinates>().xPosition, GetComponent<BoardCoordinates>().yPosition].GetComponent<Tile>();

        if (currentTile.occupier != null)
        {
            currentTile.occupier.GetComponent<Piece>().Kill();
        }

        currentTile.occupier = gameObject;


        //FindObjectOfType<NetworkClient>().SendMove(pieceID, GetComponent<BoardCoordinates>().xPosition, GetComponent<BoardCoordinates>().yPosition);

        board.GetComponent<Board>().EndTurn();
    }


    public virtual void Move()
    {
        ClearTargets();

        //if (board.flipped == false)
        //{
        //    GetComponent<BoardCoordinates>().xPosition = (int)(transform.position.x + 0.1);
        //    GetComponent<BoardCoordinates>().yPosition = (int)(transform.position.y + 0.1);
        //}
        //else
        //{
        //    GetComponent<BoardCoordinates>().xPosition = (int)(transform.position.x + 3.6f);
        //    GetComponent<BoardCoordinates>().yPosition = (int)(transform.position.y + 3.6f);
        //}

        GameObject NewTile = board.GetNearestTile(transform.position);

        GetComponent<BoardCoordinates>().xPosition = NewTile.GetComponent<Tile>().x;
        GetComponent<BoardCoordinates>().yPosition = NewTile.GetComponent<Tile>().y;

        //NewTile.SetActive(false);

        currentTile.occupier = null;

        currentTile = board.tiles[GetComponent<BoardCoordinates>().xPosition, GetComponent<BoardCoordinates>().yPosition].GetComponent<Tile>();

        if(currentTile.occupier != null)
        {
            currentTile.occupier.GetComponent<Piece>().Kill();
        }

        currentTile.occupier = gameObject;


        FindObjectOfType<NetworkClient>().SendMove(pieceID, GetComponent<BoardCoordinates>().xPosition, GetComponent<BoardCoordinates>().yPosition);

        board.GetComponent<Board>().EndMyTurn();
    }

    public void RemoteMove(int x, int y)
    {
        Debug.Log("Piece " + pieceID + " moving remotely from " + transform.position );

        Vector3 newPosition = board.GetComponent<Board>().tiles[x, y].transform.position;
        transform.position = newPosition;

        Debug.Log("to: " + transform.position);

        Move(true);
    }

    public virtual void Target()
    {

    }

    public void ClearTargets()
    {
        foreach (GameObject g in possibleTargets)
        {
            Destroy(g);
        }

        possibleTargets.Clear();
    }
}
