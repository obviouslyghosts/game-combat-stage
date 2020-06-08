using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

  private bool isTriggered = false;
  private float t = 0f;
  private float a = 0.4f;


  public void Init( float alarm )
  {
    a = alarm;
  }

  public bool IsTriggered()
  {
    return isTriggered;
  }

  public void Reset()
  {
    TimerSet();
  }

  private void Update()
  {
    TimerRun();
  }

  private void TimerRun()
  {
    if ( !isTriggered )
    {
      t += Time.deltaTime;
      isTriggered = ( t >= a );
    }
  }

  private void TimerSet()
  {
    t = 0f;
    isTriggered = ( t >= a );
  }


}
