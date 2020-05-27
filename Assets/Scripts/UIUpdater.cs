using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{

  public Text UIhealth;
  public Text UIwealth;

  private void Start()
  {
    GameController gc = GameObject.Find("GameController").GetComponent<GameController>();
    UpdateUI(gc.health, gc.wealth);
  }


  public void UpdateUI( int h, int w)
  {
    UIhealth.text = h.ToString();
    UIwealth.text = w.ToString();
  }

}
