using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] powerUps = new GameObject[3];
    public GameObject[] enemies = new GameObject[2];
    public float xRange;
    public float xAmount;
    public float zAmount;

    public float zRange;
    private int enemyCount;
    private int spawnAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnAmount = 1;
        enemyCount = 0;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount ==0 )
        {
            spawnAmount += 2;
            SpawnEnemy(spawnAmount);
            SpawnPowerUp();
        }
    }

    public void SpawnEnemy(int amount)
    {
        
        for (int i = 0; i < amount; i++)
        {
            float probablity = Random.Range(0, 1f);
            int index;
            if (probablity < 0.3f)
            {
                index = 0;
            }
            else
            {
                index = 1;
            }

            float spawnX = Random.Range(-xRange, xRange);
            float spawnZ = Random.Range(-zRange, zRange);
            Vector3 vc = new Vector3(spawnX, 0, spawnZ);
            Instantiate(enemies[index], vc, Quaternion.identity);
        }
        
    }

    public void SpawnPowerUp()
    {
        float probability = Random.Range(0, 1);
        if (probability < 1f)
        {
            int index = Random.Range(0, 3);
            float xPos = Random.Range(-xAmount, xAmount);
            float zPos = Random.Range(-zAmount, zAmount);
            Instantiate(powerUps[index], new Vector3(xPos, 0.5f, zPos) ,Quaternion.identity);
        }
    }
}
