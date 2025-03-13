using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private KeyScript.KeyType KeyType;

    public KeyScript.KeyType GetKeyType()
    {
        return KeyType;
    }

    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}
