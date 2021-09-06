using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {
  private void OnTriggerEnter2D(Collider2D collider) {
    if (collider.CompareTag("Enermy") || collider.CompareTag("Player")) {
      Destroy(collider.gameObject);
    }
  }
}
