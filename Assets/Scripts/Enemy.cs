using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public float brainSpeed = 1f;
  public float moveSpeed = 4f;
  public float moveAlarm = 1f;
  public Transform movePoint;
  public int backpackSize;

  private int level = 1;
  private int backpack = 0;
  private float moveTimer = 0;

  void Start()
  {
    movePoint.parent = null;
  }

  void Update()
  {
    if (MoveTimerCheck())
    {
      // move the move point
      Debug.Log("Moving Point");
    }

    StepTowardsPoint();
  }

  private bool MoveTimerCheck()
  {
    if (moveTimer>= moveAlarm)
    {
      moveTimer = 0;
      return true;
    }
    moveTimer+= Time.deltaTime;
    return false;
  }

  private void StepTowardsPoint()
  {
    if (Vector3.Distance(transform.position,movePoint.position) >= 0.01f)
    {
      transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
      Debug.Log("Moving towards point");
    }
    else
    {
      Debug.Log("Arrived at point");
    }

  }


}
