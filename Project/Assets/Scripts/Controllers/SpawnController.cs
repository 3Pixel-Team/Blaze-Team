using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

 
    

    public EnemyInfoSO enemyInfo;

    void Start()
    {
        enemyInfo.enemyCount = 0;

        if (enemyInfo.enemies != null)
        {
            StartCoroutine(EnemySpawnner());
            enemyInfo.waveCount = 1;
        }


    }
   


    // find number of enemys spawned and increase by one every set number of seconds until max number of enemys are spawned
    IEnumerator EnemySpawnner()
    {
        while (enemyInfo.enemyCount < enemyInfo.maxEnemies)
        {
            Vector3 spawnpoint = enemyInfo.spawnPoints[Random.Range(0,enemyInfo.spawnPoints.Length)].transform.position;
            Instantiate(enemyInfo.enemies[Random.Range(0,enemyInfo.enemies.Length)], spawnpoint, Quaternion.identity);
            yield return new WaitForSeconds(enemyInfo.spawnDelay);
            enemyInfo.enemyCount += 1;
        }
    }

    private void Update()
    {
        
    }
}