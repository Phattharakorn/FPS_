using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;

    [SerializeField] private Transform playerTransform;

    Transform traget;

    public int Radius = 0;
    public int maxHealth = 100; // ���ʹ�������
    public int currentHealth; // ���ʹ�Ѩ�غѹ
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // ��˹� Animator
        currentHealth = maxHealth; // ��˹�������ʹ�������
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < Radius)// ����¹�Ţ������
        {
            traget = playerTransform;
        }
        if (traget != null)
        {
            agent.SetDestination(playerTransform.position);
            animator.SetBool("IsRunning", true); // ����¹ Animation �� Run
        }
        else
        {
            animator.SetBool("IsRunning", false); // ����¹ Animation �� Idle
        }
    }
    private void Die()
    {
        Debug.Log("Agent died!");
        Destroy(gameObject); // ����� GameObject �ͧ Agent
    }
}
