using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;

    [SerializeField] private Transform playerTransform;
    private Transform target;

    [Header("Boss Settings")]
    public int detectionRadius = 10; // ระยะที่บอสตรวจจับผู้เล่น
    public int maxHealth = 200; // ค่า HP สูงกว่าศัตรูทั่วไป
    private int currentHealth;
    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return; // ถ้าตายแล้ว ไม่ต้องทำอะไรต่อ

        if (Vector3.Distance(transform.position, playerTransform.position) < detectionRadius)
        {
            target = playerTransform;
        }

        if (target != null)
        {
            agent.SetDestination(playerTransform.position);
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // ถ้าตายแล้ว ไม่ต้องรับดาเมจอีก

        currentHealth -= damage;
        Debug.Log("Boss took " + damage + " damage. HP left: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Boss has died!");

        animator.SetTrigger("Die"); // เล่นอนิเมชันตาย
        agent.isStopped = true; // หยุดการเคลื่อนที่
        GetComponent<Collider>().enabled = false; // ปิด Collider

        StartCoroutine(DestroyAfterDeath());
    }

    IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(3f); // รอ 3 วินาทีก่อนทำลาย
        Destroy(gameObject);
    }

    // Detect collision with the player and make the player die
    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the player
        if (other.CompareTag("Player"))
        {
            // Assuming your Player script has a TakeDamage or Die method
            PlayerHP_Ammo player = other.GetComponent<PlayerHP_Ammo>();
            if (player != null)
            {
                player.TakeDamage(100); // Adjust damage value as needed
                Debug.Log("Player hit by Boss!");

                // You could also have additional logic here for cooldowns or effects
            }
        }
    }
}
