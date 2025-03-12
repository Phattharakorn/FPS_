using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] private Transform playerTransform;

    Transform traget;

    public int maxHealth = 100; // เลือดเริ่มต้น
    public int currentHealth; // เลือดปัจจุบัน
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth; // กำหนดค่าเลือดเริ่มต้น
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < 5)// เปลี่ยนเลขระยะได้
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
        Destroy(gameObject); // ทำลาย GameObject ของ Agent
    }
}
