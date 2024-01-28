using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class CarControl : MonoBehaviour
{

    public float moveSpeed = 3.0f;
    public float maxMoveSpeed = 5.0f;
    public float minMoveSpeed = 1.0f;
    public float meanMoveSpeed = 2.0f;
    public float sigmaMoveSpeed = 5.0f;
    public float resetSpeedTime = 3.0f;
    public float maxResetSpeedTime = 6.0f;
    public float minResetSpeedTime = 0.0f;
    public float meanResetSpeedTime = 3.0f;
    public float sigmaResetSpeedTime = 5.0f;
    public float liveTime = 10.0f;
    public Vector3 direction = Vector3.right;
    public Vector3 changeLaneDirection = Vector3.up;
    private bool isTurning = false;
    private bool isChangingLane = false;
    public Tilemap tilemap;

    // Update is called once per frame
    void Start()
    {
        moveSpeed = RandomGaussian(minMoveSpeed, maxMoveSpeed, meanMoveSpeed, sigmaMoveSpeed);
        resetSpeedTime = RandomGaussian(minResetSpeedTime, maxResetSpeedTime, meanResetSpeedTime, sigmaResetSpeedTime);
        tilemap = GameObject.FindGameObjectWithTag("Turn").GetComponent<Tilemap>();
    }
    void FixedUpdate()
    {
        if (resetSpeedTime > 0)
        {
            resetSpeedTime -= Time.deltaTime;
        }
        else
        {
            if (!isTurning && !isChangingLane) {
                moveSpeed = RandomGaussian(minMoveSpeed, maxMoveSpeed, meanMoveSpeed, sigmaMoveSpeed);
                resetSpeedTime = RandomGaussian(minResetSpeedTime, maxResetSpeedTime, meanResetSpeedTime, sigmaResetSpeedTime);

            }
        }
        CheckTurn();
        Move();

    }
    void CheckTurn()
    {
        // Check road if there is a turn
        // If there is a turn, turn the car

        RaycastHit2D hit;
        Vector2 rayOrigin = transform.position; // 车子的中心位置
        Vector2 rayDirection = Vector2.right; // 假设射线向前
        int layerMask = LayerMask.GetMask("Turn"); // 只包含 "Turn" 层的 LayerMask
        hit = Physics2D.Raycast(rayOrigin, rayDirection, 0.001f, layerMask);

        if (hit.collider != null)
        {
            print("hit" + hit.collider.gameObject.name);
            // 检测到的是 "Turn" 层上的对象
            if (hit.collider.gameObject.GetComponent<Tilemap>() != null)
            {
                Vector3Int cellPosition = tilemap.WorldToCell(hit.point);

                TileBase tile = tilemap.GetTile(cellPosition);
                if (tile == null) return;
                string tileType = tile.name;
                print(gameObject.name + " hit " + tileType);
                switch (tileType)
                {
                    case "1": // up
                        if (!isTurning)
                            StartCoroutine(Turn(Vector3.up));
                        // direction = Vector3.up;
                        break;
                    case "2": // right
                        if (!isTurning)
                        StartCoroutine(Turn(Vector3.right));
                        // direction = Vector3.down;
                        break;
                    case "3": // down
                        if (!isTurning)
                        StartCoroutine(Turn(Vector3.down));
                        // direction = Vector3.left;
                        break;
                    case "4": // left
                        if (!isTurning)
                        StartCoroutine(Turn(Vector3.left));
                        // direction = Vector3.right;
                        break;
                    case "5": // left or right
                    case "7":
                        if (Random.Range(0, 2) == 0)
                            if (!isTurning)
                            StartCoroutine(Turn(Vector3.left));
                            // direction = Vector3.left;
                        else
                            if (!isTurning)
                            StartCoroutine(Turn(Vector3.right));
                            // direction = Vector3.right;
                        break;
                    case "6": // up or down
                    case "8":
                        if (Random.Range(0, 2) == 0)
                            if (!isTurning)
                            StartCoroutine(Turn(Vector3.up));
                            // direction = Vector3.up;
                        else
                            if (!isTurning)
                            StartCoroutine(Turn(Vector3.down));
                            // direction = Vector3.down;
                        break;
                    case "21": // up or right
                    case "26":
                        if (Random.Range(0, 2) == 0)
                            if (!isTurning)
                            StartCoroutine(Turn(Vector3.up));
                            // direction = Vector3.up;
                        else
                            if (!isTurning)
                            StartCoroutine(Turn(Vector3.right));
                            // direction = Vector3.right;
                        break;
                    case "22": // right or down
                    case "27":
                        if (Random.Range(0, 2) == 0)
                            if (!isTurning)
                            // direction = Vector3.right;
                            StartCoroutine(Turn(Vector3.right));
                        else
                            if (!isTurning)
                            // direction = Vector3.down;
                            StartCoroutine(Turn(Vector3.down));
                        break;
                    case "23": // down or left
                    case "28":
                        if (Random.Range(0, 2) == 0)
                            if (!isTurning)
                            // direction = Vector3.down;
                            StartCoroutine(Turn(Vector3.down));
                        else
                            if (!isTurning)
                            // direction = Vector3.left;
                            StartCoroutine(Turn(Vector3.left));
                        break;
                    case "24": // left or up
                    case "25":
                        if (Random.Range(0, 2) == 0)
                            if (!isTurning)
                            // direction = Vector3.left;
                            StartCoroutine(Turn(Vector3.left));
                        else
                            if (!isTurning)
                            // direction = Vector3.up;
                            StartCoroutine(Turn(Vector3.up));
                        break;
                    case "9": // up or left or right
                        int random = Random.Range(0, 3);
                        if (random == 0)
                            if (!isTurning)
                            // direction = Vector3.up;
                            StartCoroutine(Turn(Vector3.up));
                        else if (random == 1)
                            if (!isTurning)
                            // direction = Vector3.left;
                            StartCoroutine(Turn(Vector3.left));
                        else
                            if (!isTurning)
                            // direction = Vector3.right;
                            StartCoroutine(Turn(Vector3.right));
                        break;
                    case "10": // up or down or right
                        random = Random.Range(0, 3);
                        if (random == 0)
                            if (!isTurning)
                            // direction = Vector3.up;
                            StartCoroutine(Turn(Vector3.up));
                        else if (random == 1)
                            if (!isTurning)
                            // direction = Vector3.down;
                            StartCoroutine(Turn(Vector3.down));
                        else
                            if (!isTurning)
                            // direction = Vector3.right;
                            StartCoroutine(Turn(Vector3.right));
                        break;
                    case "11": // down or left or right
                        random = Random.Range(0, 3);
                        if (random == 0)
                            if (!isTurning)
                            // direction = Vector3.down;
                            StartCoroutine(Turn(Vector3.down));
                        else if (random == 1)
                            if (!isTurning)
                            // direction = Vector3.left;
                            StartCoroutine(Turn(Vector3.left));
                        else
                            if (!isTurning)
                            // direction = Vector3.right;
                            StartCoroutine(Turn(Vector3.right));
                        break;
                    case "12": // down or left or up
                        random = Random.Range(0, 3);
                        if (random == 0)
                            if (!isTurning)
                            // direction = Vector3.down;
                            StartCoroutine(Turn(Vector3.down));
                        else if (random == 1)
                            if (!isTurning)
                            // direction = Vector3.left;
                            StartCoroutine(Turn(Vector3.left));
                        else
                            if (!isTurning)
                            // direction = Vector3.up;
                            StartCoroutine(Turn(Vector3.up));
                        break;
                    case "13": // right lane
                    case "19": 
                        if (Random.Range(0, 2) == 0) {
                            changeLaneDirection = Vector3.right;
                            StartCoroutine(ChangeLane());
                        }
                        break;
                    case "14": // down lane
                    case "20":
                        if (Random.Range(0, 2) == 0) {
                            changeLaneDirection = Vector3.down;
                            StartCoroutine(ChangeLane());
                        }
                        break;
                    case "15": // left lane
                    case "17":
                        if (Random.Range(0, 2) == 0) {
                            changeLaneDirection = Vector3.left;
                            StartCoroutine(ChangeLane());
                        }
                        break;
                    case "16": // up lane
                    case "18":
                        if (Random.Range(0, 2) == 0) {
                            changeLaneDirection = Vector3.up;
                            StartCoroutine(ChangeLane());
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
    IEnumerator Turn(Vector3 targetDirection)
    {
        yield return new WaitForSeconds(0.5f / moveSpeed);
        direction = targetDirection;
        isTurning = false;
    }
    IEnumerator ChangeLane()
    {
        isChangingLane = true;
        yield return new WaitForSeconds(1.0f / moveSpeed);
        isChangingLane = false;
    }
    void DetectTileAtPosition(Vector3 worldPosition)
    {
        // 将世界坐标转换为 Tilemap 坐标
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);

        // 获取相应位置的 Tile
        TileBase tile = tilemap.GetTile(cellPosition);

        if (tile != null)
        {
            // 基于 Tile 的属性或自定义属性判断 Tile 类型
            // 例如，你可以根据 Tile 的名字来判断类型
            string tileType = tile.name;
            Debug.Log("Tile Type: " + tileType);

            // 根据 tileType 进行更多的处理...
        }
    }
    void Move()
    {
        transform.Translate(direction * Time.deltaTime * moveSpeed);
        if (isChangingLane)
        {
            transform.Translate(changeLaneDirection * Time.deltaTime * moveSpeed);
        }
        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    public static float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f, float mean = 0.0f, float sigma = 1.0f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        // float mean = (minValue + maxValue) / 2.0f;
        // float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }
}
