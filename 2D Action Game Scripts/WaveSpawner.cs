using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currWave;
    private int CurrWaveIndex;
    private Transform player;

    private bool finishedSpawning;

    public GameObject Boss;
    public Transform BossSpawnPoint;

    public GameObject healthBar;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(CurrWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }
     
    IEnumerator SpawnWave(int index)
    {
        currWave = waves[index];

        for(int i = 0; i < currWave.count; i++)
        {
            if(player == null)
            {
                yield break;
            }

            Enemy RandomEnemy = currWave.enemies[Random.Range(0, currWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(RandomEnemy, randomSpot.position, randomSpot.rotation);

            if(i == currWave.count - 1)
            {
                finishedSpawning = true;
            }
            else
            {
                finishedSpawning = false;
            }

            yield return new WaitForSeconds(currWave.timeBetweenSpawns);
        }
    }

    private void Update()
    {
        if(finishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawning = false;
            if(CurrWaveIndex + 1 < waves.Length)
            {
                CurrWaveIndex++;
                StartCoroutine(StartNextWave(CurrWaveIndex));
            }
            else
            {
                Instantiate(Boss, BossSpawnPoint.position, BossSpawnPoint.rotation);
                healthBar.SetActive(true);
            }
        }
    }
}
