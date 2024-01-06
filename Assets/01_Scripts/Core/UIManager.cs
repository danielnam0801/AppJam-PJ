using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{ 
    private float distance;
    private float bestDistance;

    [SerializeField] Slider distanceSlider; //기본 거리 나타내주는 슬라이더
    [SerializeField] Image bestDistanceImage; //bestDistance 표시는 Image 위치를 변경해 구현했기에 Image타입
    [SerializeField] Text distanceText; //거리 나타내주는 Text
    [SerializeField] Text bestDistanceText; // 최종 거리 나타내주는 Text

    [SerializeField] GameObject overHigh;
    [SerializeField] Text overHighText; // 화면을 넘어 올라갔을때 표시하는 Text

    [SerializeField] Text goldAmount; // 돈 보유량 표시 Text
    [SerializeField] private Scrollbar _guageSlider;

    [SerializeField] private UIComponent[] gameStartUI;
    [SerializeField] private UIComponent[] gamePlayUI;
    [SerializeField] private UIComponent[] gamePauseUI;
    [SerializeField] private UIComponent[] gameEndUI;

    public void SetGuageSliderActive(bool value)
    {
        _guageSlider.gameObject.SetActive(value);
    }

    public void SetGuageSliderValue(float value)
    {
        _guageSlider.value = value;
    }

    void Update()
    {
        if(GameManager.Instance.CurGameState == GameState.GameOver)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.Restart();
            }
        }

        Vector3 pos = PlayerManager.Instance.PlayerTrm.position;
        distance = pos.x * -1;

        if (distance > 10000)
            distance = 10000;
        if (distance < 0)
            distance = 0;
        if (bestDistance < distance)
        {
            bestDistance = distance;
        }

        goldAmount.text = CashManager.Instance.Cash.ToString();
        distanceText.text = distance.ToString();
        distanceSlider.value = 1 - (distance / 10000);
        bestDistanceText.text = bestDistance.ToString();
        bestDistanceImage.rectTransform.localPosition = new Vector3(-0.037f * bestDistance, 0f, 0f);

        if (bestDistanceImage.rectTransform.localPosition.x < -370)
        {
            bestDistanceImage.rectTransform.localPosition = new Vector3(-0.037f * 10000, 0f, 0f);
        }

        if (pos.y > 1600)
        {
            overHigh.SetActive(true);
            overHighText.text = (pos.y - 1600).ToString();
        }
        else overHigh.SetActive(false);
    }

    public void GameStartUI()
    {
        for (int i = 0; i < gamePlayUI.Length; i++)
        {
            gamePlayUI[i].SetUp(false);
        }
        for (int i = 0; i < gameEndUI.Length; i++)
        {
            gameEndUI[i].SetUp(false);
        }
        for(int i = 0; i < gameStartUI.Length; i++)
        {
            gameStartUI[i].SetUp(true);
        }
    }

    public void GamePlayUI()
    {
        for (int i = 0; i < gameStartUI.Length; i++)
        {
            gameStartUI[i].SetUp(false);
        }
        for (int i = 0; i < gamePlayUI.Length; i++)
        {
            gamePlayUI[i].SetUp(true);
        }
    }

    public void GameOverUI()
    {
        for (int i = 0; i < gamePlayUI.Length; i++)
        {
            gamePlayUI[i].SetUp(false);
        }
        for (int i = 0; i < gameEndUI.Length; i++)
        {
            gameEndUI[i].SetUp(true);
        }
    }

    public void PauseUI(bool value)
    {
        Debug.Log("pauseOn");
        for (int i = 0; i < gamePauseUI.Length; i++)
        {
            gamePauseUI[i].SetUp(value);
        }
    }
}
