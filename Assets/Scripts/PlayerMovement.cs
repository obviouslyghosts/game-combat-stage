using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float moveSpeed = 5f;
  public Transform movePoint;

  public LayerMask whatStopsMovement;
  public Animator anim;

  private float attackWait = 0.4f;
  private float attackTimer= 0.4f;
  private bool hasInput = false;

  // Start is called before the first frame update
  void Start()
  {
    movePoint.parent = null;
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);


    if (Vector3.Distance(transform.position,movePoint.position) <= 0.05f)
    {
      if (attackTimer >= attackWait)
      {
        if (Mathf.Abs(Input.GetAxisRaw("Jump")) == 1f)
        {
          anim.SetTrigger("attack");
          attackTimer = 0f;
        }
      }
      else
      {
        attackTimer += Time.deltaTime;
      }

      if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f )
      {
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal")/2, 0f, 0f), 0.2f, whatStopsMovement))
        {
          movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
          hasInput = true;
        }
      }
      if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f )
      {
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical")/2, 0f), 0.4f, whatStopsMovement))
        {
          movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
          hasInput = true;
        }
      }
      anim.SetBool("moving", hasInput);
    } else
    {
      anim.SetBool("moving", true);
      hasInput = false;
    }


  }

}
