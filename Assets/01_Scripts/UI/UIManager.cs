using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] GameObject player;

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
        distance = player.transform.position.x * -1;

        if (distance > 10000)
            distance = 10000;
        if (distance < 0)
            distance = 0;
        if (bestDistance < distance)
        {
            bestDistance = distance;
        }


        distanceText.text = distance.ToString();
        distanceSlider.value = 1 - (distance / 10000);
        bestDistanceText.text = bestDistance.ToString();
        bestDistanceImage.rectTransform.localPosition = new Vector3(-0.037f * bestDistance, 0f, 0f);

        if (bestDistanceImage.rectTransform.localPosition.x < -370)
        {
            bestDistanceImage.rectTransform.localPosition = new Vector3(-0.037f * 10000, 0f, 0f);
        }

        if (player.transform.position.y > 1600)
        {
            overHigh.SetActive(true);
            overHighText.text = (player.transform.position.y - 1600).ToString();
        }
        else overHigh.SetActive(false);
    }
}
