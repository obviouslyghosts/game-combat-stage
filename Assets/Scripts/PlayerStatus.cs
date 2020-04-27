using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

  public int health;
  public int wealth;

  public void Attacked(int damage)
  {
    health -= damage;
  }

  public void Spew(int value)
  {
    wealth -= value;
  }

}
