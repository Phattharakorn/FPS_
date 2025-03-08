using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weapons;  // Array of weapon GameObjects
    private int currentWeaponIndex = 0;

    void Start()
    {
        SelectWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Switch weapon using number keys (1, 2, 3...)
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectWeapon(i);
            }
        }

        // Switch weapon using scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            NextWeapon();
        }
        else if (scroll < 0f)
        {
            PreviousWeapon();
        }
    }

    void SelectWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;  // Prevent invalid index

        // Disable all weapons
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == index);
        }

        currentWeaponIndex = index;
    }

    void NextWeapon()
    {
        int nextIndex = (currentWeaponIndex + 1) % weapons.Length;
        SelectWeapon(nextIndex);
    }

    void PreviousWeapon()
    {
        int prevIndex = (currentWeaponIndex - 1 + weapons.Length) % weapons.Length;
        SelectWeapon(prevIndex);
    }
}
