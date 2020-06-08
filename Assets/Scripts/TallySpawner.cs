using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallySpawner : MonoBehaviour
{
  public GameController gameController;
  private List<string> enemyList = new List<string>();

  // public bool isActive = true;
  private Timer timer;
  public float spawnInterval = 0.4f;

  // public GameObject[] spawnPoints;
  public GameObject[] enemies;
  public int enemiesMax = 5;
  private int enemiesSpawned = 0;
  private int enemiesLeft = 0;

  private void Start()
  {
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
    List<string> temp = gameController.GetTallyEnemies();
    foreach ( string s in temp)
    {
      enemyList.Add( s );
    }
    gameController.ClearTallyEnemies();
    Debug.Log( enemyList );
    Debug.Log( enemyList.Count );

    timer = GetComponent<Timer>();
    timer.Init( alarm: spawnInterval );
    timer.Reset();
  }


  void Update()
  {
    if ( timer.IsTriggered()  && enemyList.Count > 0)
    {
      string name = enemyList[ 0 ];
      enemyList.RemoveAt( 0 );
      Debug.Log("spawning " + name);
      Vector3 spawnPos = this.gameObject.transform.position;
      GameObject enemy = GetEnemy( name );
      Instantiate(enemy, spawnPos, Quaternion.identity);
      timer.Reset();
    }

  }

  private GameObject GetEnemy ( string name )
  {
    foreach ( GameObject e in enemies )
    {
      if ( e.GetComponent<EnemyTally>().type == name )
      {
        return e;
      }
    }
    return enemies[ UnityEngine.Random.Range(0, enemies.Length) ];
  }


}
