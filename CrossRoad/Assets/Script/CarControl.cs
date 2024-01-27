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
            moveSpeed = RandomGaussian(minMoveSpeed, maxMoveSpeed, meanMoveSpeed, sigmaMoveSpeed);
            resetSpeedTime = RandomGaussian(minResetSpeedTime, maxResetSpeedTime, meanResetSpeedTime, sigmaResetSpeedTime);
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
                    case "go_up":
                        StartCoroutine(Turn(Vector3.up));
                        // direction = Vector3.up;
                        break;
                    case "go_down":
                        StartCoroutine(Turn(Vector3.down));
                        // direction = Vector3.down;
                        break;
                    case "go_left":
                        StartCoroutine(Turn(Vector3.left));
                        // direction = Vector3.left;
                        break;
                    case "go_right":
                        StartCoroutine(Turn(Vector3.right));
                        // direction = Vector3.right;
                        break;
                    case "turn_up_or_left":
                        if (Random.Range(0, 2) == 0)
                        {
                            direction = Vector3.left;
                        }
                        else
                        {
                            direction = Vector3.up;
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
