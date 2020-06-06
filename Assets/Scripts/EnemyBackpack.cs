using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackpack : MonoBehaviour
{
  // private GameObject[] backpack;
  private List<GameObject> backpack = new List<GameObject>();
  private int value;
  private int weight;
  private int carryCapacity = 5;



  public void Add( GameObject item)
  {
    backpack.Add( item );
    // value += item.GetComponent<Loot>
    Loot theLoot = item.GetComponent( typeof( Loot )) as Loot;
    value += theLoot.value;
    weight += theLoot.weight;
    theLoot.PickedUp();

    CheckWeight();
  }

  private void CheckWeight()
  {
    if ( weight >= carryCapacity )
    {
      gameObject.GetComponent<EnemyGeneric>().TriggerEncumbered();
    }

  }


}
