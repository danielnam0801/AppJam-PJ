using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleUpUI : UIComponent
{
    [SerializeField] GameObject uiObj;
    [SerializeField] Text text;
    [SerializeField] float duration = 1f;
    [SerializeField] bool loop = true;
    [SerializeField] Ease ease;
    [SerializeField] float scaleUp;

    Vector3 baseVec;
    private void Awake()
    {
        baseVec = transform.localScale;
    }

    bool use = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetUp(use);
            use = !use;
        }
    }

    public override void SetUp(bool value)
    {
        if (value)
        {
            Vector3 endValue = baseVec * scaleUp;
            if (endValue == Vector3.zero) endValue = Vector3.one;
            uiObj.transform.DOKill();
            if (loop)
                uiObj.transform.DOScale(endValue, duration).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
            else
                uiObj.transform.DOScale(endValue, duration).SetEase(ease);
        }
        else
        {
            uiObj.transform.DOScale(baseVec, duration).SetEase(ease);
        }

    }
}
