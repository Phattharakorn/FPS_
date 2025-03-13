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

    // เพิ่มเลือดให้กับ Agent
    //public int health = 100; // เริ่มต้นเลือด 100

    public int maxHealth = 100; // เลือดเริ่มต้น
    public int currentHealth; // เลือดปัจจุบัน


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // กำหนด Animator
        currentHealth = maxHealth; // กำหนดค่าเลือดเริ่มต้น
    }

    void Update()
    {
        // ตรวจสอบระยะห่างระหว่าง Agent และผู้เล่น
        if (Vector3.Distance(transform.position, playerTransform.position) < 10) // เปลี่ยนเลขระยะได้
        {
            if (!playerDetected)
            {
                playerDetected = true;
                timeSincePlayerDetected = 0f; // รีเซ็ตเวลาเมื่อพบผู้เล่นครั้งแรก
            }

            agent.SetDestination(playerTransform.position);
            animator.SetBool("IsRunning", true); // เปลี่ยน Animation เป็น Run
        }
        else
        {
            playerDetected = false; // ถ้าผู้เล่นออกนอกระยะ ให้หยุดตรวจจับ
            animator.SetBool("IsRunning", false); // เปลี่ยน Animation เป็น Idle
        }

        // ถ้าพบผู้เล่น
        if (playerDetected)
        {
            timeSincePlayerDetected += Time.deltaTime;
            animator.SetBool("IsRunning", true); // เปลี่ยน Animation เป็น Run

            // ถ้าผ่านไป 3 วินาทีหลังจากพบผู้เล่น
            if (timeSincePlayerDetected >= 2f)
            {
                speedMultiplier = 3f;
                agent.speed *= speedMultiplier;
                playerDetected = false; // ป้องกันการเพิ่มความเร็วหลายครั้ง
            }
        }
    }
}
