using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] private Transform playerTransform;

    Transform traget;

    public int maxHealth = 100; // ���ʹ�������
    public int currentHealth; // ���ʹ�Ѩ�غѹ
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth; // ��˹�������ʹ�������
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < 5)// ����¹�Ţ������
        {
            traget = playerTransform;
        }

        if (traget != null)
        {
            agent.SetDestination(playerTransform.position);
        }
    }
    private void Die()
    {
        Debug.Log("Agent died!");
        Destroy(gameObject); // ����� GameObject �ͧ Agent
    }
}
