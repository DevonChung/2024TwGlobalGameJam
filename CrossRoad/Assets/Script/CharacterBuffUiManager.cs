using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CharacterBuffUiManager : MonoBehaviour
{
    public static CharacterBuffUiManager instance;
    public enum ExtraStatusType { 
        Rice, Shoe, UFO, Drug
    };
    public GameObject statusIconManagerPrefab;
    protected int currentStatusCount = 0;
    protected List<GameObject> player_status_list;
    [SerializeField]
    GameObject RiceObject;
    [SerializeField]
    GameObject ShoeObject;
    [SerializeField]
    GameObject DrugObject;
    [SerializeField]
    GameObject UFOObject;
  

    // Start is called before the first frame update
    void Start()
    {
        player_status_list = new List<GameObject>();
        if (instance == null)
        {
            instance = this;
        }
    }

    protected void LayoutStatusIcon()
    {
        float start_x_pos = 0;
        start_x_pos = (player_status_list.Count-1) * (-0.5f);
        foreach (GameObject obj in player_status_list)
        {
            obj.transform.localPosition = new Vector2(start_x_pos, 0);
            start_x_pos += 1f;
        }
        

    }

    public void AddStatusIcon(ExtraStatusType add_status)
    {
        //GameObject gameObj = Instantiate(statusIconManagerPrefab, this.transform);
        GameObject gameObj = null;
        if (add_status == ExtraStatusType.Rice)
        {
            gameObj = Instantiate(RiceObject, this.transform);           
        }
        else if (add_status == ExtraStatusType.Shoe)
        {
            gameObj = Instantiate(ShoeObject, this.transform);
        }
        else if (add_status == ExtraStatusType.Drug)
        {
            gameObj = Instantiate(DrugObject, this.transform);
        }
        else if (add_status == ExtraStatusType.UFO)
        {
            gameObj = Instantiate(UFOObject, this.transform);
        }     
      
        player_status_list.Add(gameObj);
        LayoutStatusIcon();
    }


    public void DelStatusIcon(ExtraStatusType del_status)
    {
        foreach (GameObject gameObj in player_status_list)
        {
            Debug.LogWarning("play icon:" + gameObj.gameObject.name);
            Debug.LogWarning("del status is:" + del_status);
            StatusIconManager statusIconManager = gameObj.GetComponent<StatusIconManager>();
            if (statusIconManager.get_current_status_type() == del_status)
            {
                Debug.LogWarning("del status is:2" + del_status);
                player_status_list.Remove(gameObj);
                Destroy(gameObj);
                break;
            }
            Debug.LogWarning("del status is:3" + del_status);
        }
        LayoutStatusIcon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
