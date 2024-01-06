using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] AbilitySO[] abilitySOs;

    [SerializeField] Image selectImage1;
    [SerializeField] Image selectImage2;
    [SerializeField] Image selectImage3;
    [SerializeField] TMP_Text abilityName;
    [SerializeField] TMP_Text abilityDescription;
    [SerializeField] Button upgradeButton;

    private int currentIndex;


    private void Start()
    {
        selectImage1.sprite = abilitySOs[0].abilityImage;
        selectImage2.sprite = abilitySOs[1].abilityImage;
        selectImage3.sprite = abilitySOs[2].abilityImage;

        selectImage1.GetComponent<Button>().onClick.AddListener(() => ImageClick(1));
        selectImage2.GetComponent<Button>().onClick.AddListener(() => ImageClick(2));
        selectImage3.GetComponent<Button>().onClick.AddListener(() => ImageClick(3));

        ImageClick(1);
    }

    public void ImageClick(int index) // 
    {
        print(index + "이미지클릭");
        abilityName.text = abilitySOs[index - 1].abilityName + " (" + abilitySOs[index - 1].abilityLevel + ")";
        abilityDescription.text = abilitySOs[index - 1].abilityDescription;
        upgradeButton.GetComponentInChildren<TMP_Text>().text = abilitySOs[index - 1].abilityPrice.ToString() + "won";
        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(() => UpgradeLevel(index));
    }

    void UpgradeLevel(int index)
    {
        if (CashManager.Instance.Cash >= abilitySOs[index - 1].abilityPrice)
        {
            print("업그레이드레벨");
            abilitySOs[index - 1].abilityLevel++;
            CashManager.Instance.DecreaseCash(abilitySOs[index - 1].abilityPrice);
        }
        else
        {
            print("돈없음");
        }
    }
}
