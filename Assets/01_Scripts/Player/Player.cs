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

    [SerializeField]
    private float _addGravityValue;
    public float AddGravityValue => _addGravityValue;
    
    [SerializeField]
    private float _baseGravityvalue = 1;
    public float BaseGravityvalue => _baseGravityvalue; 
   
    private float curSpeed;
    public float CurSpeed => curSpeed;
    public Vector2 CurVelocity => rigidbody.velocity;

    Rigidbody2D rigidbody;

    private EPlayerState curState;

    private float afterClickTime = 0.3f;
    private float clickTimer = 0.0f;
    bool clickTIme = false;
    bool canClick = false;

    bool gameOver = false;
    bool canInteract = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void InWater()
    {
        StopImmediately();
        GameOver();
    }

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        curState = EPlayerState.Ready;
        rigidbody.gravityScale = 0f;
        gameOver = false;
        canInteract = true;
        clickTIme = false;
        canClick = false;
        clickTimer = 0f;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerManager.Instance.Cannon.Init();
            Init();
        }

        if (gameOver && !canInteract) return;
        curState = PlayerManager.Instance.CurPlayerState;
        if(clickTIme == true)
        {
            clickTimer += Time.deltaTime;
            if(clickTimer > afterClickTime)
            {
                canClick = true;
                clickTIme = false;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            switch (curState)
            {
                case EPlayerState.Ready:
                    DoReady();
                    break;
                case EPlayerState.Fly:
                    if(canClick)
                        AddGravity();
                    break;
            }
        }
        CheckFlip();
        CheckGameOver();
    }

    private void CheckFlip()
    {
        if(rigidbody.velocity.x >= 0)
            rigidbody.velocity = new Vector2(-rigidbody.velocity.x, rigidbody.velocity.y);
    }

    private void UseGravity(bool value)
    {
        rigidbody.gravityScale = value ? BaseGravityvalue : 0f;
    }

    private void DoReady()
    {
        transform.position = PlayerManager.Instance.CannonTrm.position;
        PlayerManager.Instance.SetPlayerState(EPlayerState.InCannon);
        UIManager.Instance.SetGuageSliderActive(true);
    }

    private void AddGravity()
    {
        rigidbody.gravityScale = AddGravityValue;
        Debug.Log("click");
    }

    private void RemoveGravity()
    {
        rigidbody.gravityScale = 0;
    }

    private void ReturnGravity()
    {
        rigidbody.gravityScale = BaseGravityvalue;
    }    

    public void StopImmediately()
        => rigidbody.velocity = Vector3.zero;

    private void EscapeCannonEvent()
    {
        PlayerManager.Instance.SetPlayerState(EPlayerState.Fly);
        UIManager.Instance.SetGuageSliderActive(false);
        clickTIme = true;
        UseGravity(true);
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
                shootPower *= 0.6f;
                break;
            case EShootPowerState.NormalWeak:
                shootPower *= 0.8f;
                break;
            case EShootPowerState.Normal:
                break;
            case EShootPowerState.NormalStrong:
                shootPower *= 1.2f;
                break;
            case EShootPowerState.Strong:
                shootPower *= 1.4f;
                break;
        }
        Debug.Log(shootPower);

        rigidbody.AddForce(shootVec * shootPower,ForceMode2D.Impulse);
    }

    private void CheckGameOver()
    {
        if (rigidbody.velocity.magnitude < 1 && PlayerManager.Instance.CurPlayerState != EPlayerState.Ready && PlayerManager.Instance.CurPlayerState != EPlayerState.InCannon)
        {
            canInteract = false;
            Debug.Log("Over");
            //GameOver();
        }
    }
    public void GameOver()
    {
        gameOver = true;
        StopImmediately();
        RemoveGravity();
        GameManager.Instance.GameOver();   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Obstacle obstacle;
        if (collision.transform.TryGetComponent<Obstacle>(out obstacle))
        {

            if(!canInteract)
            {
                GameOver();
            }
            if (gameOver || !canInteract) return;
            
            HitObstacle(obstacle.type);
        }
    }

    private void HitObstacle(EObstacleType obstacleType)
    {
        ReturnGravity();
        float angle = PlayerManager.Instance.ReBounceLimitAngle;

        Vector2 shootVec = PlayerManager.DegreeToVector2(angle).normalized;
        float speedX = GameManager.Instance.GetReboundXSpeed(obstacleType);
        //float speedY = Mathf.Clamp(Mathf.Abs(speedX), 0, 3) * PlayerManager.Instance.RebounceAddYSpeed;

        //float speedY = Mathf.Min(speedY, GameManager.Instance.GetLimitYSpeed();
        float speedY = speedX * GameManager.Instance.GetLimitYSpeed(obstacleType);//= GameManager.Instance.GetLimitYSpeed()


        Debug.Log("Speedx : " + speedX);
        StopImmediately();
        Vector2 shootValue = new Vector2(shootVec.x * speedX, shootVec.y * speedY);
        Debug.Log($"reboundVec : {shootValue}");
        rigidbody.AddForce(shootValue, ForceMode2D.Impulse);
    }
}
