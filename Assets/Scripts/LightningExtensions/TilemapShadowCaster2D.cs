using UnityEngine;

namespace LightningExtensions
{
    public class TilemapShadowCaster2D : MonoBehaviour
    {
        [SerializeField]
        protected CompositeCollider2D m_TilemapCollider;
 
        [SerializeField]
        protected bool m_SelfShadows = true;
 
        protected virtual void Reset()
        {
            m_TilemapCollider = GetComponent<CompositeCollider2D>();
        }
 
        protected virtual void Awake()
        {
            ShadowCaster2DGenerator.GenerateTilemapShadowCasters(m_TilemapCollider, m_SelfShadows);
        }
    }
}