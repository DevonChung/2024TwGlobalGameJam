using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class CarControl : MonoBehaviour
{

    public float moveSpeed = 3.0f;
    public float liveTime = 30.0f;
    public Vector3 direction = Vector3.right;
    public Tilemap tilemap;
    
    // Update is called once per frame
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("Turn").GetComponent<Tilemap>();
    }
    void FixedUpdate()
    {
        CheckTurn();
        Move();
        
    }
    void CheckTurn() {
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
                switch (tileType)
                {
                    case "turn_left":
                        direction = Vector3.up;
                        break;
                    case "turn_right":
                        direction = Vector3.down;
                        break;
                    case "turn_up":
                        direction = Vector3.left;
                        break;
                    case "turn_down":
                        direction = Vector3.right;
                        break;
                    case "turn_up_or_left":
                        if (Random.RangeInt(0, 2) == 0)
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
    void Move() {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
