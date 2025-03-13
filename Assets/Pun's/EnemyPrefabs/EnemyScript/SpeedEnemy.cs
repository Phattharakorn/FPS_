using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpeedEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;

    [SerializeField] private Transform playerTransform;

    private float speedMultiplier = 1f;
    private float timeSincePlayerDetected = 0f;
    private bool playerDetected = false;

    // �������ʹ���Ѻ Agent
    //public int health = 100; // ����������ʹ 100

    public int maxHealth = 100; // ���ʹ�������
    public int currentHealth; // ���ʹ�Ѩ�غѹ


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // ��˹� Animator
        currentHealth = maxHealth; // ��˹�������ʹ�������
    }

    void Update()
    {
        // ��Ǩ�ͺ������ҧ�����ҧ Agent ��м�����
        if (Vector3.Distance(transform.position, playerTransform.position) < 10) // ����¹�Ţ������
        {
            if (!playerDetected)
            {
                playerDetected = true;
                timeSincePlayerDetected = 0f; // ������������;������蹤����á
            }

            agent.SetDestination(playerTransform.position);
            animator.SetBool("IsRunning", true); // ����¹ Animation �� Run
        }
        else
        {
            playerDetected = false; // ��Ҽ������͡�͡���� �����ش��Ǩ�Ѻ
            animator.SetBool("IsRunning", false); // ����¹ Animation �� Idle
        }

        // ��Ҿ�������
        if (playerDetected)
        {
            timeSincePlayerDetected += Time.deltaTime;
            animator.SetBool("IsRunning", true); // ����¹ Animation �� Run

            // ��Ҽ�ҹ� 3 �Թҷ���ѧ�ҡ��������
            if (timeSincePlayerDetected >= 2f)
            {
                speedMultiplier = 3f;
                agent.speed *= speedMultiplier;
                playerDetected = false; // ��ͧ�ѹ������������������¤���
            }
        }
    }
}
