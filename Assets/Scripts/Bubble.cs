using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBaseAttribute]
public class Bubble : MonoBehaviour
{
    public BubbleElement element;
    public BubbleStats stats;
    public IntVariable bombScore;
    public IntVariable bubblesToSpawn;

    public GameObject explosionSFX;

    [HideInInspector] public int foundBubble;
    [HideInInspector] public List<Bubble> neihbours = new List<Bubble>();
    [HideInInspector] public bool visited = false;

    private bool exploded;

    Bubble tempEnter;
    Bubble tempExit;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        tempEnter = obj.GetComponent<Bubble>();
        
        if(tempEnter == null) return;

        if(tempEnter.element.combine.Contains(element))
        {
            if(!neihbours.Contains(tempEnter))
            {
                if(tempEnter.element == element)
                {
                    foundBubble++;
                }

                neihbours.Add(tempEnter);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        tempExit = obj.GetComponent<Bubble>();

        if(neihbours.Contains(tempExit))
        {
            if(tempExit.element == element)
            {
                foundBubble--;
            }
            neihbours.Remove(tempExit);
        }
    }

    public void Explode()
    {
        if(exploded) return;

        if(element.bombPoints)
        {
            bombScore.Value++;
        }

        stats.colected++;
        stats.toColect--;
        bubblesToSpawn.Value++;

        exploded = true;
        Instantiate(explosionSFX,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }


}
