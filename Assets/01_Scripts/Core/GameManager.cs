using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Start, 
    Play,
    GameOver
}

public class GameManager : MonoSingleton<GameManager>
{
    //[SerializeField] KeyCode inputKeyCode;
    //public KeyCode InputKeyCode => inputKeyCode;

    [SerializeField] GameState gameState = GameState.Start;

    [SerializeField] private PhysicsMaterial2D jellyMat;
    [SerializeField] private PhysicsMaterial2D whaleMat;
    [SerializeField] private PhysicsMaterial2D groundMat;

    [SerializeField] private float JellyBounciessUpgradeValue;
    [SerializeField] private float WhaleBounciessUpgradeValue;
    [SerializeField] private float GroundBounciessUpgradeValue;

    [Header("ReboundSpeed")]
    [SerializeField] private float reBoundXSpeed = 2;
    [SerializeField] private float xSpeedUpgradeValue = 0.1f;
    [SerializeField] private float LimitYSpeedWHale = 5;
    [SerializeField] private float LimitYSpeedJelly = 5;
    [SerializeField] private float LimitYSpeedGround = 5;

    public GameState CurGameState => gameState;
    public void SetGameState(GameState state) => gameState = state;
    public float JellyBounciess { get; set; }
    public float WhaleBounciess { get; set; }
    public float GroundBounciess { get; set; }

    private float curLimitXSpeed;
    public override void Init()
    {
        JellyBounciess = jellyMat.bounciness;
        WhaleBounciess = whaleMat.bounciness;
        GroundBounciess = groundMat.bounciness;
        curLimitXSpeed = reBoundXSpeed;
        SetGameState(GameState.Start);
        GameStart();
    }

    public void UpgradeSpeedX()
    {
        reBoundXSpeed += xSpeedUpgradeValue;
    }

    public float GetReboundXSpeed(EObstacleType otType)
    {
        switch (otType)
        {
            case EObstacleType.Ground:
                curLimitXSpeed = curLimitXSpeed * 0.7f;
                return curLimitXSpeed;
            case EObstacleType.Whale:
            case EObstacleType.JellyFish:
                curLimitXSpeed = reBoundXSpeed;
                return reBoundXSpeed; 

        }
        return reBoundXSpeed;
    }

    public float GetLimitYSpeed(EObstacleType otType)
    {
        switch (otType)
        {
            case EObstacleType.Whale:
                return LimitYSpeedWHale * whaleMat.bounciness;
            case EObstacleType.Ground:
                return LimitYSpeedGround * groundMat.bounciness;
            case EObstacleType.Rock:
                break;
            case EObstacleType.JellyFish:
                return LimitYSpeedJelly * jellyMat.bounciness;
               
        }
        return LimitYSpeedWHale;
    }

    public void Upgrade(EObstacleType type)
    {
        switch (type)
        {
            case EObstacleType.Whale:
                WhaleBounciess += WhaleBounciessUpgradeValue;
                break;
            case EObstacleType.Ground:
                GroundBounciess += GroundBounciessUpgradeValue;
                break;
            case EObstacleType.JellyFish:
                JellyBounciess += JellyBounciessUpgradeValue;
                break;
        }
    }

    public void UpgradeAll()
    {
        WhaleBounciess += WhaleBounciessUpgradeValue;
        GroundBounciess += GroundBounciessUpgradeValue;
        JellyBounciess += JellyBounciessUpgradeValue;
    }

    public void GameStart()
    {
        
        UIManager.Instance.GameStartUI();

        SetGameState(GameState.Start);
        //Restart();

    }


    public void Restart()
    {
        PlayerInit();
        GameStart();
    }


    private void PlayerInit()
    {
        PlayerManager.Instance.Player.Init();
        PlayerManager.Instance.Cannon.Init();
    }

    public void GamePlay()
    {
        SetGameState(GameState.Play);
        UIManager.Instance.GamePlayUI();
    }
    public void GameOver()
    {
        SetGameState(GameState.GameOver);
        UIManager.Instance.GameOverUI();
        //throw new NotImplementedException();
    }

    IEnumerator DelayCoroutine(float time, Action act)
    {
        yield return new WaitForSeconds(time);
        act?.Invoke();
    }
}
