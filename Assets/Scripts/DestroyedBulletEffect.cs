using UnityEngine;

public class DestroyedBulletEffect : MonoBehaviour
{
    public void OnDestructionFinished()
    {
        gameObject.SetActive(false);
    }
}