using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public AudioManager audioManager;
  public GameController gameController;
  public float brainSpeed = 1f;
  public float moveSpeed = 4f;
  public float moveAlarm = 0.6f;
  public float attackAlarm = 0.6f;
  public Transform movePoint;
  public Transform goalPoint;
  public int backpackSize;
  public Animator anim;
  public string type = "knight";

  public EnemyAttack attackHoriz;
  public EnemyAttack attackVert;
  private int level = 1;
  private int backpack = 0;
  private float moveTimer = 0;
  private float attackTimer = 0;

  void Start()
  {
    movePoint.parent = null;
    goalPoint.parent = null;
    moveAlarm = UnityEngine.Random.Range(0.2f,0.7f);
    backpackSize = UnityEngine.Random.Range(1,3);
    audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    gameController = GameObject.Find("GameController").GetComponent<GameController>();

  }

  void Update()
  {
    if ( AttackCheck() )
    {
      if (MoveTimerCheck())
      {
        UpdateMoveTowardsPoint();
        Debug.Log("Moving Point");
      }
      StepTowardsPoint();
    }

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("Triggered");
    if (other.gameObject.tag =="Loot")
    {
      Debug.Log("pickup loot!!!");
      Loot_phys theLoot = other.GetComponent( typeof( Loot_phys ) ) as Loot_phys;
      backpack+= theLoot.value;
      theLoot.PickedUp();
    }
  }

  private bool MoveTimerCheck()
  {
    if (moveTimer >= moveAlarm)
    {
      moveTimer = 0;
      return true;
    }
    moveTimer+= Time.deltaTime;
    return false;
  }

  private void UpdateMoveTowardsPoint()
  {
    // check for colissions!!!

    if (backpack >= backpackSize)
    {
      // move towards closest spawn point
      // Am I already on the point?
      Vector3 point = FindNearest("SpawnPoint");
      if (Vector3.Distance(transform.position, point) <= 0.5f)
      {
        GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().LeftRoom();
        gameController.LeftRoom( type );
        Destroy( goalPoint.gameObject );
        Destroy( movePoint.gameObject );
        Destroy( gameObject );
      }
      else
      {
        SimpleMove( point );
      }
      return;
    }

    if (GameObject.FindGameObjectsWithTag("Loot").Length > 0)
    {
      SimpleMove( FindNearest("Loot") );
    }
    else
    {
      SimpleMove( FindNearest("Player") );
    }
  }

  private bool AttackCheck()
  {
    if (attackHoriz.isAttacking)
    {
      if (AttackTimer() )
      {
        anim.SetTrigger("attack-Horiz");
        audioManager.Play("KnightAttack");
        gameController.ApplyDamange(1);
        // GameObject.FindWithTag("Player").GetComponent<PlayerStatus>().Attacked(1);
      }
      return false;
    }
    if (attackVert.isAttacking)
    {
      if (AttackTimer())
      {
        anim.SetTrigger("attack-Vert");
        audioManager.Play("KnightAttack");
        gameController.ApplyDamange(1);
        // GameObject.FindWithTag("Player").GetComponent<PlayerStatus>().Attacked(1);
      }
      return false;
    }
    return true;
  }

  private bool AttackTimer()
  {
    if (attackTimer >= attackAlarm)
    {
      attackTimer = 0;
      return true;
    }
    attackTimer+= Time.deltaTime;
    return false;
  }

  private Vector3 FindNearest(string tag)
  {
    float nearestItem = 1000f;
    Vector3 newTargetVector = Vector3.zero;
    GameObject[] searches = GameObject.FindGameObjectsWithTag(tag);

    if (searches.Length > 0)
    {
      foreach (GameObject item in searches)
      {
        float d = Vector3.Distance(transform.position, item.transform.position);
        if (d <= nearestItem)
        {
          nearestItem = d;
          newTargetVector = item.transform.position;
          goalPoint.position = item.transform.position;
        }
      }
      return newTargetVector;
    }
    return Vector3.zero;
  }

  private void SimpleMove(Vector3 target)
  {
    int _x = 0;
    int _y = 0;

    if ( Mathf.Abs(transform.position.x - target.x) >= 0.5f)
    {
      // move 1 unit horizontaly closer
      _x = transform.position.x > target.x ? -1 : 1;
      transform.localScale = new Vector3( (float)_x,1f,1f);
      anim.SetBool("move-Horiz", true);
      anim.SetBool("move-Vert", false);
      audioManager.Play("KnightMove");
    }
    if (Mathf.Abs(transform.position.y - target.y) >= 0.5f)
    {
      // move 1 unit vertically closer
      _y = transform.position.y > target.y ? -1 : 1;
      anim.SetBool("move-Vert", true);
      anim.SetBool("move-Horiz", false);
      audioManager.Play("KnightMove");
    }

    movePoint.position += new Vector3((float)_x,(float)_y,0f);
  }

  private void StepTowardsPoint()
  {
    if (Vector3.Distance(transform.position,movePoint.position) >= 0.01f)
    {
      transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
      Debug.Log("Moving towards point");
    }
    else
    {
      anim.SetBool("move-Horiz", false);
      anim.SetBool("move-Vert", false);
      Debug.Log("Arrived at point");
    }

  }


}
