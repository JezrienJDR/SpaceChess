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
    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[8, 8];

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                
                if( (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                {
                    tiles[i, j] = Instantiate(whiteTile, transform);
                }
                else
                {
                    tiles[i, j] = Instantiate(blackTile, transform);
                }

                tiles[i, j].transform.position = new Vector3(i, j, 0);
            }
        }

        //transform.Translate(-3.5f, -3.5f, 0);

        fleet.GetComponent<Fleet>().Setup();
        fleet2.GetComponent<Fleet2>().Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
