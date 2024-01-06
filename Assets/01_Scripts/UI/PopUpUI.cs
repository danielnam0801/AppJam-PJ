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

    RectTransform rectTrm;

    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
        switch (moveType)
        {
            case UIMoveType.Left:
                offpos = rectTrm.anchoredPosition + new Vector2(moveValue, 0);
                break;
            case UIMoveType.Right:
                offpos = rectTrm.anchoredPosition + new Vector2(-moveValue, 0);
                break;
            case UIMoveType.Up:
                offpos = rectTrm.anchoredPosition + new Vector2(0,-moveValue);
                break;
            case UIMoveType.Down:
                offpos = rectTrm.anchoredPosition + new Vector2(0, moveValue);
                break;
        }
        onpos = rectTrm.anchoredPosition;
        rectTrm.anchoredPosition = offpos;
    }

    
    public override void SetUp(bool on)
    {
        if(useFixed)
        {
            if (!on)
            {
                switch (moveType)
                {
                    case UIMoveType.Left:
                        rectTrm.DOAnchorPosX(offpos.x, duration).SetEase(ease);
                        break;
                    case UIMoveType.Right:
                        rectTrm.DOAnchorPosX(offpos.x, duration).SetEase(ease);
                        break;
                    case UIMoveType.Up:
                        rectTrm.DOAnchorPosY(offpos.y, duration).SetEase(ease);
                        break;
                    case UIMoveType.Down:
                        rectTrm.DOAnchorPosY(offpos.y, duration).SetEase(ease);
                        break;
                }
            }
            else
            {
                switch (moveType)
                {
                    case UIMoveType.Left:
                        rectTrm.DOAnchorPosX(onpos.x, duration).SetEase(ease);
                        break;
                    case UIMoveType.Right:
                        rectTrm.DOAnchorPosX(onpos.x, duration).SetEase(ease);
                        break;
                    case UIMoveType.Up:
                        rectTrm.DOAnchorPosY(onpos.y, duration).SetEase(ease);
                        break;
                    case UIMoveType.Down:
                        rectTrm.DOAnchorPosY(onpos.y, duration).SetEase(ease);
                        break;
                }
            }
        }
        else
        {
            if(on)
            {
                rectTrm.DOAnchorPos(onpos, duration).SetEase(ease);
            }
            else
            {
                rectTrm.DOAnchorPos(offpos, duration).SetEase(ease);
            }
        }
    }
}
