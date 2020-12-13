using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pawn : Piece
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
        if (alive == false || inPlay == false)
        {
            return;
        }

        selected = !selected;

        if(board == null)
        {
            Debug.Log("BOARD IS NULL. WHYYYYYYY!?!?!?!");
        }

        if(selected)
        {
            int xPosition = GetComponent<BoardCoordinates>().xPosition;
            int yPosition = GetComponent<BoardCoordinates>().yPosition;


            if (yPosition < 7)
            {
                GameObject o = board.tiles[xPosition, yPosition + 1].GetComponent<Tile>().occupier;

                if (o == null )
                {
                    GameObject t = Instantiate(target, board.tiles[xPosition, yPosition + 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t.transform.SetParent(transform);
                    possibleTargets.Add(t);


                    if (StartingPosition)
                    {
                        GameObject p = board.tiles[xPosition, yPosition + 2].GetComponent<Tile>().occupier;

                        if (p == null )
                        {
                            GameObject t2 = Instantiate(target, board.tiles[xPosition, yPosition + 2].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                            t2.transform.SetParent(transform);
                            possibleTargets.Add(t2);
                        }
                    }
                }

                GameObject oR = board.tiles[xPosition + 1, yPosition + 1].GetComponent<Tile>().occupier;

                if(oR != null && oR.GetComponent<IFF>().friend != GetComponent<IFF>().friend)
                {
                    GameObject t3 = Instantiate(target, board.tiles[xPosition + 1, yPosition + 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t3.transform.SetParent(transform);
                    possibleTargets.Add(t3);
                }

                GameObject oL = board.tiles[xPosition - 1, yPosition + 1].GetComponent<Tile>().occupier;

                if (oL != null && oL.GetComponent<IFF>().friend != GetComponent<IFF>().friend)
                {
                    GameObject t4 = Instantiate(target, board.tiles[xPosition - 1, yPosition + 1].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                    t4.transform.SetParent(transform);
                    possibleTargets.Add(t4);
                }
            }

        }

        if(!selected)
        {
            ClearTargets();
        }
    }


    public override void Move()
    {
        base.Move();
        //GameObject NewTile = board.GetNearestTile(transform.position);
        //GetComponent<BoardCoordinates>().xPosition = NewTile.GetComponent<Tile>().x;
        //GetComponent<BoardCoordinates>().yPosition = NewTile.GetComponent<Tile>().y;

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
