using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  private static string WALK_ANIMATION = "Walk";
  private static string GROUND_TAG = "Ground";
  private static string ENERMY_TAG = "Enermy";

  [SerializeField] private float moveForce = 10.0f;
  [SerializeField] private float jumpForce = 11.0f;

  private float movementX;
  private bool isOnGround;

  private Rigidbody2D playerRigidbody;
  private SpriteRenderer spriteRenderer;
  private Animator anim;

  private void Awake() {
    playerRigidbody = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();
  }

  void Start() {

  }

  void Update() {
    PlayerMove();
    AnimatePlayer();
    PlayerJump();
  }

  private void FixedUpdate() {
  }

  void PlayerMove() {
    movementX = Input.GetAxisRaw("Horizontal");
    transform.position += new Vector3(movementX, 0.0f, 0.0f) * Time.deltaTime * moveForce;
  }

  void AnimatePlayer() {
    if (movementX > 0) {
      spriteRenderer.flipX = false;
      anim.SetBool(WALK_ANIMATION, true);
    } else if (movementX < 0) {
      spriteRenderer.flipX = true;
      anim.SetBool(WALK_ANIMATION, true);
    } else {
      anim.SetBool(WALK_ANIMATION, false);
    }
  }

  void PlayerJump() {
    if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0) && isOnGround) {
      isOnGround = false;
      playerRigidbody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
    }
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.CompareTag(GROUND_TAG)) {
      isOnGround = true;
    }
    if (collision.gameObject.CompareTag(ENERMY_TAG)) {
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D collider) {
    if (collider.CompareTag(ENERMY_TAG)) {
      Destroy(gameObject);
    }
  }
}
