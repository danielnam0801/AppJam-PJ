using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public EUpgradeType UpgradeType;

    private Button button;
    public Button Button => button;

    public void OnClick()
    {
        UpgradeManager.Instance.SetCurUpgradeType(UpgradeType);
    }
}
