using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CursorView : MonoBehaviour
{
    public static CursorView current => _cursors.FirstOrDefault();
    private static List<CursorView> _cursors;
    
    public Transform Transform => _transform;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        if (_cursors == null) 
            _cursors = new List<CursorView>();
        
        _cursors.Add(this);
    }

    private void OnDisable()
    {
        _cursors.Remove(this);
    }
}