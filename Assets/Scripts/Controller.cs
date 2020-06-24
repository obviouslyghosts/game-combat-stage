using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

  public float threshold = 0.1f;

    // Update is called once per frame
    void Update()
    {
      Vector3 mov = new Vector3 ( ClampInput( Input.GetAxis( "Horizontal" ) ), ClampInput( Input.GetAxis( "Vertical" ) ), 0f );

      transform.position += mov;


    }


    private float ClampInput( float i )
    {
      return ( Mathf.Abs( i ) >= threshold ? 1 * Mathf.Sign( i ) : 0f );
    }
}
