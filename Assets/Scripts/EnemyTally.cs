using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTally : MonoBehaviour
{
  public string type = "knight";
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

  }

  private void MoveTowardsPointOfInterest()
  {
    transform.position = Vector3.MoveTowards( transform.position, movePoint.position, moveSpeed * Time.deltaTime );

  }


  private bool IsAtLoactionCheck( Vector3 pos )
  {
    return ( Vector3.Distance( transform.position, pos ) <= distanceThreshold );
  }

}
