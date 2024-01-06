using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerState
{
    Ready,
    InCannon,
    Fly,
    Bounce
}

public class Player : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed;
    public float MaxSpeed => maxSpeed;

    private float curSpeed;
    public float CurSpeed => curSpeed;

    private Vector2 movementVec;
    public Vector2 MovementVec => movementVec;

    Rigidbody rigidbody;

    private EPlayerState curState;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        curState = PlayerManager.Instance.CurPlayerState;
        if(Input.GetMouseButtonUp(0))
        {
            switch (curState)
            {
                case EPlayerState.Ready:
                    DoReady();
                    break;
                case EPlayerState.Fly:
                    MoveDown();
                    break;
            }
        }
    }

    private void DoReady()
    {
        transform.position = PlayerManager.Instance.CannonTrm.position;
        PlayerManager.Instance.SetPlayerState(EPlayerState.InCannon);
        UIManager.Instance.SetGuageSliderActive(true);
    }

    private void MoveDown()
    {
        Debug.Log("MoveDown");
    }

    private void EscapeCannonEvent()
    {
        PlayerManager.Instance.SetPlayerState(EPlayerState.Fly);
        UIManager.Instance.SetGuageSliderActive(false);
    }

    public void Shoot(Vector2 shootVec, EShootPowerState shootState)
    {
        EscapeCannonEvent();
        float shootPower = PlayerManager.Instance.BaseShootPower;
        Debug.Log(shootState.ToString());
        Debug.Log(shootVec);
        switch (shootState)
        {
            case EShootPowerState.Weak:
                shootPower /= 2;
                break;
            case EShootPowerState.NormalWeak:
                shootPower *= 2/3;
                break;
            case EShootPowerState.Normal:
                break;
            case EShootPowerState.NormalStrong:
                shootPower *= 4/3;
                break;
            case EShootPowerState.Strong:
                shootPower = shootPower * 2;
                break;
        }
        Debug.Log(shootPower);

        rigidbody.AddForce(shootVec * shootPower, ForceMode.Impulse);
    }
}
