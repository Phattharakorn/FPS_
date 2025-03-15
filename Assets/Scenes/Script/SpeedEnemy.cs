using System.Collections;
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
    public int playerdetectradius;

    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    private bool isDead = false; // ตรวจสอบว่า Enemy ตายหรือยัง

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return; // ถ้าตายแล้วไม่ต้องทำอะไรต่อ

        if (Vector3.Distance(transform.position, playerTransform.position) < playerdetectradius)
        {
            if (!playerDetected)
            {
                playerDetected = true;
                timeSincePlayerDetected = 0f;
            }

            agent.SetDestination(playerTransform.position);
            animator.SetBool("IsRunning", true);
        }
        else
        {
            playerDetected = false;
            animator.SetBool("IsRunning", false);
        }

        if (playerDetected)
        {
            timeSincePlayerDetected += Time.deltaTime;
            animator.SetBool("IsRunning", true);

            if (timeSincePlayerDetected >= 2f)
            {
                speedMultiplier = 3f;
                agent.speed *= speedMultiplier;
                playerDetected = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // ถ้าตายแล้ว ไม่ต้องรับดาเมจอีก

        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. HP left: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Enemy died!");
        animator.SetTrigger("Die");  // เล่นอนิเมชันตาย
        agent.isStopped = true; // หยุดการเคลื่อนที่
        GetComponent<Collider>().enabled = false; // ปิด Collider

        StartCoroutine(DestroyAfterDeath());
    }

    IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(2f); // รอ 2 วินาทีหลังตาย
        Destroy(gameObject);
    }
}
