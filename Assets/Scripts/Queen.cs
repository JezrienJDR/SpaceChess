using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
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

        if (alive == false || inPlay == false)
        {
            return;
        }

        selected = !selected;

        if (board == null)
        {
            Debug.Log("BOARD IS NULL. WHYYYYYYY!?!?!?!");
        }

        if (selected)
        {
            int xPosition = GetComponent<BoardCoordinates>().xPosition;
            int yPosition = GetComponent<BoardCoordinates>().yPosition;

            int NE = Mathf.Min(8 - xPosition, 8 - yPosition);

            int SW = Mathf.Min(xPosition + 1, yPosition + 1);

            int NW = Mathf.Min(xPosition + 1, 8 - yPosition);

            int SE = Mathf.Min(8 - xPosition, yPosition + 1);

            // Northeast
            for (int i = 1; i < NE; i++)
            {

                GameObject o = board.tiles[xPosition + i, yPosition + i].GetComponent<Tile>().occupier;

                bool enemyTargeted = false;

                if (o != null)
                {
                    if (o.GetComponent<IFF>().friend == GetComponent<IFF>().friend)
                    {
                        Debug.Log("FRIEND TARGETED!");
                        break;
                    }
                    else {
                        Debug.Log("ENEMY TARGETED");
                        enemyTargeted = true;
                    }
                }


                GameObject t = Instantiate(target, board.tiles[xPosition + i, yPosition + i].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                t.transform.SetParent(transform);
                possibleTargets.Add(t);

                if (enemyTargeted)
                {
                    break;
                }
            }

            // Southwest
            for (int i = 1; i < SW; i++)
            {

                GameObject o = board.tiles[xPosition - i, yPosition - i].GetComponent<Tile>().occupier;

                bool enemyTargeted = false;

                if (o != null)
                {
                    if (o.GetComponent<IFF>().friend == GetComponent<IFF>().friend)
                    {
                        Debug.Log("FRIEND TARGETED!");
                        break;
                    }
                    else {
                        Debug.Log("ENEMY TARGETED");
                        enemyTargeted = true;
                    }
                }


                GameObject t = Instantiate(target, board.tiles[xPosition - i, yPosition - i].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                t.transform.SetParent(transform);
                possibleTargets.Add(t);

                if (enemyTargeted)
                {
                    break;
                }
            }

            // Northwest
            for (int i = 1; i < NW; i++)
            {

                GameObject o = board.tiles[xPosition - i, yPosition + i].GetComponent<Tile>().occupier;

                bool enemyTargeted = false;

                if (o != null)
                {
                    if (o.GetComponent<IFF>().friend == GetComponent<IFF>().friend)
                    {
                        Debug.Log("FRIEND TARGETED!");
                        break;
                    }
                    else {
                        Debug.Log("ENEMY TARGETED");
                        enemyTargeted = true;
                    }
                }


                GameObject t = Instantiate(target, board.tiles[xPosition - i, yPosition + i].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                t.transform.SetParent(transform);
                possibleTargets.Add(t);

                if (enemyTargeted)
                {
                    break;
                }
            }


            // Southeast
            for (int i = 1; i < SE; i++)
            {

                GameObject o = board.tiles[xPosition + i, yPosition - i].GetComponent<Tile>().occupier;

                bool enemyTargeted = false;

                if (o != null)
                {
                    if (o.GetComponent<IFF>().friend == GetComponent<IFF>().friend)
                    {
                        Debug.Log("FRIEND TARGETED!");
                        break;
                    }
                    else {
                        Debug.Log("ENEMY TARGETED");
                        enemyTargeted = true;
                    }
                }


                GameObject t = Instantiate(target, board.tiles[xPosition + i, yPosition - i].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                t.transform.SetParent(transform);
                possibleTargets.Add(t);

                if (enemyTargeted)
                {
                    break;
                }
            }



            for (int y = yPosition + 1; y < 8; y++)
            {
                GameObject o = board.tiles[xPosition, y].GetComponent<Tile>().occupier;

                bool enemyTargeted = false;

                if (o != null)
                {
                    if (o.GetComponent<IFF>().friend == GetComponent<IFF>().friend)
                    {
                        Debug.Log("FRIEND TARGETED!");
                        break;
                    }
                    else {
                        Debug.Log("ENEMY TARGETED");
                        enemyTargeted = true;
                    }
                }

                GameObject t = Instantiate(target, board.tiles[xPosition, y].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                t.transform.SetParent(transform);
                possibleTargets.Add(t);

                if (enemyTargeted)
                {
                    break;
                }
            }

            for (int y = yPosition - 1; y >= 0; y--)
            {
                GameObject o = board.tiles[xPosition, y].GetComponent<Tile>().occupier;

                bool enemyTargeted = false;

                if (o != null)
                {
                    if (o.GetComponent<IFF>().friend == GetComponent<IFF>().friend)
                    {
                        Debug.Log("FRIEND TARGETED!");
                        break;
                    }
                    else
                    {
                        Debug.Log("ENEMY TARGETED");
                        enemyTargeted = true;
                    }
                }

                GameObject t = Instantiate(target, board.tiles[xPosition, y].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                t.transform.SetParent(transform);
                possibleTargets.Add(t);

                if (enemyTargeted)
                {
                    break;
                }
            }

            for (int x = xPosition + 1; x < 8; x++)
            {
                GameObject o = board.tiles[x, yPosition].GetComponent<Tile>().occupier;

                bool enemyTargeted = false;

                if (o != null)
                {
                    if (o.GetComponent<IFF>().friend == GetComponent<IFF>().friend)
                    {
                        Debug.Log("FRIEND TARGETED!");
                        break;
                    }
                    else
                    {
                        Debug.Log("ENEMY TARGETED");
                        enemyTargeted = true;
                    }
                }


                GameObject t = Instantiate(target, board.tiles[x, yPosition].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                t.transform.SetParent(transform);
                possibleTargets.Add(t);

                if (enemyTargeted)
                {
                    break;
                }
            }

            for (int x = xPosition - 1; x >= 0; x--)
            {
                GameObject o = board.tiles[x, yPosition].GetComponent<Tile>().occupier;

                bool enemyTargeted = false;

                if (o != null)
                {
                    if (o.GetComponent<IFF>().friend == GetComponent<IFF>().friend)
                    {
                        Debug.Log("FRIEND TARGETED!");
                        break;
                    }
                    else
                    {
                        Debug.Log("ENEMY TARGETED");
                        enemyTargeted = true;
                    }
                }

                GameObject t = Instantiate(target, board.tiles[x, yPosition].transform.position + new Vector3(0, 0, -2), new Quaternion(0, 0, 0, 0));
                t.transform.SetParent(transform);
                possibleTargets.Add(t);

                if (enemyTargeted)
                {
                    break;
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
