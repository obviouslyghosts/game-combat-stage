using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  public GameController gameController;
  public AudioManager audioManager;
  public LayerMask collisionLayer;
  public Animator anim;
  public PlayerSpew spew;

  public float force;
  public float moveSpeed = 0.5f;
  public float alarmAttack = 0.2f;
  private float timerAttack;
  private bool triggerAttack = false;
  public float threshold = 0.15f;
  // private Rigidbody2D rigid;
  // Get input
  // move based on that input, NO GRID

  private void Start()
  {
    audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
    // rigid = gameObject.GetComponent<Rigidbody2D>();
  }


  private void Update()
  {
    Debug.Log( "Horiz: " +  Input.GetAxis( "Horizontal" ) );
    UpdateTimers();
    if ( triggerAttack &&
    ( Mathf.Abs( Input.GetAxisRaw( "Jump" ) ) > 0f
    | Input.GetMouseButton( 0 ) ) )
    {
      Attack( );
    }
    if ( ( Mathf.Abs( Input.GetAxis("Horizontal") ) >= threshold
      | Mathf.Abs( Input.GetAxis( "Vertical" ) ) >= threshold
      | Mathf.Abs( Input.GetAxis( "DPad X" ) ) >= threshold
      | Mathf.Abs( Input.GetAxis( "DPad Y" ) ) >= threshold
      ) ) // triggerMove &&
    {
      // Move( Input.GetAxis( "Horizontal" ) , Input.GetAxis( "Vertical" ) );
      Move( Input.GetAxis( "Horizontal" ) + Input.GetAxis( "DPad X" ), Input.GetAxis( "Vertical" ) + Input.GetAxis( "DPad Y" ) );
    }
    else
    {
      UpdateAnim( 0, 0);
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

  private void Move( float x, float y )
  {
    // x = ClampInput( x ) * moveSpeed;
    // y = ClampInput( y ) * moveSpeed;
    //
    x *= moveSpeed;
    y *= moveSpeed;

    x = SimpleCollisionCheck( x, 0f ) ? 0f : x;
    y = SimpleCollisionCheck( 0f, y ) ? 0f : y;

    Debug.Log( x );
    // rigid.AddForce( new Vector3( x , y, 0f ), ForceMode2D.Force );
    transform.position += new Vector3( x , y, 0f );
    UpdateAnim( x, y);
    UpdateAudio( x, y);
  }

  private void UpdateTimers( )
  {
    if ( !triggerAttack )
    {
      timerAttack += Time.deltaTime;
      triggerAttack = ( timerAttack >= alarmAttack );
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

  private float ClampInput( float i )
  {
    return ( Mathf.Abs( i ) >= threshold ? 1 * Mathf.Sign( i ) : 0f );
  }

  private bool SimpleCollisionCheck( float x, float y )
  {
    return ( Physics2D.OverlapCircle( transform.position + new Vector3( x, y, 0f ), 0.4f, collisionLayer ) );
  }


}
