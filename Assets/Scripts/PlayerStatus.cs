using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
  public Text UIhealth;
  public Text UIwealth;

  public int health;
  public int wealth;

  public void Attacked(int damage)
  {
    health -= damage;
    UIhealth.text = health.ToString();
  }

  public void Spew(int value)
  {
    wealth -= value;
    UIwealth.text = wealth.ToString();
  }

  public void Pickup(int value)
  {
    wealth += value;
    UIwealth.text = wealth.ToString();
  }

}
