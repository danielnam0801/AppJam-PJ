using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Obstacle
{
    private void Update()
    {
        //Vector3 position = transform.position;
        //position.x = PlayerManager.Instance.PlayerTrm.position.x;
        //transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().InWater();
        }
    }
}
