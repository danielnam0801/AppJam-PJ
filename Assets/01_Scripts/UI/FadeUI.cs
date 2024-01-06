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

    private void Awake()
    {
        SetUp(false);
    }

    public override void SetUp(bool value)
    {
        Debug.Log("Fade");
        if (value)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].DOKill();
                images[i].DOFade(0.63f, duration).SetEase(ease).SetUpdate(true);
            }
        }
        else
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].DOKill();
                images[i].DOFade(0, duration).SetEase(ease).SetUpdate(true);
            }
        }
    }
}
