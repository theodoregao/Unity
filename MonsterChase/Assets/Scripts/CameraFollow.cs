using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
  [SerializeField]
  private float minX = -60, maxX = 60;

  private Transform player;
  private Vector3 tempPos;
  // Start is called before the first frame update
  void Start() {
    player = GameObject.FindWithTag("Player").transform;
  }

  // Update is called once per frame
  void LateUpdate() {
    if (player == null) {
      return;
    }

    tempPos = transform.position;
    tempPos.x = player.position.x;
    tempPos.x = Mathf.Clamp(tempPos.x, minX, maxX);
    transform.position = tempPos;
  }
}
