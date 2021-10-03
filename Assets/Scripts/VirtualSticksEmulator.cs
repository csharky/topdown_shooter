using UnityEngine;

public class VirtualSticksEmulator : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(Application.isMobilePlatform);
    }
}