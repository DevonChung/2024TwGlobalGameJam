using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStationBehavior : MonoBehaviour
{
    public int FinePrice = 1000;
    public int RewardPrice = 900;
    public string ReturnMoneyMessage = "國家感謝你的誠實";
    public string StealMoneyMessage = "暗扛錢 加倍扣錢";
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void MinusPlayerMoney(int amount)
    {
        MyGameManager.instance.LostMoney(amount);
    }

    bool checkPlayerHasHoldMoney(PlayerControl playerControl)
    {
        bool bRet = false;
        if (playerControl.get_thousand_money_number() > 0)
        {
            bRet = true;
        }else
        { 
            bRet = false; 
        }
        return bRet;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("detect collision" + collision.gameObject.name);
        PlayerControl playerControl = collision.gameObject.GetComponent<PlayerControl>();
        if (playerControl != null)
        {
            if (checkPlayerHasHoldMoney(playerControl) == true)
            {
                MinusPlayerMoney(playerControl.get_thousand_money_number() * RewardPrice);
                playerControl.ResetThousandMoneyNumber();
                MyGameManager.instance.myUIManager.SetText(ReturnMoneyMessage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("detect trigger collision: check if player steal money");
        PlayerControl playerControl = collision.gameObject.GetComponent<PlayerControl>();
        if (playerControl != null)
        {
            if (checkPlayerHasHoldMoney(playerControl) == true)
            {
                MinusPlayerMoney(playerControl.get_thousand_money_number() * FinePrice);
                playerControl.ResetThousandMoneyNumber();
                MyGameManager.instance.myUIManager.SetText(StealMoneyMessage);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
