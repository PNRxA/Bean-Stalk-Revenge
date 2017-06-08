using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Enemy[] enemyPrefab;
    public Transform spawnPoint;

    public static float countdown = 3f;

    private int waveIndex = 1;

    // Update is called once per frame
    void Update()
    {
        // If the countdown is <= 0 and not in a wave then spawn a wave
        if (countdown <= 0 && !GameManager.inWave)
        {
            GameManager.inWave = true;
            StartCoroutine(SpawnWave());
        }
        // If the countdown is active then count down
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave()
    {
        // Spawn enemy based on wave number
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        // Increase wave number after whole wave has been spawned
        waveIndex++;
    }

    // Spawn random enemy
    void SpawnEnemy()
    {
        Enemy spawnEnemy = Instantiate(enemyPrefab[Random.Range(0, 3)], spawnPoint.position, spawnPoint.rotation);
        // Enemy health is set based on the wave you're on
        spawnEnemy.health *= waveIndex;
        spawnEnemy.target = GameObject.FindGameObjectWithTag("Target").transform;
    }
}
