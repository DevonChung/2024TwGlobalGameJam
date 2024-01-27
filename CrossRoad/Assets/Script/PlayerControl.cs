using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public AudioManager audioManager;
    public int NotMyMoney = 0;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator anim;
    private Tilemap tilemap;
    private bool movable = true;
    private bool isdrug = false;
    private bool isUFO = false;
    private bool isChaoPie = false;
    private float ACComming = 0.0f; // over 10 sec will get a AC
    private string currentTile = "None";
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        audioManager = AudioManager.instance;
        tilemap = GameObject.FindGameObjectWithTag("Special").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movable)
            Move();
        currentTile = CheckStandOn();
        
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
        MyGameManager.instance.AddMoney(-20);
        if (currentTile == "Crosswalk") {
            MyGameManager.instance.AddMoney(+30);
        }
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
