using UnityEngine;

public class PlayerCameraRecoil : MonoBehaviour
{
    [Header("Recoil Settings")]
    public float recoilAngle = 2f;      // Vertical recoil per shot
    public float maxRecoilAngle = 10f;  // Max tilt
    public float recoilReturnSpeed = 5f; // Speed of returning

    private float currentRecoil = 0f;

    void Update()
    {
        // Gradually return recoil to normal
        currentRecoil = Mathf.Lerp(currentRecoil, 0, Time.deltaTime * recoilReturnSpeed);

        // Apply recoil only on the X-axis (up/down tilt)
        transform.localRotation = Quaternion.Euler(-currentRecoil, transform.localRotation.eulerAngles.y, 0);
    }

    public void ApplyRecoil()
    {
        // Add recoil kick (clamp to max)
        currentRecoil = Mathf.Clamp(currentRecoil + recoilAngle, 0, maxRecoilAngle);
    }
}
