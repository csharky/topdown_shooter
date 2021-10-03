using System;
using UnityEngine;

namespace FootstepControllers
{
    public class BloodFootstepController : BaseFootstepController
    {
        [SerializeField] private Transform _leftFootstepTransform;
        [SerializeField] private Transform _rightFootstepTransform;

        [SerializeField] private BloodFootstepSpriteController _bloodFootstepPrefab;
        [SerializeField] private Sprite[] _bloodSprites;
        [SerializeField] private CollisionTriggerListener _collisionTriggerListener;
        [SerializeField] private LayerMask _bloodDispatcherLayers;

        private int _leftFootsteps = 0;

        private void Awake()
        {
            _collisionTriggerListener.OnTriggerExit += TriggerExited;
        }

        private void Start()
        {
            Pool.Current.AddToPool(_bloodFootstepPrefab);
        }

        private void TriggerExited(Collider2D other)
        {
            if (!_bloodDispatcherLayers.Contains(other.gameObject.layer)) return;
            var bloodDispatcher = other.gameObject.GetComponent<BloodDispatcher>();
            if (bloodDispatcher == null) return;

            _leftFootsteps = _bloodSprites.Length;
        }

        public override void OnFootstep(AnimatorFootstepListener.FootstepSide side)
        {
            if (!enabled || _leftFootsteps <= 0) return;

            var index = _bloodSprites.Length - _leftFootsteps;

            var footstep = Pool
                .Current
                .Get(_bloodFootstepPrefab)
                .SpriteRenderer;
            var footstepTransform = footstep.transform;
            footstepTransform.position =
                side == AnimatorFootstepListener.FootstepSide.Left
                    ? _leftFootstepTransform.position
                    : _rightFootstepTransform.position;
            footstepTransform.rotation = transform.rotation;
            footstep.flipX = side == AnimatorFootstepListener.FootstepSide.Right;
            footstep.sprite = _bloodSprites[index];

            _leftFootsteps--;
        }
    }
}