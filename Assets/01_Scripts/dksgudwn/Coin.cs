using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CashManager.Instance.IncreaseCash(1);
            print("����ȹ��");
            Destroy(gameObject);
        }
    }
}
