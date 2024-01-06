using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;

    void Update()
    {
        if (player.transform.position.y > 1600)
        {
            transform.position = new Vector3(player.transform.position.x, 1600f, -10f);
        }
        else if (player.transform.position.y < 0)
        {
            transform.position = new Vector3(player.transform.position.x, 0f, -10f);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        }
    }
}
