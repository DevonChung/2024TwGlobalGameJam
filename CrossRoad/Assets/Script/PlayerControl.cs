using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    protected int thousand_money_number = 0;

    public AudioManager audioManager;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator anim;
    private Tilemap tilemap;
    private bool movable = true;
    private bool isChaoPie = false;
    private float ACComming = 0.0f; // over 10 sec will get a AC
    private string currentTile = "None";
    private bool isdrug = false;
    private bool isUFO = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        audioManager = AudioManager.instance;
        tilemap = GameObject.FindGameObjectWithTag("Special").GetComponent<Tilemap>();
    }

    public int get_thousand_money_number()
    { return thousand_money_number; }

    public void ResetThousandMoneyNumber()
    {
        thousand_money_number = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanMoveNow())
            Move();
        currentTile = CheckStandOn();
        
    }

    bool CanMoveNow()
    {
        if (MyGameManager.instance.currentState == MyGameManager.CrossRoadGameStatus.InGame)
        {
            return movable;
        }
        else 
        {
            rb.velocity = new Vector2(0,0);
            return false;
        }
    }

    void SetAnimationParam(float xVelocity, float yVelocity)
    {
        if (yVelocity > 0)
        {
            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
            anim.SetBool("Idle", false);
        }
        else if (yVelocity < 0)
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
            anim.SetBool("Idle", false);
        }
        else if (xVelocity > 0)
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Left", false);
            anim.SetBool("Right", true);
            anim.SetBool("Idle", false);
        }
        else if (xVelocity < 0)
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Left", true);
            anim.SetBool("Right", false);
            anim.SetBool("Idle", false);
        }
      
    }

    void Move() {
        float xVelocity = Input.GetAxisRaw("Horizontal");
        float yVelocity = Input.GetAxisRaw("Vertical");
        SetAnimationParam(xVelocity, yVelocity);
         Vector2 direction = new Vector2(xVelocity, yVelocity);
        rb.velocity = direction.normalized * moveSpeed;
    }
    string CheckStandOn() {
        RaycastHit2D hit;
        Vector2 rayOrigin = transform.position; // 车子的中心位置
        Vector2 rayDirection = Vector2.up; // 假设射线向前
        int layerMask = LayerMask.GetMask("Special"); // 只包含 "Turn" 层的 LayerMask
        hit = Physics2D.Raycast(rayOrigin, rayDirection, 0.001f, layerMask);

        if (hit.collider != null)
        {
            print("hit" + hit.collider.gameObject.name);
            // 检测到的是 "Turn" 层上的对象
            if (hit.collider.gameObject.GetComponent<Tilemap>() != null)
            {
                Vector3Int cellPosition = tilemap.WorldToCell(hit.point);

                // 获取相应位置的 Tile
                TileBase tile = tilemap.GetTile(cellPosition);
                string tileType = "None"
                if (tile == null) return tileType;
                tileType = tile.name;
                return tileType;
            }
        }
    }
    void CheckAC() {
        if (currentTile == "sidewalk") {
            ACComming += Time.deltaTime;
            if (ACComming > 10.0f) {
                ACFall();
                ACComming = 0.0f;
            }
        }
        else {
            ACComming -= Time.deltaTime;
        }
    }
    void ACFall() {
        print("ACFall");
        GameObject AC = Instantiate(Resources.Load("Prefabs/AC")) as GameObject;
        AC.transform.position = transform.position;
        CarCrash();
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
        thousand_money_number++;
        MyGameManager.instance.AddMoney(1000);
        // TODO
    }
    void CarCrash() {
        print("CarCrash");
        MyGameManager.instance.AddMoney(-20);
        if (currentTile == "Crosswalk") {
            MyGameManager.instance.AddMoney(+30);
        }
        // TODO
        collider.isTrigger = true;
        movable = false;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Crash");
        audioManager.PlayCrashAudio();
        StartCoroutine(ResetPlayer());

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Endline":
                MyGameManager.instance.ReachEndLine();
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        switch (collision.gameObject.tag) {
            case "Money":
                AddMoney();
                 Destroy(collision.gameObject);
                break;
            case "Car":
                CarCrash();
                break;
            
            default:
                break;
        }
    }
}
