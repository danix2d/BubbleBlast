using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public IntVariable bombScore;
    public Vector3Variable position;

    public GameObject[] bombPrefabs;

    [Header("Bubbles Settings")]
    public int initBubbleSpawn;
    public IntVariable bubblesToSpawn;
    public List<SpawnChance> bubbles = new List<SpawnChance>();

    private bool spawned;

    private void OnEnable() {
        bombScore.Value = 0;
        bubblesToSpawn.Value = initBubbleSpawn;
    }

    private void Update() {

        if(!spawned)
        {
            StartCoroutine(SpawnBubbles(0.05f));
        }
    }

    IEnumerator SpawnBubbles(float waitTime)
    {
        if(spawned){yield break;}
        if(bubblesToSpawn.Value == 0){yield break;}

        spawned = true;

        while(bubblesToSpawn.Value > 0)
        {
            Vector3 rand = new Vector3(transform.position.x + Random.Range(-3,3),transform.position.y + Random.Range(-3,3));
            Instantiate(CalculateWeight(), rand, Quaternion.identity);
            bubblesToSpawn.Value--;
            yield return new WaitForSeconds(waitTime);
        }

        spawned = false;
    }

    public void SpawnBomb()
    {
        if(bombScore.Value >= 4)
        {
            Instantiate(bombPrefabs[0], position.Value, Quaternion.identity);
        }

        bombScore.Value = 0;
    }



    private GameObject CalculateWeight()
    {
        int weight = 0;
        GameObject bubble = null;
        
        for (int i = 0; i < bubbles.Count; i++)
        {
            weight += bubbles[i].rarity;
        }

        int randValue = Random.Range(0, weight);
        int top = 0;
        
        for (int z = 0; z < bubbles.Count; z++)
        {
            top += bubbles[z].rarity;
            if (randValue < top){
                bubble = bubbles[z].bubblePrefab;
                break;
            }
        }
        return bubble;
    }


    [System.Serializable]
    public class SpawnChance
    {
        public GameObject bubblePrefab; 
        public int rarity;
    }
}
