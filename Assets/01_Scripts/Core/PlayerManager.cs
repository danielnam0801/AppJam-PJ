using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private float baseShootPower;
    [SerializeField] private float reBounceLimitAngle = 60;

    //[SerializeField] private float reBounceaddYSpeed = 3;

    [SerializeField] private float activeObstacleRange = 10f;

    

    private Player player;
    public Player Player => player;

    private Cannon cannon;
    public Cannon Cannon => cannon;

    private Transform playerTrm;
    public Transform PlayerTrm => playerTrm;

    private Transform cannonTrm;
    public Transform CannonTrm => cannonTrm;

    public float BaseShootPower => baseShootPower;
    public float ReBounceLimitAngle => reBounceLimitAngle;
    //public float RebounceAddYSpeed => reBounceaddYSpeed;
    //public float ReboundXSpeed => reBoundXSpeed;
    public float ActiveObstacleRange => activeObstacleRange;

    private EPlayerState curState;
    public EPlayerState CurPlayerState => curState;

    public override void Init()
    {
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            playerTrm = player.transform;
        }

        cannon = FindObjectOfType<Cannon>();
        if (cannon != null)
        {
            cannonTrm = cannon.transform;
        }

        curState = EPlayerState.Ready;
    }
    
    public void SetPlayerState(EPlayerState playerState)
    {
        curState = playerState;
    }

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

}
