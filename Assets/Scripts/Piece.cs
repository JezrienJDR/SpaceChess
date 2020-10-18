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

    public bool alive = true;
    public bool inPlay = true;



    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill()
    {
        board.AddToDead(gameObject);
        GetComponent<BoardCoordinates>().xPosition = 666;
        GetComponent<BoardCoordinates>().yPosition = 666;

        alive = false;
        inPlay = false;
    }

    public virtual void Move()
    {
        ClearTargets();
        GetComponent<BoardCoordinates>().xPosition = (int)(transform.position.x + 0.1);
        GetComponent<BoardCoordinates>().yPosition = (int)(transform.position.y + 0.1);

        currentTile.occupier = null;

        currentTile = board.tiles[GetComponent<BoardCoordinates>().xPosition, GetComponent<BoardCoordinates>().yPosition].GetComponent<Tile>();

        if(currentTile.occupier != null)
        {
            currentTile.occupier.GetComponent<Piece>().Kill();
        }

        currentTile.occupier = gameObject;

        board.GetComponent<Board>().EndTurn();
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
