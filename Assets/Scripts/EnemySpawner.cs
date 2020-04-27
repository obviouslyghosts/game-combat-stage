using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

  public float spawnAlarm = 1f;
  private float spawnTimer= 0f;
  public GameObject[] spawnPoints;
  public GameObject[] enemies;
  public int maxEnemies = 1;
  private int spawnedEnemies = 0;

  void Update()
  {
    if ( SpawnTimerCheck() && (spawnedEnemies < maxEnemies))
    {

      Vector3 spawnPos = spawnPoints[ UnityEngine.Random.Range(0, spawnPoints.Length) ].transform.position;
      GameObject enemy = enemies[ UnityEngine.Random.Range(0, enemies.Length) ];

      Instantiate(enemy, spawnPos, Quaternion.identity);

      spawnedEnemies++;

    }


  }


  private bool SpawnTimerCheck()
  {
    if (spawnTimer >= spawnAlarm)
    {
      spawnTimer = 0;
      return true;
    }
    spawnTimer+= Time.deltaTime;
    return false;
  }
}
