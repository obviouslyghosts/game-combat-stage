using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{

  public Animator anim;

  public void UpdateMovement( string direction )
  {

    switch ( direction )
    {
      case "Horiz":
      anim.SetBool("moveHoriz", true);
      anim.SetBool("moveVert", false);
      break;

      case "Vert":
      anim.SetBool("moveHoriz", false);
      anim.SetBool("moveVert", true);
      break;

      case "None":
      anim.SetBool("moveHoriz", false);
      anim.SetBool("moveVert", false);
      break;

    }

  }
}
