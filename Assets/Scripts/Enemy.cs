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

  public Animator anim;

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
      UpdateMoveTowardsPoint();
      Debug.Log("Moving Point");
    }

    StepTowardsPoint();
  }

  private bool MoveTimerCheck()
  {
    if (moveTimer >= moveAlarm)
    {
      moveTimer = 0;
      return true;
    }
    moveTimer+= Time.deltaTime;
    return false;
  }

  private void UpdateMoveTowardsPoint()
  {
    // check for colissions!!!

    
    float nearestItem = 0f;
    Vector3 newTargetVector = Vector3.zero;
    GameObject[] loot = GameObject.FindGameObjectsWithTag("Loot");

    if (loot.Length > 0)
    {
      int _x = 0;
      int _y = 0;
      foreach (GameObject item in loot)
      {
        float d = Vector3.Distance(transform.position, item.transform.position);
        if (d > nearestItem) {
          nearestItem = d;
          newTargetVector = item.transform.position;
        }
      }

      if ( Mathf.Abs(transform.position.x - newTargetVector.x) >= 0.1f)
      {
        // move 1 unit horizontaly closer
        _x = transform.position.x > newTargetVector.x ? -1 : 1;
        transform.localScale = new Vector3( (float)_x,1f,1f);
        anim.SetBool("moveHoriz", true);
        anim.SetBool("moveVert", false);
      }
      else if (Mathf.Abs(transform.position.y - newTargetVector.y) >= 0.1f)
      {
        // move 1 unit vertically closer
        _y = transform.position.y > newTargetVector.y ? -1 : 1;
        anim.SetBool("moveVert", true);
        anim.SetBool("moveHoriz", false);
      }

      movePoint.position += new Vector3((float)_x,(float)_y,0f);
    }

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
      anim.SetBool("moveHoriz", false);
      anim.SetBool("moveVert", false);
      Debug.Log("Arrived at point");
    }

  }


}
