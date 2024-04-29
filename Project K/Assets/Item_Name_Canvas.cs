using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Item_Name_Canvas : MonoBehaviour
{
  private Canvas itemCanvas;
  private GameObject player;
  private Collider []_collider;
  

  private void Start()
  {
    player = gameObject;
    itemCanvas = gameObject.GetComponent<Canvas>();
    transform.localPosition = Vector3.zero;
  }

  private void Update()
  {
    FindObjects();
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawSphere(transform.position, 2);
  }

  void FindObjects()
  {
    _collider = Physics.OverlapSphere(transform.position, 2);
    foreach (var hit in _collider)
    {
      if (!hit)
        continue;

      if (hit.transform.CompareTag("Strength") || hit.transform.CompareTag("Flavor") ||
          hit.transform.CompareTag("BaseFlavor"))
      {
        itemCanvas.transform.position = Vector3.zero;
        var itemName = hit.gameObject.GetComponent<Item>().GetName();
        var textUI = Instantiate(gameObject, transform.position, quaternion.identity, transform.parent);
        var text = textUI.AddComponent<TextMeshProUGUI>();
        text.text = itemName;
      }
      else
      {
        itemCanvas.transform.position = new Vector3(-5, -5, -5);
      }
    }
  }
 
   
  
  
}
