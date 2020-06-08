using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTally : MonoBehaviour
{
  public string type = "knight";
  public int holdingValue = 5;
  public float moveSpeed = 4f;
  public float spawnInterval = 0.4f;
  public float waitAtShop = 1f;
  public Transform movePoint;
  public GameObject[] allPoints;
  private int pointNumber = 0;
  private bool doneMoving = false;

  private Timer timer;
  private bool atDestination = true;
  public float distanceThreshold = 0.01f;


  private void Start()
  {
    timer = GetComponent<Timer>();
    timer.Init( alarm: spawnInterval );
    timer.Reset();
    movePoint.parent = null;
    pointNumber = 0;

    if ( allPoints.Length < 1 )
    {
      allPoints = GameObject.Find( "TallyPoints" ).GetComponent<TallyPoints>().GetAllPoints();
    }
  }

  private void Update()
  {
    if ( timer.IsTriggered() && atDestination )
    {
      // move point to new spot
      if ( pointNumber < allPoints.Length )
      {
        timer.Init( alarm: waitAtShop );
        movePoint.position = allPoints[ pointNumber ].transform.position;
        FaceMeTowardsGoal( movePoint.position );
        pointNumber ++;
        atDestination = IsAtLoactionCheck( movePoint.position );
      }
      timer.Reset();
    }

    if ( !atDestination )
    {
      // run towards point
      transform.position = Vector3.MoveTowards( transform.position, movePoint.position, moveSpeed * Time.deltaTime );
      atDestination = IsAtLoactionCheck( movePoint.position );
    }

    if ( atDestination && pointNumber >= allPoints.Length )
    {
      // GameObject.Find( "TallySpawner" ).GetComponent<TallySpawner>().AddWealth( holdingValue );
      Destroy( gameObject );
    }

  }

  private void MoveTowardsPointOfInterest()
  {
    transform.position = Vector3.MoveTowards( transform.position, movePoint.position, moveSpeed * Time.deltaTime );
  }

  private void FaceMeTowardsGoal( Vector3 target )
  {
    int _x = transform.position.x > target.x ? -1 : 1;
    transform.localScale = new Vector3( (float)_x,1f,1f);
  }


  private bool IsAtLoactionCheck( Vector3 pos )
  {
    return ( Vector3.Distance( transform.position, pos ) <= distanceThreshold );
  }

}
