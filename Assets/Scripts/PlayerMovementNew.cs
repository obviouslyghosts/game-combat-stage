using System.Collections;
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
  // private Vector2 inputOne = new Vector2( 0f, 0f );
  // private Vector2 inputTwo = new Vector2( 0f, 0f );
  // private Vector2 inputThree = new Vector2( 0f, 0f );
  public float threshold = 0.05f;
  // NOTES
  // Last input overrides new input


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
        if ( triggerMove &
          ( Mathf.Abs( Input.GetAxis("Horizontal") ) >= threshold
          | Mathf.Abs( Input.GetAxis( "Vertical" ) ) >= threshold
          | Mathf.Abs( Input.GetAxis( "DPad X" ) ) >= threshold
          | Mathf.Abs( Input.GetAxis( "DPad Y" ) ) >= threshold
          ) ) // triggerMove &&
        {

          // Move( Input.GetAxis( "Horizontal" ) , Input.GetAxis( "Vertical" ) );
          Move( Input.GetAxis( "Horizontal" ) + Input.GetAxis( "DPad X" ), Input.GetAxis( "Vertical" ) + Input.GetAxis( "DPad Y" ) );
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

  private void Move( float x, float y )
  {
    float _x = Mathf.Abs( x );
    float _y = Mathf.Abs( y );


    if ( ( _x > 0 ) | ( _y > 0 ) )
    {

      if ( _x * _y > 0)
      {
        // Zero out axis
        x = ( _x >= _y ) ?  x : 0f;
        y = ( _y > _x ) ? y : 0f;
      }

      Debug.Log( _x + " " + _y);
      x = ClampInput( x );
      y = ClampInput( y );

      Debug.Log( x + " " + y);

      x = SimpleCollisionCheck( x, 0f ) ? 0f : x;
      y = SimpleCollisionCheck( 0f, y ) ? 0f : y;

      movePoint.position += new Vector3( x , y, 0f );
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

  private float ClampInput( float i )
  {
    return ( Mathf.Abs( i ) >= threshold ? 1 * Mathf.Sign( i ) : 0f );
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
