using UnityEngine;

public class TallyScene : MonoBehaviour
{
  public void ReturnToGame()
  {
    GameObject.Find("GameState").GetComponent<GameStateController>().StartGame();
  }
}
