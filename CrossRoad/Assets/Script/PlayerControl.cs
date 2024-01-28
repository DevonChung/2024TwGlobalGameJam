using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour
{
    public float debug_center_y = 1.5f;
    public float moveSpeed = 5.0f;
    public float crashTime = 1.5f;
    protected int thousand_money_number = 0;

    public GameObject ACPrefab;
    public AudioManager audioManager;
    private Rigidbody2D rb;
    private BoxCollider2D collid;
    private Animator anim;
    private Tilemap tilemap;
    private Joystick jsMovement;
    private bool iscrash = false;
    private bool isChaoPie = false;
    private float ACComming = 0.0f; // over 10 sec will get a AC
    private string currentTile = "None";
    private bool isdrug = false;
    private bool isUFO = false;
    private bool isShoe = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collid = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        audioManager = AudioManager.instance;
        tilemap = GameObject.FindGameObjectWithTag("Special").GetComponent<Tilemap>();
        jsMovement = GameObject.FindGameObjectWithTag("Joystick").GetComponentInChildren<Joystick>();
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
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Car"), iscrash);
        CheckAC();
    }

    bool CanMoveNow()
    {
        if (MyGameManager.instance.currentState == MyGameManager.CrossRoadGameStatus.InGame)
        {
            return true;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
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

    void MoveInput()
    {
        Vector2 direction = Vector2.zero;

        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            // InputDirection can be used as per the need of your project
            direction = jsMovement.InputDirection;
        }
        else
        {
            float xVelocity = Input.GetAxisRaw("Horizontal");
            float yVelocity = Input.GetAxisRaw("Vertical");
            direction = new Vector2(xVelocity, yVelocity);
        }
    }
    void Move()
    {

        float xVelocity;
        float yVelocity;
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            // InputDirection can be used as per the need of your project
            xVelocity = jsMovement.InputDirection.x;
            yVelocity = jsMovement.InputDirection.y;
        }
        else
        {
            xVelocity = Input.GetAxisRaw("Horizontal");
            yVelocity = Input.GetAxisRaw("Vertical");
        }
        SetAnimationParam(xVelocity, yVelocity);
        if (isdrug)
        {
            xVelocity = -xVelocity;
            yVelocity = -yVelocity;
        }
        Vector2 direction = new Vector2(xVelocity, yVelocity);
        rb.velocity = direction.normalized * moveSpeed;
        if (iscrash)
        {
            rb.velocity *= 0.2f;
        }
        if (isUFO)
        {
            rb.velocity *= 0.5f;
        }
        if (isShoe)
        {
            rb.velocity *= 2.0f;
        }
    }
    string CheckStandOn()
    {
        RaycastHit2D hit;
        Vector2 rayOrigin = transform.position + new Vector3(0.0f, 0.5f); // 车子的中心位置
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
                if (tile != null)
                    return tile.name;
            }
        }
        return "None";
    }
    void CheckAC()
    {
        if (currentTile == "sidewalk")
        {
            ACComming += Time.deltaTime;
            if (ACComming > 10.0f)
            {
                ACFall();
                ACComming = 0.0f;
            }
        }
        else
        {
            if (ACComming > 0.0f)
                ACComming -= Time.deltaTime * 0.5f;
            else
                ACComming = 0.0f;
        }
    }
    void ACFall()
    {

        print("ACFall");
        MyGameManager.instance.myUIManager.SetText("人在路上走，冷氣從天降。");
        GameObject AC = Instantiate(ACPrefab, transform);
        AC.transform.localPosition = new Vector3(0, 9, -10);
        AC.transform.localScale = new Vector3(2, 2, 2);
        StartCoroutine(ACFalling(AC));


    }
    IEnumerator ACFalling(GameObject AC)
    {
        float duration = 2.0f;
        while (AC.transform.localPosition.y > 3.0f)
        {
            float dy = 6.0f / duration * Time.deltaTime;
            AC.transform.localPosition -= new Vector3(0, dy, 0);
            yield return new WaitForEndOfFrame();
        }
        Destroy(AC);
        AudioManager.instance.PlayAcAudio();
        // AudioManager.instance.PlayCrashAudio();
        MyGameManager.instance.AddMoney(-50);
        StartCoroutine(PlayerCrashed());
    }


    void AddMoney()
    {
        print("GetMoney");
        thousand_money_number++;
        MyGameManager.instance.AddMoney(1000);
        AudioManager.instance.PlayMoneyAudio();
        // TODO
    }
    IEnumerator ResetPlayer(string status, float duration)
    {
        print("ResetBuff");
        yield return new WaitForSeconds(duration);
        print("status: " + status);
        switch (status)
        {
            case "crash":
                iscrash = false;
                break;
            case "isdrug":
                isdrug = false;
                CharacterBuffUiManager.instance.DelStatusIcon(CharacterBuffUiManager.ExtraStatusType.Drug);
                break;
            case "isUFO":
                isUFO = false;
                CharacterBuffUiManager.instance.DelStatusIcon(CharacterBuffUiManager.ExtraStatusType.UFO);
                break;
            case "isShoe":
                isShoe = false;
                CharacterBuffUiManager.instance.DelStatusIcon(CharacterBuffUiManager.ExtraStatusType.Shoe);
                break;
            case "isChaoPie":
                isChaoPie = false;
                CharacterBuffUiManager.instance.DelStatusIcon(CharacterBuffUiManager.ExtraStatusType.Rice);
                break;
            default:
                break;
        }
    }
    void CarCrash()
    {
        print("CarCrash");
        MyGameManager.instance.AddMoney(-20);
        if (currentTile == "crosswalk1" || currentTile == "crosswalk2")
        {
            MyGameManager.instance.myUIManager.SetText("未禮讓行人，罰款最高可達6000元");
            MyGameManager.instance.AddMoney(+30);
        }
        // TODO
        iscrash = true;
        rb.velocity = Vector2.zero;
        anim.SetBool("Crash", true);
        audioManager.PlayCrashAudio();
        StartCoroutine(PlayerCrashed());
        StartCoroutine(ResetPlayer("crash", crashTime));
    }
    IEnumerator PlayerCrashed()
    {
        float time = 0.0f;
        while (time < crashTime)
        {
            // using sine function to make the sprite blink from 1 to 0.2
            float alpha = Mathf.Sin((time * 36.0f + 90.0f) / math.PI) * 0.4f + 0.6f;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    void drug()
    {
        print("drug");
        MyGameManager.instance.myUIManager.SetText("謀吧啦，拔嘎nono，拔say拔say");
        isdrug = true;
        CharacterBuffUiManager.instance.AddStatusIcon(CharacterBuffUiManager.ExtraStatusType.Drug);
        AudioManager.instance.PlayDrugAudio();
        StartCoroutine(ResetPlayer("isdrug", 5.0f));
    }
    void UFO()
    {
        print("UFO");
        MyGameManager.instance.myUIManager.SetText("欸！有飛碟！");
        isUFO = true;
        CharacterBuffUiManager.instance.AddStatusIcon(CharacterBuffUiManager.ExtraStatusType.UFO);
        AudioManager.instance.PlayUfoAudio();
        StartCoroutine(ResetPlayer("isUFO", 5.0f));
    }
    void Shoe()
    {
        print("Shoe");
        MyGameManager.instance.myUIManager.SetText("我的滑板鞋，時尚時尚最時尚，是魔鬼的步伐。");
        isShoe = true;
        CharacterBuffUiManager.instance.AddStatusIcon(CharacterBuffUiManager.ExtraStatusType.Shoe);
        StartCoroutine(ResetPlayer("isShoe", 5.0f));
    }
    void ChaoPie()
    {
        print("ChaoPie");
        MyGameManager.instance.myUIManager.SetText("超派！！！");
        isChaoPie = true;
        CharacterBuffUiManager.instance.AddStatusIcon(CharacterBuffUiManager.ExtraStatusType.Rice);
        StartCoroutine(ResetPlayer("isChaoPie", 5.0f));
    }
    IEnumerator flyAway(Vector2 direction, GameObject obj)
    {
        // move the object away (object with rigidbody)
        while (obj.transform.position.x > -10 && obj.transform.position.x < 10)
        {
            obj.transform.position += (Vector3)direction * 50.0f * Time.deltaTime;
            obj.transform.Rotate(0, 0, 10.0f);
            yield return new WaitForEndOfFrame();
        }
        Destroy(obj);
    }
    void ChaoPiePunch(GameObject obj)
    {
        // make the object fly away
        audioManager.PlayCrashAudio();
        obj.GetComponent<BoxCollider2D>().enabled = false;
        Vector2 direction = obj.transform.position - transform.position;
        StartCoroutine(flyAway(direction, obj));
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

    void OnCollisionEnter2D(Collision2D collision)
    {

        // Items
        switch (collision.gameObject.tag)
        {
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
            case "Shoe":
                Shoe();
                Destroy(collision.gameObject);
                return;
            case "ChaoPie":
                ChaoPie();
                Destroy(collision.gameObject);
                return;
            default:
                break;
        }
        if (isChaoPie)
        {
            ChaoPiePunch(collision.gameObject);
        }
        else
        {
            // Obstacles
            switch (collision.gameObject.tag)
            {
                case "Car":
                    MyGameManager.instance.myUIManager.SetText("走路不看路，亂穿馬路，你是猴子派來的嗎？");
                    CarCrash();
                    break;
                case "Monkey":
                    MyGameManager.instance.myUIManager.SetText("哪裡來的猴子啊！？");
                    CarCrash();
                    break;
                default:
                    break;
            }
        }
    }
}
