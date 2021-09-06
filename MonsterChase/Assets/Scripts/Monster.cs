using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
  [HideInInspector]
  public float speed;

  private Rigidbody2D monsterRigidbody;

  void Awake() {
    monsterRigidbody = GetComponent<Rigidbody2D>();
  }

  void FixedUpdate() {
    monsterRigidbody.velocity = new Vector2(speed, monsterRigidbody.velocity.y);
  }
}
