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


}
