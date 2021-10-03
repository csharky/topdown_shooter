using System;
using UnityEngine;

public class BloodFootstepSpriteController : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    public void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}