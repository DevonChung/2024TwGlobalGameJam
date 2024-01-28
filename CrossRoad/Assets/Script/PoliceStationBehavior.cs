using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStationBehavior : MonoBehaviour
{
    public int FinePrice = 2000;
    public int RewardPrice = 900;
    public string ReturnMoneyMessage = "��a�P�§A���۹�";
    public string StealMoneyMessage = "�t���� �[������";
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void MinusPlayerMoney(int amount)
    {
        Debug.Log("Lost money amout is" + amount);
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
                for (int i = 0; i < playerControl.get_thousand_money_number(); ++i)
                {
                    CharacterBuffUiManager.instance.DelStatusIcon(CharacterBuffUiManager.ExtraStatusType.Money);
                }
                MyGameManager.instance.myUIManager.SetText(ReturnMoneyMessage);
                AudioManager.instance.PlayPoliceStationAudio();
                
                playerControl.ResetThousandMoneyNumber();
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
                Debug.Log("detect trigger collision: check "+ playerControl.get_thousand_money_number());
                for (int i = 0; i < playerControl.get_thousand_money_number(); ++i)
                {
                    CharacterBuffUiManager.instance.DelStatusIcon(CharacterBuffUiManager.ExtraStatusType.Money);
                }
                MyGameManager.instance.myUIManager.SetText(StealMoneyMessage);
                CharacterBuffUiManager.instance.DelStatusIcon(CharacterBuffUiManager.ExtraStatusType.Money);
                playerControl.ResetThousandMoneyNumber();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
