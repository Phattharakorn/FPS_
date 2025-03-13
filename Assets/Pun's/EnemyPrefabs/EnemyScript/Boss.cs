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
    public int maxHealth = 100; // เลือดเริ่มต้น
    public int currentHealth; // เลือดปัจจุบัน
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // กำหนด Animator
        currentHealth = maxHealth; // กำหนดค่าเลือดเริ่มต้น
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < Radius)// เปลี่ยนเลขระยะได้
        {
            traget = playerTransform;
        }
        if (traget != null)
        {
            agent.SetDestination(playerTransform.position);
            animator.SetBool("IsRunning", true); // เปลี่ยน Animation เป็น Run
        }
        else
        {
            animator.SetBool("IsRunning", false); // เปลี่ยน Animation เป็น Idle
        }
    }
    private void Die()
    {
        Debug.Log("Agent died!");
        Destroy(gameObject); // ทำลาย GameObject ของ Agent
    }
}
