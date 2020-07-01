using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

  public int value = 1;
  public int weight = 1;
  public Sprite[] sprites;
  private bool resting;
  private float theta;
  private int bounces;
  private float threshold;
  // private Vector2 start;
  private Vector3 end;
  public float moveSpeed = 8f;


  private void Start()
  {
    if ( sprites.Length > 0 )
    {
      Sprite s = sprites[ UnityEngine.Random.Range(0, sprites.Length) ];
      gameObject.transform.Find( "Sprite" ).gameObject.GetComponent<SpriteRenderer>().sprite = s;
    }
  }

  private void Update()
  {
    if ( !resting )
    {
      transform.position = Vector3.MoveTowards( transform.position, end, moveSpeed * Time.deltaTime );
      resting = IsResting();
    }
  }

  public bool IsResting()
  {
    return ( Vector3.Distance( transform.position, end ) <= 0.05f );
  }


  public void PickedUp()
  {
    Destroy(gameObject);
  }

  public void SetDirection( Vector3 pos )
  {
    end = transform.position + pos;
  }


}
