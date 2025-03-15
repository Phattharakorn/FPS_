using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    [SerializeField] private Transform playerTransform;

    private Transform target;
    public int Range;

    [Header("Health Settings")]
    public int maxHealth = 100; // Max HP
    private int currentHealth;  // Current HP
    private bool isDead = false; // Track if the enemy is dead

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Initialize HP
    }

    void Update()
    {
        if (isDead) return; // Prevent movement if dead

        if (Vector3.Distance(transform.position, playerTransform.position) < Range) // Detect player
        {
            target = playerTransform;
        }

        if (target != null)
        {
            agent.SetDestination(playerTransform.position);
            animator.SetBool("IsRunning", true); // Play running animation
        }
        else
        {
            animator.SetBool("IsRunning", false); // Play idle animation
        }
    }

    // 📌 Take Damage Function
    public void TakeDamage2(int damage)
    {
        if (isDead) return; // Ignore damage if already dead

        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. HP left: " + currentHealth);

        // 📌 Play hit animation (if exists)
        animator.SetTrigger("Hit");

        // 📌 Check if HP is zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 📌 Die Function
    private void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Enemy Died!");

        // 📌 Stop movement
        agent.isStopped = true;
        agent.enabled = false;

        // 📌 Play death animation (make sure your animator has a "Die" trigger)
        animator.SetTrigger("Die");

        // 📌 Destroy after animation
        StartCoroutine(DestroyAfterDeath());
    }

    private IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(3f); // Wait for animation to play
        Destroy(gameObject); // Remove enemy from scene
    }
}
