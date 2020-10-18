﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet2 : MonoBehaviour
{
    public GameObject pawn;
    public GameObject[] pawns;
    public GameObject BishopL;
    public GameObject BishopR;
    public GameObject RookL;
    public GameObject RookR;
    public GameObject KnightL;
    public GameObject KnightR;
    public GameObject King;
    public GameObject Queen;

    public GameObject sceneBoard;

    public Board board;


    public List<GameObject> pieces;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Setup()
    {
        //board = sceneBoard.GetComponent<Board>();

        Debug.Log("Rook" + RookL);
        Debug.Log("board" + board);
        Debug.Log(board.tiles[0, 0]);

        RookL = Instantiate(RookL, (board.tiles[0, 7].transform.position + new Vector3(0,0,-1)), new Quaternion(0, 0, 0, 0));
        RookR = Instantiate(RookR, (board.tiles[7, 7].transform.position + new Vector3(0, 0, -1)), new Quaternion(0, 0, 0, 0));

        KnightL = Instantiate(KnightL, (board.tiles[1, 7].transform.position + new Vector3(0, 0, -1)), new Quaternion(0, 0, 0, 0));
        KnightR = Instantiate(KnightR, (board.tiles[6, 7].transform.position + new Vector3(0, 0, -1)), new Quaternion(0, 0, 0, 0));

        BishopL = Instantiate(BishopL, (board.tiles[2, 7].transform.position + new Vector3(0, 0, -1)), new Quaternion(0, 0, 0, 0));
        BishopR = Instantiate(BishopR, (board.tiles[5, 7].transform.position + new Vector3(0, 0, -1)), new Quaternion(0, 0, 0, 0));

        King = Instantiate(King, (board.tiles[4, 7].transform.position + new Vector3(0, 0, -1)), new Quaternion(0, 0, 0, 0));
        Queen = Instantiate(Queen, (board.tiles[3, 7].transform.position + new Vector3(0, 0, -1)), new Quaternion(0, 0, 0, 0));

        if(RookL.GetComponent<Piece>() == null)
        {
            Debug.Log("ROOKL IS NULL");
        }

        RookL.GetComponent<BoardCoordinates>().xPosition = 0;
        RookL.GetComponent<BoardCoordinates>().yPosition = 7;

        RookL.GetComponent<Piece>().currentTile = board.tiles[0, 7].GetComponent<Tile>();
        RookL.GetComponent<Piece>().currentTile.occupier = RookL;

        RookR.GetComponent<BoardCoordinates>().xPosition = 7;
        RookR.GetComponent<BoardCoordinates>().yPosition = 7;

        RookR.GetComponent<Piece>().currentTile = board.tiles[7, 7].GetComponent<Tile>();
        RookR.GetComponent<Piece>().currentTile.occupier = RookR;

        KnightL.GetComponent<BoardCoordinates>().xPosition = 1;
        KnightL.GetComponent<BoardCoordinates>().yPosition = 7;

        if(KnightL.GetComponent<Piece>() == null)
        {
            Debug.Log("Board is NULL");
        }

        KnightL.GetComponent<Piece>().currentTile = board.tiles[1, 7].GetComponent<Tile>();
        KnightL.GetComponent<Piece>().currentTile.occupier = KnightL;

        KnightR.GetComponent<BoardCoordinates>().xPosition = 6;
        KnightR.GetComponent<BoardCoordinates>().yPosition = 7;

        KnightR.GetComponent<Piece>().currentTile = board.tiles[6, 7].GetComponent<Tile>();
        KnightR.GetComponent<Piece>().currentTile.occupier = KnightR;

        BishopL.GetComponent<BoardCoordinates>().xPosition = 2;
        BishopL.GetComponent<BoardCoordinates>().yPosition = 7;

        BishopL.GetComponent<Piece>().currentTile = board.tiles[2, 7].GetComponent<Tile>();
        BishopL.GetComponent<Piece>().currentTile.occupier = BishopL;

        BishopR.GetComponent<BoardCoordinates>().xPosition = 5;
        BishopR.GetComponent<BoardCoordinates>().yPosition = 7;

        BishopR.GetComponent<Piece>().currentTile = board.tiles[5, 7].GetComponent<Tile>();
        BishopR.GetComponent<Piece>().currentTile.occupier = BishopR;
              
        King.GetComponent<BoardCoordinates>().xPosition = 4;
        King.GetComponent<BoardCoordinates>().yPosition = 7;

        King.GetComponent<Piece>().currentTile = board.tiles[4, 7].GetComponent<Tile>();
        King.GetComponent<Piece>().currentTile.occupier = King;

        Queen.GetComponent<BoardCoordinates>().xPosition = 3;
        Queen.GetComponent<BoardCoordinates>().yPosition = 7;

        Queen.GetComponent<Piece>().currentTile = board.tiles[3, 7].GetComponent<Tile>();
        Queen.GetComponent<Piece>().currentTile.occupier = Queen;

        pieces.Add(King);
        pieces.Add(Queen);
        pieces.Add(RookL);
        pieces.Add(RookR);
        pieces.Add(BishopL);
        pieces.Add(BishopR);
        pieces.Add(KnightL);
        pieces.Add(KnightR);


        pawns = new GameObject[8];

        for (int p = 0; p < 8; p++)
        {
            pawns[p] = Instantiate(pawn, (board.tiles[p, 6].transform.position), new Quaternion(0, 0, 0, 0));

            pawns[p].GetComponent<BoardCoordinates>().xPosition = p;
            pawns[p].GetComponent<BoardCoordinates>().yPosition = 6;

            pawns[p].GetComponent<Piece>().currentTile = board.tiles[p, 6].GetComponent<Tile>();
            pawns[p].GetComponent<Piece>().currentTile.occupier = pawns[p];


            pieces.Add(pawns[p]);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
