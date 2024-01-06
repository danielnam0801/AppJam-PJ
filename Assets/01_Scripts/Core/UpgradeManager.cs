using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EUpgradeType
{
    UpgradeCannon = 0,
    BounceUp,
    MoveSpeedUp,
}
public class UpgradeManager : MonoSingleton<UpgradeManager>
{
    [SerializeField] AbilitySO[] abilitySOs;
    [SerializeField] TMP_Text abilityName;
    [SerializeField] TMP_Text abilityDescription;
    [SerializeField] TMP_Text wonText;

    public EUpgradeType CurType;
    public int baseNeedMoney = 1000;
    public int AddNeedMoney = 100;
    public int CurNeedMoney;

    public int[] levetTxt = new int[3] { 1, 1, 1 };

    public override void Init()
    {
        CurNeedMoney = baseNeedMoney;

        int idx = (int)CurType;
        abilityName.text = abilitySOs[idx].abilityName + " (" + levetTxt[idx] + ")";
        abilityDescription.text = abilitySOs[idx].abilityDescription;
        wonText.text = abilitySOs[idx].abilityPrice.ToString() + "won";
    }

    public void Update()
    {
        int idx = (int)CurType;
        abilityName.text = abilitySOs[idx].abilityName + " (" + levetTxt[idx] + ")";
        abilityDescription.text = abilitySOs[idx].abilityDescription;
        wonText.text = abilitySOs[idx].abilityPrice.ToString() + "won";
    }


    public void SetCurUpgradeType(EUpgradeType type)    
    {
        CurType = type;
    }

    public void Upgrade()
    {
        if (CashManager.Instance.UseCash(CurNeedMoney))
        {
            CurNeedMoney += AddNeedMoney;
            switch (CurType)
            {
                case EUpgradeType.UpgradeCannon:
                    PlayerManager.Instance.UpgradeCannonShootPower();
                    break;
                case EUpgradeType.BounceUp:
                    GameManager.Instance.UpgradeAll();
                    break;
                case EUpgradeType.MoveSpeedUp:
                    GameManager.Instance.UpgradeSpeedX();
                    break;
            }
            levetTxt[(int)CurType]++;
           
        }
           
    }
}
