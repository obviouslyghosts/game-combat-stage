using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

  public float spawnAlarm = 1f;
  private float spawnTimer= 0f;
  public GameObject[] spawnPoints;
  public GameObject[] enemies;

  void Update()
  {
    if ( SpawnTimerCheck() )
    {

      Vector3 spawnPos = spawnPoints[ UnityEngine.Random.Range(0, spawnPoints.Length) ].transform.position;
      GameObject enemy = enemies[ UnityEngine.Random.Range(0, enemies.Length) ];

      Instantiate(enemy, spawnPos, Quaternion.identity);

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
