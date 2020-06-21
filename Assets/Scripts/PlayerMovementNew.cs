﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNew : MonoBehaviour
{
  public bool isActive = true;
  public float moveSpeed = 4f;
  public Transform movePoint;
  public AudioManager audioManager;
  public LayerMask collisionLayer;
  public Animator anim;
  public PlayerSpew spew;
  public GameController gameController;
  public float alarmAttack = 0.2f;
  private float timerAttack;
  private bool triggerAttack = false;
  public float alarmMove = 0.5f;
  private float timerMove = 0f;
  private bool triggerMove = false;
  private bool atDestination = true;

  void Start()
  {
    movePoint.parent = null;
    timerAttack = alarmAttack;
    audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
  }

  void Update()
  {
    if ( isActive )
    {
      UpdateTimers();
      if ( atDestination ) // move towards Point
      {
        if ( triggerAttack && Mathf.Abs( Input.GetAxisRaw( "Jump" ) ) > 0f )
        {
          Attack( );
        }
        if ( triggerMove && Input.anyKey )
        {
          Move( Input.GetAxisRaw( "Horizontal" ), Input.GetAxisRaw( "Vertical" ) );
          atDestination = AtDestinationCheck();
        }
        if ( atDestination )
        {
          UpdateAnim( 0f, 0f );
        }
      }
      else
      {
        atDestination = MoveTowardsPoint();
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("Triggered");
    if (other.gameObject.tag =="Loot")
    {
      Debug.Log("pickup loot!!!");
      Loot theLoot = other.GetComponent(typeof(Loot)) as Loot;
      gameController.ApplyPickup(theLoot.value);
      theLoot.PickedUp();
    }
  }

  private bool MoveTowardsPoint()
  {
    transform.position = Vector3.MoveTowards( transform.position, movePoint.position, moveSpeed * Time.deltaTime );
    return ( AtDestinationCheck() );
  }

  private bool AtDestinationCheck()
  {
    return ( Vector3.Distance( transform.position, movePoint.position ) <= 0.05f );
  }

  private void Move( float x, float y)
  {
    if ( ( Mathf.Abs( x ) > 0 ) | ( Mathf.Abs( y ) > 0 ) )
    {
      x = SimpleCollisionCheck( x, 0f ) ? 0f : x;
      y = SimpleCollisionCheck( 0f, y ) ? 0f : y;

      Vector3 newPos = new Vector3( x , y, 0f );

      movePoint.position += newPos;
      UpdateAnim( x, y);
      UpdateAudio( x, y);
      timerMove = 0f;
      triggerMove = false;
    }
  }

  private void Attack( )
  {
    Debug.Log( "Triggered Attack" );
    anim.SetTrigger("attack");
    audioManager.Play("PlayerSpew");
    spew.Everywhere();
    timerAttack = 0f;
    triggerAttack = false;
  }

  private void UpdateTimers( )
  {
    if ( !triggerAttack )
    {
      timerAttack += Time.deltaTime;
      triggerAttack = ( timerAttack >= alarmAttack );
    }

    if ( !triggerMove )
    {
      timerMove += Time.deltaTime;
      triggerMove = ( timerMove >= alarmMove );
    }
  }

  private void UpdateAnim( float x, float y )
  {
    anim.SetBool("moveHoriz", ( Mathf.Abs(x) > 0 ) );
    anim.SetBool("moveVert", ( Mathf.Abs(y) > 0 ) );
  }

  private void UpdateAudio( float x, float y )
  {
    if ( Mathf.Abs( x ) > 0 | Mathf.Abs( y ) > 0 )
    {
      audioManager.Play("PlayerMove");
    }
  }

  private bool SimpleCollisionCheck( float x, float y )
  {
    return ( Physics2D.OverlapCircle( movePoint.position + new Vector3( x / 2 , y / 2, 0f ), 0.4f, collisionLayer ) );
  }

  public void SetScriptActive(bool v)
  {
    isActive = v;
  }

}
