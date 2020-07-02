using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot_phys : MonoBehaviour
{

  public int value = 1;
  public int weight = 1;
  public Sprite[] sprites;
  private bool resting = false;
  private float theta;
  private int bounces;
  private float threshold;
  private Vector3 end;
  public float moveSpeed = 8f;
  private float restingTimer = 0f;
  private float restingAlarm = 1.5f;
  private Rigidbody2D rigid;


  private void Start()
  {
    if ( sprites.Length > 0 )
    {
      Sprite s = sprites[ UnityEngine.Random.Range(0, sprites.Length) ];
      gameObject.transform.Find( "Sprite" ).gameObject.GetComponent<SpriteRenderer>().sprite = s;
    }
    rigid = GetComponent<Rigidbody2D>();
    // resting = true;
  }

  private void Update()
  {
    // if ( !resting )
    // {
    //   transform.position = Vector3.MoveTowards( transform.position, end, moveSpeed * Time.deltaTime );
    //   resting = IsResting();
    // }
  }

  public bool IsResting()
  {
    return rigid.velocity.magnitude <= 0.5f;
  }

  public void Blast( Vector3 pos, float force )
  {
    Debug.Log( "Blasted!" );
    gameObject.GetComponent<Rigidbody2D>().AddForce( pos * force, ForceMode2D.Impulse );
  }


  public void PickedUp()
  {
    Destroy(gameObject);
  }

  public void SetDirection( Vector3 pos )
  {
    end = transform.position + pos;
    resting = IsResting();
  }


}
