using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameEvent bubblePoped;

    public BombElement element;

    public GameObject explosionSFX;
    private bool exploded;
    
    private List<RaycastHit2D> collisions = new List<RaycastHit2D>();

    [Range(1,4)]public int directions = 4;

    public float hitSize = 1f;

    public float distance = 25;

    [HideInInspector] public bool foundBomb;

    private bool found;

    private Bubble bubble;
    private Bomb bomb;

    Bomb tempBomb;
    public bool hitBomb;

    public void GetAllObjectInRange(float time)
    {
        StartCoroutine(WaitTime(time));
    }

    IEnumerator WaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        float stepAngleSize = 360 / directions;

        collisions.Clear();

        for (int i = 0; i < directions; i++)
        {
            float angle = 90 - 360 / 2 + stepAngleSize * i;
            Vector3 dir = DirFromAngle(angle);
            collisions.AddRange(Physics2D.CircleCastAll(transform.position,hitSize,dir,distance));
        }

        for (int z = 0; z < collisions.Count; z++)
        {
            bubble = collisions[z].transform.gameObject.GetComponent<Bubble>();
            bomb = collisions[z].transform.gameObject.GetComponent<Bomb>();

            if(bubble)
            {
                bubble.Explode();
            }

            if(bomb && !bomb.found && !foundBomb && bomb.gameObject != gameObject)
            {
                bomb.GetAllObjectInRange(1f);
                bomb.found = true;
                foundBomb = true;
                Debug.Log(bomb.gameObject);
            }
        }

        bubblePoped.Raise();

        Explode();
    }

    private void Explode()
    {
        if(exploded) return;
        
        exploded = true;

        Instantiate(explosionSFX,transform.position,Quaternion.identity);

        Destroy(gameObject);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(hitBomb) return;

        Vector2 point = other.ClosestPoint((Vector2)transform.position);

        tempBomb = other.GetComponent<Bomb>();
        if(tempBomb == null) return;
        tempBomb.hitBomb = true;

        if(tempBomb.element.combine.Contains(element))
        {
            Instantiate(element.combinedBomb,point,Quaternion.identity);
            hitBomb = true;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }



    private Vector3 DirFromAngle(float angleInDegrees)
    {
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
