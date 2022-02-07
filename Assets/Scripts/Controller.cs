using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public IntVariable moves;
    public IntVariable bombSpawnScore;
    public Vector3Variable bubblePosition;

    public GameEvent bubblePoped;
    public GameEvent bombSpawn;

    private Bubble bubble;
    private Bomb bomb;

    private List<Bubble> neihbours = new List<Bubble>();

    private bool madeMove;

    RaycastHit2D hit;
    

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            neihbours.Clear();
            bombSpawnScore.Value = 0;
            madeMove = false;

            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null)
            {
                bubble = hit.collider.gameObject.GetComponent<Bubble>();
                bomb = hit.collider.gameObject.GetComponent<Bomb>();

                if(bubble && bubble.element.clickable)
                {
                    bubblePosition.Value = bubble.gameObject.transform.position;

                    if(!madeMove && bubble.foundBubble > 0)
                    {
                        moves.Value--;
                        madeMove = true;
                        bubblePoped.Raise();
                        bombSpawn.Raise();
                    }

                    GetAllNeihbours(bubble.neihbours);

                    for (int i = 0; i < neihbours.Count; i++)
                    {
                        neihbours[i].Explode();
                    }

                }

                if(bomb)
                {
                    bomb.GetAllObjectInRange(0);
                
                    if(!madeMove)
                    {
                        moves.Value--;
                        madeMove = true;
                    }
                }

            }



  
        }

    }

    private void GetAllNeihbours(List<Bubble> _neihbours)
    {
        for (int i = 0; i < _neihbours.Count; i++)
        {
            if(_neihbours[i].visited) continue;
            if(_neihbours[i].foundBubble == 0) continue;

            _neihbours[i].visited = true;
        
            for (int z = 0; z < _neihbours[i].neihbours.Count; z++)
            {
                if(!neihbours.Contains(_neihbours[i].neihbours[z]))
                {
                    neihbours.Add(_neihbours[i].neihbours[z]);
                }
            }

            GetAllNeihbours(_neihbours[i].neihbours);
        }
    }
}
