using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] GameObject player;

    private float distance;
    private float bestDistance;

    [SerializeField] Slider distanceSlider; //�⺻ �Ÿ� ��Ÿ���ִ� �����̴�
    [SerializeField] Image bestDistanceImage; //bestDistance ǥ�ô� Image ��ġ�� ������ �����߱⿡ ImageŸ��
    [SerializeField] Text distanceText; //�Ÿ� ��Ÿ���ִ� Text
    [SerializeField] Text bestDistanceText; // ���� �Ÿ� ��Ÿ���ִ� Text

    [SerializeField] GameObject overHigh;
    [SerializeField] Text overHighText; // ȭ���� �Ѿ� �ö����� ǥ���ϴ� Text

    [SerializeField] Text goldAmount; // �� ������ ǥ�� Text
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
