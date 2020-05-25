using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{

  public void PlayGame()
  {
    SceneManager.LoadScene("Sample");
  }

  public void Tally()
  {
    SceneManager.LoadScene("Tally");
  }


}
