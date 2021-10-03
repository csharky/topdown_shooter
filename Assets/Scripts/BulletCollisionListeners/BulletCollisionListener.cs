using UnityEngine;

public class BulletCollisionListener : MonoBehaviour
{
    public virtual void OnBeforeFrameCollision(BulletController bullet, RaycastHit2D raycastHit2D)
    {
    }

    public virtual void OnCollision(BulletController bullet, RaycastHit2D raycastHit2D)
    {
    }
}