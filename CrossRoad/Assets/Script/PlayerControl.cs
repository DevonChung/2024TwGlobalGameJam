using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public AudioManager audioManager;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator anim;
    private bool movable = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        audioManager = AudioManager.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movable)
            Move();
    }
    void Move() {
        float xVelocity = Input.GetAxisRaw("Horizontal");
        float yVelocity = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(xVelocity, yVelocity);
        rb.velocity = direction.normalized * moveSpeed;
    }
    IEnumerator ResetPlayer() {
        // wait until the animation is finished
        print(anim.GetCurrentAnimatorStateInfo(0));
        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);
        collider.isTrigger = false;
        movable = true;
    }
    void AddMoney() {
        print("GetMoney");
        // TODO
    }
    void CarCrash() {
        print("CarCrash");
        // TODO
        collider.isTrigger = true;
        movable = false;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Crash");
        audioManager.PlayCrashAudio();
        StartCoroutine(ResetPlayer());

    }
    void OnCollisionEnter2D(Collision2D collision) {
        switch (collision.gameObject.tag) {
            case "Money":
                AddMoney();
                // Destroy(collision.gameObject);
                break;
            case "Car":
                CarCrash();
                break;

            default:
                break;
        }
    }
}
