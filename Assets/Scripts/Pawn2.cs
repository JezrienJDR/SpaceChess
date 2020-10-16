using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn2 : Piece
{
    public GameObject target;



    bool StartingPosition = true;


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClicked()
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


            if (yPosition < 7)
            {
                GameObject o = board.tiles[xPosition, yPosition - 1].GetComponent<Tile>().occupier;

                if (o == null || o.GetComponent<IFF>().friend == false)
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition, yPosition - 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);


                    if (StartingPosition)
                    {
                        GameObject p = board.tiles[xPosition, yPosition - 2].GetComponent<Tile>().occupier;

                        if (p == null || p.GetComponent<IFF>().friend == false)
                        {
                            GameObject t2 = Instantiate(target, board.tiles[xPosition, yPosition - 2].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                            t2.transform.SetParent(transform);
                            possibleTargets.Add(t2);
                        }
                    }
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

        if (StartingPosition)
        {
            StartingPosition = false;
        }
        selected = false;
    }

    private void OnMouseOver()
    {
        //Debug.Log("OVER");
    }

    private void OnMouseUp()
    {
        // Debug.Log("UP");
    }
}