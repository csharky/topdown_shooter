using System;
using UnityEngine;

public class ObjectShadow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    private Transform _parentTransform;
    private Transform _selfTransform;

    private void Awake()
    {
        _selfTransform = transform;
        _parentTransform = _selfTransform.parent;
    }

    private void LateUpdate()
    {
        _selfTransform.position = _parentTransform.position + _offset;
    }
}