﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        selected = !selected;

        if (board == null)
        {
            Debug.Log("BOARD IS NULL. WHYYYYYYY!?!?!?!");
        }

        if (selected)
        {
            int xPosition = GetComponent<BoardCoordinates>().xPosition;
            int yPosition = GetComponent<BoardCoordinates>().yPosition;

            if (yPosition != 7)
            {
                GameObject N = board.tiles[xPosition, yPosition + 1].GetComponent<Tile>().occupier;

                if (N == null || N.GetComponent<IFF>().friend == false)
                {
                    if(target == null)
                    {
                        Debug.Log("TARGET IS NULL");
                    }

                    GameObject t = Instantiate(target, board.tiles[xPosition, yPosition + 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (xPosition != 7 && yPosition != 7)
            {
                GameObject NE = board.tiles[xPosition + 1, yPosition + 1].GetComponent<Tile>().occupier;

                if (NE == null || NE.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition + 1, yPosition + 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (xPosition != 7)
            {
                GameObject E = board.tiles[xPosition + 1, yPosition].GetComponent<Tile>().occupier;


                if (E == null || E.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition + 1, yPosition].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (xPosition != 7 && yPosition != 0)
            {
                GameObject SE = board.tiles[xPosition + 1, yPosition - 1].GetComponent<Tile>().occupier;

                if (SE == null || SE.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition + 1, yPosition - 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (yPosition != 0)
            {
                GameObject S = board.tiles[xPosition, yPosition - 1].GetComponent<Tile>().occupier;

                if (S == null || S.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition, yPosition - 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (xPosition != 0 && yPosition != 0)
            {
                GameObject SW = board.tiles[xPosition - 1, yPosition - 1].GetComponent<Tile>().occupier;

                if (SW == null || SW.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition - 1, yPosition - 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (xPosition != 0)
            {
                GameObject W = board.tiles[xPosition - 1, yPosition].GetComponent<Tile>().occupier;

                if (W == null || W.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition - 1, yPosition].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (xPosition != 0 && yPosition != 7)
            {
                GameObject NW = board.tiles[xPosition - 1, yPosition + 1].GetComponent<Tile>().occupier;

                if (NW == null || NW.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition - 1, yPosition + 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }


           
        }

        if (!selected)
        {
            ClearTargets();
        }
    }

    public override void Move()
    {
        base.Move();
        GetComponent<BoardCoordinates>().xPosition = (int)(transform.position.x + 0.1);
        GetComponent<BoardCoordinates>().yPosition = (int)(transform.position.y + 0.1);

        selected = false;
    }
}