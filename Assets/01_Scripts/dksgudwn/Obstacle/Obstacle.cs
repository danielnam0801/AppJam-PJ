using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EObstacleType
{
    Whale,
    Ground,
    Rock,
    JellyFish,
    Water
}

public class Obstacle : MonoBehaviour
{
    public EObstacleType type;
    Collider2D m_collider;
    Animator m_animator;

    private void Awake()
    {
        m_collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        float Range = PlayerManager.Instance.ActiveObstacleRange;
        float distance = Vector3.Distance(PlayerManager.Instance.PlayerTrm.position, transform.position);
        if (distance < Range)
        {
            m_collider.enabled = true;
        }
        else
        {
            m_collider.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (type == EObstacleType.Ground)
            {
                if (collision.transform.position.y < transform.position.y)
                {
                    collision.gameObject.GetComponent<Player>().GameOver();
                    Debug.Log("Contact side");
                }
            }
            if (type == EObstacleType.Whale)
            {
                m_animator = GetComponentInChildren<Animator>();
                print(m_animator);
                m_animator.SetTrigger("Roll");
                print("돌고있음");
            }
        }
    }
}
