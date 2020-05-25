using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{

  public enum PlayState
  {
    Title,
    Play,
    Tally,
    Level
  }


  private bool stateChanged = false;
  private PlayState stateCurrent;
  private PlayState stateNew;
  public static GameStateController instance;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy( gameObject );
      return;
    }

    DontDestroyOnLoad( gameObject );

  }

  private void Start()
  {
    int activeScene = SceneManager.GetActiveScene().buildIndex;

    switch(activeScene)
    {
      case 0:
        stateCurrent = PlayState.Title;
        break;
      case 1:
        stateCurrent = PlayState.Play;
        break;
      case 2:
        stateCurrent = PlayState.Tally;
        break;
    }
    // stateCurrent = PlayState.Play;
    stateNew = stateCurrent;
    // GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().SetScriptActive(true);
  }

  private void Update()
  {
    if (stateChanged)
    {
      // from
      switch(stateCurrent)
      {
        case PlayState.Title:
          break;
        case PlayState.Play:
          GameObject.Find("Player").GetComponent<PlayerMovement>().SetScriptActive(false);
          GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().SetScriptActive(false);
          break;
        case PlayState.Tally:
          break;
        case PlayState.Level:
          break;
      }

      // To
      switch(stateNew)
      {
        case PlayState.Title:
          break;
        case PlayState.Play:
          // GameObject.Find("Player").GetComponent<PlayerMovement>().SetScriptActive(true);
          // GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().SetScriptActive(true);
          break;
        case PlayState.Tally:
          GameObject.Find("Scenes").GetComponent<Scenes>().Tally();
          break;
        case PlayState.Level:
          break;

      }

      stateCurrent = stateNew;
      stateChanged = false;
    }
  }

  public void WaveEnded()
  {
    stateNew = PlayState.Tally;
    stateChanged = !(stateNew == stateCurrent);
  }

  public void StartGame()
  {
    stateNew = PlayState.Play;
    stateChanged = !(stateNew == stateCurrent);
    GameObject.Find("Scenes").GetComponent<Scenes>().PlayGame();
  }


}
