using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public bool isActive = true;
  public float moveSpeed = 4f;
  public Transform movePoint;
  public AudioManager audioManager;
  public LayerMask whatStopsMovement;
  public Animator anim;
  public PlayerSpew spew;
  public PlayerStatus status;
  public float attackWait = 0.2f;
  private float attackTimer; //= attackWait;
  // private bool hasInput = false;
  private bool canMove = true;
  // Start is called before the first frame update
  void Start()
  {
    movePoint.parent = null;
    attackTimer = attackWait;
    audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    status = gameObject.GetComponent<PlayerStatus>();
  }

  // Update is called once per frame
  void Update()
  {
    if (isActive)
    {
      transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);


      if (Vector3.Distance(transform.position,movePoint.position) <= 0.05f)
      {
        if (attackTimer >= attackWait)
        {
          if (Mathf.Abs(Input.GetAxisRaw("Jump")) == 1f)
          {
            anim.SetTrigger("attack");
            audioManager.Play("PlayerSpew");
            attackTimer = 0f;
            spew.Everywhere();
          }
        }
        else
        {
          attackTimer += Time.deltaTime;
        }

        if (canMove && Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f )
        {
          if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal")/2, 0f, 0f), 0.2f, whatStopsMovement))
          {
            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            anim.SetBool("moveHoriz", true);
            anim.SetBool("moveVert", false);
            audioManager.Play("PlayerMove");
            canMove = false;
          }
          else
          {
            anim.SetBool("moveHoriz", false);
          }
        }
        else if (canMove && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f )
        {
          if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical")/2, 0f), 0.4f, whatStopsMovement))
          {
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            anim.SetBool("moveVert", true);
            anim.SetBool("moveHoriz", false);
            audioManager.Play("PlayerMove");
            canMove = false;
          }
          else
          {
            anim.SetBool("moveVert", false);
          }
        }
        else
        {
          if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) != 1 & Mathf.Abs(Input.GetAxisRaw("Vertical")) !=1)
          {
            canMove = true;
          }

          anim.SetBool("moveHoriz", false);
          anim.SetBool("moveVert", false);
        }

        } else
        {

          // anim.SetBool("moving", true);
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
      status.Pickup(theLoot.value);
      theLoot.PickedUp();
    }
  }

  public void SetScriptActive(bool v)
  {
    isActive = v;
  }

}
