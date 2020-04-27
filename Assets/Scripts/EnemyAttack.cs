using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
  public bool isAttacking;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag =="Player")
    {
      Debug.Log("attacking!");
      isAttacking = true;
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.tag =="Player")
    {
      Debug.Log("not attacking!");
      isAttacking = false;
    }
  }

}
