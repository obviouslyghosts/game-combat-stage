using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

  private bool timerIsRunning = true;
  private bool isAtGoal = false;
  private bool isAtDestination = false;
  private float timer = 0f;
  public float alarm = 0.6f;
  public float distanceThreshold = 0.01f;
  public float moveSpeed = 4f;
  public bool hasStaticDestination  = true;
  public EnemyGeneric mainBrain;

  public Transform movePoint;
  public Transform goalPoint;


  private void Start()
  {
    movePoint.parent = null;
    goalPoint.parent = null;
    isAtGoal = IsAtLoactionCheck("goal", goalPoint.position );
    isAtDestination = IsAtLoactionCheck( "move", movePoint.position );
  }

  private void Update()
  {
    if ( timerIsRunning )
    {
      TimerRun();
    }

  }

  public void RunTowardsNearest( string tag )
  {
    if ( !timerIsRunning )
    {
      MovePointOfInterest( tag );
      TimerSet();
    }

    MoveTowardsPointOfInterest();
  }

  public void GoalIsStationary( bool f )
  {
    hasStaticDestination = f;
  }

  private void MovePointOfInterest( string tag )
  {
    // rewrite to check if object still exists

    Debug.Log("Moving Point Of Interest");

    movePoint.position += GetOneSquareCloser( FindNearest( tag ) );
    isAtGoal = IsAtLoactionCheck( "goal", goalPoint.position );
    isAtDestination = IsAtLoactionCheck( "move", movePoint.position );
  }

  private void MoveTowardsPointOfInterest()
  {
    if ( isAtDestination )
    {
      return;
    }
    Debug.Log("Moving towards Point Of Interest");

    if ( IsAtLoactionCheck( "move", movePoint.position ) )
    {
      Debug.Log( "Arrived at destination" );
      mainBrain.UpdateMovement("None");
      isAtDestination = true;
    }
    else
    {
      transform.position = Vector3.MoveTowards( transform.position, movePoint.position, moveSpeed * Time.deltaTime );
    }
  }

  private bool IsAtLoactionCheck( string v, Vector3 pos )
  {
    float d = Vector3.Distance( transform.position, pos );
    Debug.Log("Checking Distance: " + v + ( d <= distanceThreshold ).ToString() );
    return ( Vector3.Distance( transform.position, pos ) <= distanceThreshold );
  }

  private Vector3 GetOneSquareCloser( Vector3 target )
  {
    int _x = 0;
    int _y = 0;

    if ( Mathf.Abs(transform.position.x - target.x) >= 0.5f)
    {
      // move 1 unit horizontaly closer
      _x = transform.position.x > target.x ? -1 : 1;
      transform.localScale = new Vector3( (float)_x,1f,1f);
      mainBrain.UpdateMovement("Vert");
      // audioManager.Play("KnightMove");
    }
    if (Mathf.Abs(transform.position.y - target.y) >= 0.5f)
    {
      // move 1 unit vertically closer
      _y = transform.position.y > target.y ? -1 : 1;
      mainBrain.UpdateMovement("Horiz");
      // audioManager.Play("KnightMove");
    }
    return( new Vector3( (float)_x, (float)_y, 0f ) ) ;
  }

  private Vector3 FindNearest( string tag )
  {
    float nearestItem = 1000f;
    Vector3 newTargetVector = Vector3.zero;
    GameObject[] searches = GameObject.FindGameObjectsWithTag(tag);

    if ( searches.Length > 0 )
    {
      foreach ( GameObject item in searches )
      {
        float d = Vector3.Distance( transform.position, item.transform.position );
        if ( d <= nearestItem )
        {
          nearestItem = d;
          newTargetVector = item.transform.position;
          goalPoint.position = item.transform.position;
        }
      }
      return newTargetVector;
    }
    // Trigger newSearch
    gameObject.GetComponent<EnemyGeneric>().TriggerNotFound( tag );

    return Vector3.zero;
  }

  private void TimerRun()
  {
    timer += Time.deltaTime;
    timerIsRunning = timer <= alarm;
  }

  private void TimerSet()
  {
    timer = 0f;
    timerIsRunning = timer <= alarm;
  }

}
