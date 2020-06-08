using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

  public static GameController instance;
  private UIUpdater ui;
  public List<string> enemyList = new List<string>();

  public int health;
  public int wealth;


  private void Awake()
  {

    if (instance != null)
    {
      Destroy( gameObject );
      return;
    }
    instance = this;
    DontDestroyOnLoad( gameObject );
  }

  public void ApplyDamange( int dmg )
  {
    health -= dmg;
    UIUpdate();
  }

  public void ApplySpew( int value)
  {
    wealth -= value;
    UIUpdate();
  }

  public void ApplyPickup( int value )
  {
    wealth += value;
    UIUpdate();
  }

  public void LeftRoom( string type )
  {
    enemyList.Add( type );
  }

  public List<string> GetTallyEnemies()
  {
    return enemyList;
  }

  public void ClearTallyEnemies()
  {
    enemyList.Clear();
  }

  public void StartWave()
  {
    SceneManager.LoadScene("Sample");
  }

  public void WaveEnded()
  {
    SceneManager.LoadScene("Tally");
  }

  private void UIUpdate()
  {
    if (ui == null)
    {
      ui = GameObject.Find("UIUpdater").GetComponent<UIUpdater>();
    }

    if (ui != null)
    {
      ui.UpdateUI(health, wealth);
    }

  }


}
