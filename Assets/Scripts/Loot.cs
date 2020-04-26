using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

  public int value = 1;


  public void PickedUp()
  {
    Destroy(gameObject);
  }

}
