using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum UIMoveType
{
    Left,
    Right,
    Up,
    Down,
}

public class PopUpUI : UIComponent
{
    [SerializeField] Ease ease;
    [SerializeField] UIMoveType moveType;
    [SerializeField] RectTransform offTrm;
    [SerializeField] RectTransform onTrm;
    [SerializeField] float moveValue = 500f;
    [SerializeField] float duration = 1f;
    [SerializeField] bool useFixed = true;

    Vector3 offpos;
    Vector3 onpos;

    private void Awake()
    {
        switch (moveType)
        {
            case UIMoveType.Left:
                offpos = transform.position + new Vector3(moveValue, 0);
                break;
            case UIMoveType.Right:
                offpos = transform.position + new Vector3(-moveValue, 0);
                break;
            case UIMoveType.Up:
                offpos = transform.position + new Vector3(0,-moveValue);
                break;
            case UIMoveType.Down:
                offpos = transform.position + new Vector3(0, moveValue);
                break;
        }
        onpos = transform.position;
    }
    bool isOn = false;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetUp(isOn);
            isOn = !isOn;
        }
    }
    public override void SetUp(bool on)
    {
        if(useFixed)
        {
            if (on)
            {
                switch (moveType)
                {
                    case UIMoveType.Left:
                        transform.DOMoveX(offpos.x, duration).SetEase(ease);
                        break;
                    case UIMoveType.Right:
                        transform.DOMoveX(offpos.x, duration).SetEase(ease);
                        break;
                    case UIMoveType.Up:
                        transform.DOMoveY(offpos.y, duration).SetEase(ease);
                        break;
                    case UIMoveType.Down:
                        transform.DOMoveY(offpos.y, duration).SetEase(ease);
                        break;
                }
            }
            else
            {
                switch (moveType)
                {
                    case UIMoveType.Left:
                        transform.DOMoveX(onpos.x, duration).SetEase(ease);
                        break;
                    case UIMoveType.Right:
                        transform.DOMoveX(onpos.x, duration).SetEase(ease);
                        break;
                    case UIMoveType.Up:
                        transform.DOMoveY(onpos.y, duration).SetEase(ease);
                        break;
                    case UIMoveType.Down:
                        transform.DOMoveY(onpos.y, duration).SetEase(ease);
                        break;
                }
            }
        }
        else
        {
            if(on)
            {
                transform.DOMove(onpos, duration).SetEase(ease);
            }
            else
            {
                transform.DOMove(offpos, duration).SetEase(ease);
            }
        }
    }
}
