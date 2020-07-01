using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSmooth : MonoBehaviour
{
  public bool isActive = true;
  public float moveSpeed = 4f;
  public Transform movePoint;
  public Transform lastPoint;
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
  private int spacesMoved = 0;
  // private bool
  public float threshold = 0.05f;

  void Start()
  {
    movePoint.parent = null;
    lastPoint.parent = null;
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
        if ( triggerAttack &&
        ( Mathf.Abs( Input.GetAxisRaw( "Jump" ) ) > 0f
        | Input.GetMouseButton( 0 ) ) )
        {
          Attack( );
          timerMove = 0;
          triggerMove = false;

        }
        if ( triggerMove &
          ( Mathf.Abs( Input.GetAxis("Horizontal") ) >= threshold
          | Mathf.Abs( Input.GetAxis( "Vertical" ) ) >= threshold
          | Mathf.Abs( Input.GetAxis( "DPad X" ) ) >= threshold
          | Mathf.Abs( Input.GetAxis( "DPad Y" ) ) >= threshold
          ) ) // triggerMove &&
        {
          spacesMoved += 1;
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
        // check if key up
        if (
          ( Mathf.Abs( Input.GetAxis("Horizontal") ) <= threshold
          & Mathf.Abs( Input.GetAxis( "Vertical" ) ) <= threshold
          & Mathf.Abs( Input.GetAxis( "DPad X" ) ) <= threshold
          & Mathf.Abs( Input.GetAxis( "DPad Y" ) ) <= threshold
          ) ) // triggerMove &&
        {
          Debug.Log("Button Up");
          if ( spacesMoved >= 2 )
          {
            float distLeft = Vector3.Distance( transform.position, movePoint.position );
            float distHome  = Vector3.Distance( transform.position, lastPoint.position );

            if ( distHome <= distLeft )
            {
              movePoint.position = new Vector3( lastPoint.position.x , lastPoint.position.y, 0f );
            }
          }
          spacesMoved = 0;
        }

        atDestination = MoveTowardsPoint();

        if ( atDestination )
        {
          lastPoint.position = new Vector3( movePoint.position.x , movePoint.position.  y, 0f );
        }

      }
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("Triggered");
    if (other.gameObject.tag =="Loot")
    {
      Loot theLoot = other.GetComponent(typeof(Loot)) as Loot;
      if ( theLoot.IsResting() )
      {
        gameController.ApplyPickup(theLoot.value);
        theLoot.PickedUp();
      }
      // Debug.Log("pickup loot!!!");
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
      x = ClampInput( x );
      y = ClampInput( y );

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
