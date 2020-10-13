using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    
    [SerializeField]
    public Board board;

    public List<GameObject> possibleTargets;

    public bool selected = false;

    public Tile currentTile;



    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Move()
    {
        ClearTargets();
        GetComponent<BoardCoordinates>().xPosition = (int)(transform.position.x + 0.1);
        GetComponent<BoardCoordinates>().yPosition = (int)(transform.position.y + 0.1);

        currentTile.occupier = null;

        currentTile = board.tiles[GetComponent<BoardCoordinates>().xPosition, GetComponent<BoardCoordinates>().yPosition].GetComponent<Tile>();

        currentTile.occupier = gameObject;
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
