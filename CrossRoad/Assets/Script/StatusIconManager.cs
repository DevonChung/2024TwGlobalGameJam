using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterBuffUiManager;

public class StatusIconManager : MonoBehaviour
{
    private SpriteRenderer current_icon_render;
    [SerializeField]
    private ExtraStatusType current_status_type;
    // Rice, Shoe, UFO, Drug
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public ExtraStatusType get_current_status_type()
    {
        return current_status_type;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
