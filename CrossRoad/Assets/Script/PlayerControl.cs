using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public AudioManager audioManager;
    public int NotMyMoney = 0;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator anim;
    private bool movable = true;
    private bool isdrug = false;
    private bool isUFO = false;
    private bool isChaoPie = false;
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
        if(isdrug) {
            xVelocity = -xVelocity;
            yVelocity = -yVelocity;
        }

        Vector2 direction = new Vector2(xVelocity, yVelocity);
        rb.velocity = direction.normalized * moveSpeed;
        if (isUFO) {
            rb.velocity *= 0.5f;
        }
    }
    IEnumerator ResetPlayer() {
        // wait until the animation is finished
        print(anim.GetCurrentAnimatorStateInfo(0));
        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);
        collider.isTrigger = false;
        movable = true;
    }
    IEnumerator ResetBuff(string status, float duration) {
        print("ResetBuff");
        yield return new WaitForSeconds(duration);
        print("status: " + status);
        switch (status) {
            case "isdrug":
                isdrug = false;
                break;
            case "isUFO":
                isUFO = false;
                break;
            case "isChaoPie":
                isChaoPie = false;
                break;
            default:
                break;
        }
    }
    void ClearNotMyMoney() {
        print("ClearMoney");
        NotMyMoney = 0;
    }
    void AddMoney() {
        print("GetMoney");
        NotMyMoney += 1000;
        MyGameManager.instance.AddMoney(1000);
    }
    void CarCrash() {
        print("CarCrash");
        collider.isTrigger = true;
        movable = false;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Crash");
        audioManager.PlayCrashAudio();
        StartCoroutine(ResetPlayer());
    }

    void drug() {
        print("drug");
        isdrug = true;
        StartCoroutine(ResetBuff("isdrug", 5.0f));
    }
    void UFO() {
        print("UFO");
        isUFO = true;
        StartCoroutine(ResetBuff("isUFO", 5.0f));
    }
    void ChaoPie() {
        print("ChaoPie");
        isChaoPie = true;
        StartCoroutine(ResetBuff("isChaoPie", 5.0f));
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
        // Items
        switch (collision.gameObject.tag) {
            case "Money":
                AddMoney();
                Destroy(collision.gameObject);
                return;
            case "Drug":
                drug();
                Destroy(collision.gameObject);
                return;
            case "UFO":
                UFO();
                Destroy(collision.gameObject);
                return;
            case "ChaoPie":
                ChaoPie();
                Destroy(collision.gameObject);
                return;
            default:
                break;
        }
        if (isChaoPie) {
            ChaoPiePunch(collision.gameObject);
        }
        else {
            // Obstacles
            switch (collision.gameObject.tag) {
                case "Car":
                    CarCrash();
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator flyAway(Vector2 direction, GameObject obj) {
        // move the object away (object with rigidbody)
        while (obj.transform.position.x > -10 && obj.transform.position.x < 10) {
            obj.transform.position += (Vector3)direction * 50.0f * Time.deltaTime;
            obj.transform.Rotate(0, 0, 10.0f);
            yield return new WaitForEndOfFrame();
        }
        Destroy(obj);
    }
    void ChaoPiePunch(GameObject obj) {
        // make the object fly away
        obj.GetComponent<BoxCollider2D>().isTrigger = true;
        Vector2 direction = obj.transform.position - transform.position;
        StartCoroutine(flyAway(direction, obj));
    }
}
