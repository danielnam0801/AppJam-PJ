using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private Button button;
    private Image image;
    private void Awake()
    {
        button = GetComponent<Button>();    
        image = GetComponent<Image>();
    }

    public void Update()
    {
        if(CashManager.Instance.MoneyCompare(UpgradeManager.Instance.CurNeedMoney))
        {
            button.interactable = true;
            image.color = Color.white;
        }else
        {
            button.interactable = false;
            Color color = Color.black;
            color.a = 0.3f;
            image.color = color;
        }
    }
}
