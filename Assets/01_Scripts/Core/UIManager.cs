using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Scrollbar _guageSlider;

    public void SetGuageSliderActive(bool value)
    {
        _guageSlider.gameObject.SetActive(value);
    }

    public void SetGuageSliderValue(float value)
    {
        _guageSlider.value = value;
    }
}
