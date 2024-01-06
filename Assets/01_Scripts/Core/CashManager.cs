using UnityEngine;

public class CashManager : MonoSingleton<CashManager>
{
    private int cash;

    public int Cash
    {
        get { return cash; }
        private set { cash = value; }
    }

    public void IncreaseCash(int value, int bounceCount = 1)
    {
        cash = value * bounceCount;
    }

    public void DecreaseCash(int value)
    {
        cash -= value;
    }
}
