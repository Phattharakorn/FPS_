using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerHP_Ammo : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHP = 100;
    public int currentHP;

    [Header("Ammo Settings")]
    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;

    [Header("Raycast Settings")]
    public Transform raycastOrigin;
    public float shootRange = 100f;
    public LayerMask enemyLayer;
    public bool debugRaycast = false;

    [Header("Bullet Trail Settings")]
    public LineRenderer bulletTrailPrefab;
    public float bulletTrailDuration = 0.05f;

    [Header("Damage Settings")]
    public int collisionDamage = 10;
    public int collisionDamage2 = 40;
    // Damage taken from enemy collisions
    public float damageCooldown = 1.5f;  // Cooldown before taking damage again
    private bool canTakeDamage = true;  // To track cooldown state

    [Header("UI References")]
    public TMP_Text hpText;
    public TMP_Text ammoText;

    void Start()
    {
        currentHP = maxHP;
        currentAmmo = maxAmmo;
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();

        if (Input.GetMouseButtonDown(0) && !isReloading)
            Shoot();

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            StartCoroutine(Reload());
    }

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            UpdateUI();

            RaycastHit hit;
            Vector3 startPos = raycastOrigin ? raycastOrigin.position : Camera.main.transform.position;
            Vector3 direction = raycastOrigin ? raycastOrigin.forward : Camera.main.transform.forward;
            Vector3 shootEndPos = startPos + direction * shootRange;

            if (Physics.Raycast(startPos, direction, out hit, shootRange, enemyLayer))
            {
                shootEndPos = hit.point;

                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<Enemy>().TakeDamage(10);
                    Debug.Log("Enemy hit! Damage applied.");
                }
            }

            StartCoroutine(ShowBulletTrail(startPos, shootEndPos));

            if (debugRaycast)
                Debug.DrawLine(startPos, shootEndPos, Color.red, 0.2f);
        }
        else
        {
            Debug.Log("Out of ammo! Reload needed.");
        }
    }

    IEnumerator ShowBulletTrail(Vector3 startPos, Vector3 endPos)
    {
        LineRenderer bulletTrail = Instantiate(bulletTrailPrefab, startPos, Quaternion.identity);
        bulletTrail.SetPosition(0, startPos);
        bulletTrail.SetPosition(1, endPos);
        bulletTrail.enabled = true;

        yield return new WaitForSeconds(bulletTrailDuration);

        Destroy(bulletTrail.gameObject);
    }

    IEnumerator Reload()
    {
        if (currentAmmo == maxAmmo) yield break;

        isReloading = true;
        ammoText.text = "Reloading";
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;  // Prevent damage if cooldown is active

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        Debug.Log("Player took damage: " + damage);

        if (currentHP <= 0)
            Die();

        UpdateUI();
        StartCoroutine(DamageCooldown()); // Start cooldown
    }

    public void TakeDamage2(int damage)
    {
        if (!canTakeDamage) return;  // Prevent damage if cooldown is active

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        Debug.Log("Player took damage: " + damage);

        if (currentHP <= 0)
            Die();

        UpdateUI();
        StartCoroutine(DamageCooldown()); // Start cooldown
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    void Die()
    {
        Debug.Log("Player has died!");
        gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        if (hpText) hpText.text = "HP: " + currentHP;
        if (ammoText) ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
        if (isReloading) ammoText.text = "Reloading";
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && canTakeDamage)
        {
            TakeDamage(collisionDamage);
        }
    }
}
