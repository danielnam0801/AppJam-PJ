using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EShootPowerState
{
    Weak = 0,
    NormalWeak,
    Normal,
    NormalStrong,
    Strong,
    End
}

public class Cannon : MonoBehaviour
{
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;

    [SerializeField] private float baseAngle;

    [SerializeField] private float guageUpSpeed;

    private float curAngle;
    public float CurAngle => curAngle;

    private float guageUpdir = 1;

    private Player player;
    private float InCannonTIme;
    
    private void Start()
    {
        player = PlayerManager.Instance.Player;
        InCannonTIme = 0;
    }
    
    public void Init()
    {
        curAngle = baseAngle;
        guageUpdir = 1;
        InCannonTIme = 0;
    }

    private void LateUpdate()
    {
        if (Time.timeScale == 0f) return;
        if (PlayerManager.Instance.CurPlayerState != EPlayerState.InCannon) return;
        
        InCannonTIme +=Time.deltaTime;
        if (InCannonTIme < 0.2f) return;

        if (Input.GetMouseButton(0))
        {
            MouseDown();
            SetGuage();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            MouseUp(curAngle);
        }
    }

    private void SetGuage()
    {
        float value = (curAngle-minAngle) / (maxAngle-minAngle);
        UIManager.Instance.SetGuageSliderValue(value);
    }

    public void MouseDown()
    {
        if (curAngle >= maxAngle)
        {
            guageUpdir = -1;
        }
        else if(curAngle <= minAngle)
        {
            guageUpdir = 1;
        }

        curAngle += guageUpdir * guageUpSpeed * Time.deltaTime;
        curAngle = Mathf.Clamp(curAngle, minAngle, maxAngle);
    }

    float lerpValue;
    public void MouseUp(float curAngle)
    {
        Vector2 curShootVec = PlayerManager.DegreeToVector2(curAngle).normalized;
        curShootVec.x *= -1;

        lerpValue = (maxAngle - minAngle) / ((int)EShootPowerState.End);

        int enumValue = (int)(curAngle - minAngle) / (int)lerpValue;
        EShootPowerState state;
        if (enumValue == 0)
        {
            state = EShootPowerState.Weak;
        }
        else if (enumValue == 1)
        {
            state = EShootPowerState.Normal;
        }
        else if (enumValue == 2)
        {
            state = EShootPowerState.Strong;
        }
        else if (enumValue == 3)
        {
            state = EShootPowerState.Normal;
        }
        else if (enumValue == 4)
        {
            state = EShootPowerState.Weak;
        }
        else
        {
            state = EShootPowerState.Weak;
        }
        //PlayerManager.Instance.Player.Shoot(curShootVec, (EShootPowerState)enumValue);    
        PlayerManager.Instance.Player.Shoot(curShootVec, state);    
    }
}
