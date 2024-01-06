using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : UIComponent
{
    [SerializeField] Ease ease;
    //[SerializeField] UIMoveType moveType;
    [SerializeField] float duration = 1f;
    [SerializeField] Image[] images;

    public override void SetUp(bool value)
    {
        if (value)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].DOFade(1, duration).SetEase(ease);
            }
        }
        else
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].DOFade(0, duration).SetEase(ease);
            }
        }
    }
}
