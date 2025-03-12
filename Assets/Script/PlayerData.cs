using UnityEngine;
using TMPro;
using System.Collections;
public class PlayerHP_Ammo : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHP = 100;
    private int currentHP;

    [Header("Ammo Settings")]
    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;

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
        if (Input.GetMouseButtonDown(0) && !isReloading) // LMB to fire
            Shoot();

        if (Input.GetKeyDown(KeyCode.R) && !isReloading) // Press R to reload
            StartCoroutine(Reload());
    }

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            Debug.Log("Bang! Ammo left: " + currentAmmo);
            UpdateUI();
        }
        else
        {
            Debug.Log("Out of ammo! Reload needed.");
        }
    }

    IEnumerator Reload()
    {
        if (currentAmmo == maxAmmo) yield break; // Skip if already full

        isReloading = true;
        ammoText.text = "Reloading";
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reloaded!");
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        if (currentHP <= 0)
            Die();

        UpdateUI();
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
    }
}
