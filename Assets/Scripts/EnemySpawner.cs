using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

  public bool isActive = true;
  public float spawnAlarm = 1f;
  private float spawnTimer= 0f;
  public GameObject[] spawnPoints;
  public GameObject[] enemies;
  public int enemiesMax = 5;
  private int enemiesSpawned = 0;
  private int enemiesLeft = 0;


  void Update()
  {
    if (isActive)
    {
      if ( SpawnTimerCheck() && (enemiesSpawned < enemiesMax))
      {
        Vector3 spawnPos = spawnPoints[ UnityEngine.Random.Range(0, spawnPoints.Length) ].transform.position;
        GameObject enemy = enemies[ UnityEngine.Random.Range(0, enemies.Length) ];
        Instantiate(enemy, spawnPos, Quaternion.identity);
        enemiesSpawned++;
      }
      if (enemiesLeft >= enemiesMax)
      {
        GameObject.Find("GameState").GetComponent<GameStateController>().WaveEnded();
      }
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

  public void NewWave()
  {

  }

  public void LeftRoom()
  {
    enemiesLeft++;
  }

  public void SetScriptActive(bool v)
  {
    isActive = v;
  }
}
