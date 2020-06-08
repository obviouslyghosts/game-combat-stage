using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{

  public void StartWave()
  {
    GameObject.Find("GameController").GetComponent<GameController>().StartWave();
  }

  public void WaveEnded()
  {
    GameObject.Find("GameController").GetComponent<GameController>().WaveEnded();
  }

  public void EndTally()
  {
    int w = GameObject.Find( "TallySpawner" ).GetComponent<TallySpawner>().GetAddedWealth();
    GameObject.Find("GameController").GetComponent<GameController>().AddTallyWealth( w );
    StartWave();
  }

  private void Update()
  {
    if ( Input.GetKeyDown( "space" ) )
    {
      string scene = SceneManager.GetActiveScene().name;
      if ( scene == "Tally" )
      {
        // Debug.Log( "Add this wealth" );
        EndTally();
      }
    }
  }


}
