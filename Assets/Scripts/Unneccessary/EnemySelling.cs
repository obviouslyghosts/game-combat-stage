using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelling : MonoBehaviour
{

  public Transform movePoint;
  public Transform goalPoint;

  public Transform[] points;
  private int atPoint = 0;
  private bool hasPoints = false;

  private void Start()
  {
    hasPoints = points.Length > 0;
    if ( !hasPoints )
    {
      // get array of points!
    }
  }

  private void Update()
  {
    if ( hasPoints )
    {

    }

  }
}
