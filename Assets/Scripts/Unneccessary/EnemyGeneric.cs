using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneric : MonoBehaviour
{
  public EnemyMovement movement;
  public EnemyBackpack backpack;
  public EnemyAnim anim;

  public string currentSearch;
  private string newSearch;
  private string[] searchList = {"Loot", "Player", "SpawnPoint" };
  private string staticSearchList = "Loot SpawnPoint";
  private string searchListIgnore = "Loot SpawnPoint";

  private void Start()
  {
    newSearch = "Loot";
    currentSearch = newSearch;
  }

  private void Update()
  {
    newSearch = QuickSearch();

    if ( newSearch != currentSearch )
    {
      currentSearch = newSearch;
      movement.GoalIsStationary( staticSearchList.Contains( currentSearch ) );
    }
    movement.RunTowardsNearest( currentSearch );
  }

  private void OnTriggerEnter2D( Collider2D other )
  {
    Debug.Log( "Triggered" );
    if ( other.gameObject.tag =="Loot" )
    {
      Debug.Log( "pickup loot!!!" );
      backpack.Add( other.gameObject );
    }
  }

  private string QuickSearch()
  {
    if ( newSearch == "SpawnPoint" )
    {
      return newSearch;
    }

    foreach ( string tag in searchList )
    {
      if ( GameObject.FindGameObjectsWithTag( tag ).Length > 0 )
      return tag;
    }
    return currentSearch;
  }

  public void UpdateMovement(string direction)
  {
    anim.UpdateMovement( direction );
  }

  public void TriggerEncumbered()
  {
    Debug.Log("Encumbered");
    newSearch = "SpawnPoint";
  }

  public void TriggerNotFound( string tag )
  {
    Debug.Log("Tag Not Found");

  }



}
