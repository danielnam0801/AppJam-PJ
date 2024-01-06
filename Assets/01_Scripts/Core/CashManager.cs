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

    public bool UseCash(int use)
    {
        if(use > cash)
        {
            cash -= use;
            return true;
        }
        else return false;
    }

    public bool MoneyCompare(int money) => money >= cash;
}
