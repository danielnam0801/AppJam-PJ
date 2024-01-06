using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private float baseShootPower;

    private Player player;
    public Player Player => player;

    private Cannon cannon;
    public Cannon Cannon => cannon;

    private Transform playerTrm;
    public Transform PlayerTrm => playerTrm;

    private Transform cannonTrm;
    public Transform CannonTrm => cannonTrm;

    public float BaseShootPower => baseShootPower;

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

}
