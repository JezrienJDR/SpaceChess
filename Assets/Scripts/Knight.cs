using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
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

            if (yPosition +2 <= 7 && xPosition + 1 <= 7)
            {
                GameObject NNE = board.tiles[xPosition + 1, yPosition + 2].GetComponent<Tile>().occupier;

                if (NNE == null || NNE.GetComponent<IFF>().friend == false)
                {
                    if (target == null)
                    {
                        Debug.Log("TARGET IS NULL");
                    }

                    GameObject t = Instantiate(target, board.tiles[xPosition + 1, yPosition + 2].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (xPosition + 2 <= 7 && yPosition + 1 <= 7)
            {
                GameObject ENE = board.tiles[xPosition + 2, yPosition + 1].GetComponent<Tile>().occupier;

                if (ENE == null || ENE.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition + 2, yPosition + 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (xPosition + 2 <= 7 && yPosition - 1 >= 0)
            {
                GameObject ESE = board.tiles[xPosition + 2, yPosition - 1].GetComponent<Tile>().occupier;


                if (ESE == null || ESE.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition + 2, yPosition - 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (xPosition + 1 <= 7 && yPosition -2 >= 0)
            {
                GameObject SSE = board.tiles[xPosition + 1, yPosition - 2].GetComponent<Tile>().occupier;

                if (SSE == null || SSE.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition + 1, yPosition - 2].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (yPosition -2 >= 0 && xPosition - 1 >= 0)
            {
                GameObject SSW = board.tiles[xPosition -1, yPosition - 2].GetComponent<Tile>().occupier;

                if (SSW == null || SSW.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition - 1, yPosition - 2].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (xPosition -2 >= 0 && yPosition -1 >= 0)
            {
                GameObject WSW = board.tiles[xPosition - 2, yPosition - 1].GetComponent<Tile>().occupier;

                if (WSW == null || WSW.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition - 2, yPosition - 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (xPosition -2 >= 0 && yPosition + 1 <= 7)
            {
                GameObject WNW = board.tiles[xPosition - 2, yPosition + 1].GetComponent<Tile>().occupier;

                if (WNW == null || WNW.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition - 2, yPosition + 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);
                }
            }

            if (xPosition - 1 >= 0 && yPosition +2 <= 7)
            {
                GameObject NNW = board.tiles[xPosition - 1, yPosition + 2].GetComponent<Tile>().occupier;

                if (NNW == null || NNW.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition - 1, yPosition + 2].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
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
