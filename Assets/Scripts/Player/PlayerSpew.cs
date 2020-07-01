using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpew : MonoBehaviour
{
  public Camera cam;
  public GameObject[] items;
  public int level;
  public LayerMask bounds; // same as player!!!
  public GameController gameController;
  public GameObject burstEffect;
  public GameObject aim;
  private float aimDirection;
  // public PlayerStatus playerStatus;

  private void Start()
  {
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
    cam = Camera.main;
  }

  private void Update()
  {
    AimArrow();
  }

  public void Everywhere()
  {
    // check for bounds
    Vector2 newAim = new Vector2 ( ( aimDirection % 2 ) * Mathf.Sign( aimDirection - 2 ), ( ( 1 - aimDirection % 2 ) * Mathf.Sign( 1 - aimDirection ) ) );
    SpawnItem( newAim );

    // SpawnItem( Vector3.left );
    // SpawnItem( Vector3.up );
    // SpawnItem( Vector3.down );
  }

  private void AimArrow()
  {
    Vector3 mousePos = cam.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane) );
    float angle = ( Mathf.Atan2( mousePos.y - transform.position.y, mousePos.x - transform.position.x ) * Mathf.Rad2Deg ) - 45;
    angle = ( angle + 359 ) % 359;
    angle = Mathf.Floor( angle / 90 );
    aim.transform.rotation = Quaternion.Euler(0f, 0f, angle * 90 );
    aimDirection = angle;
  }

  private bool IsInBounds(Vector3 pos)
  {
    return !Physics2D.OverlapCircle(transform.position + pos / 2, 0.2f, bounds);
  }

  private void SpawnItem( Vector3 pos )
  {
    if ( IsInBounds( pos ) )
    {
      GameObject item = items[ UnityEngine.Random.Range(0, items.Length) ];
      Instantiate(item, transform.position + pos, Quaternion.identity);
      Instantiate(burstEffect, transform.position + pos, Quaternion.identity);
      gameController.ApplySpew(1);
      // playerStatus.Spew(1);
    }
  }



}
