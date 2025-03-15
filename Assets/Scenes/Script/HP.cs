using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [Header("Health Item Settings")]
    public int healAmount = 20; // Amount of health to heal the player

    [Header("Layer Setup")]
    public LayerMask playerLayer; // A LayerMask to specify which layer the player belongs to
    PlayerHP_Ammo playerHP;
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the other object is on the "Player" layer
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            // Get the PlayerHP_Ammo component from the player object that triggered the collider
            PlayerHP_Ammo playerHP = other.GetComponent<PlayerHP_Ammo>();

            // Null check to avoid NullReferenceException
                // Heal the player if their health is not at max
                if (playerHP.currentHP < playerHP.maxHP)
                {
                    playerHP.Heal(healAmount);

                    // Destroy the health item after it's picked up
                    Destroy(gameObject);
                }
               
        }
    }
}
