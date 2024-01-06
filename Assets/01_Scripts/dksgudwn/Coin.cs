using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CashManager.Instance.IncreaseCash(1);
            print("ÄÚÀÎÈ¹µæ");
            Destroy(gameObject);
        }
    }
}
