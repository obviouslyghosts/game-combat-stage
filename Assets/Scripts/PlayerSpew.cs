using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpew : MonoBehaviour
{
  public GameObject[] items;
  public int level;
  public LayerMask bounds; // same as player!!!
  public GameController gameController;
  // public PlayerStatus playerStatus;

  private void Start()
  {
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
  }

  public void Everywhere()
  {
    // check for bounds
    SpawnItem(Vector3.left);
    SpawnItem(Vector3.right);
    SpawnItem(Vector3.up);
    SpawnItem(Vector3.down);
  }

  private bool IsInBounds(Vector3 pos)
  {
    return !Physics2D.OverlapCircle(transform.position + pos / 2, 0.2f, bounds);
  }

  private void SpawnItem(Vector3 pos)
  {
    if (IsInBounds(pos))
    {
      GameObject item = items[ UnityEngine.Random.Range(0, items.Length) ];
      Instantiate(item, transform.position + pos, Quaternion.identity);
      gameController.ApplySpew(1);
      // playerStatus.Spew(1);
    }
  }



}
