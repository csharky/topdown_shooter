using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class VhsEffectController : MonoBehaviour
{
    [SerializeField] private Vector2 _visibilityRange;
    [SerializeField] private Vector2 _comparisonRange;
    [SerializeField] private Vector2 _colorRange;
    [SerializeField] private Vector2 _backgroundColorRange;
    [SerializeField] private Material _material;
    [SerializeField] private float _value = -1f;

    public void SetValue(float value)
    {
        _material.SetFloat("_NoiseVisibility", _visibilityRange.x + (_visibilityRange.y - _visibilityRange.x) * value);
        _material.SetFloat("_ComparisonValue", _comparisonRange.x + (_comparisonRange.y - _comparisonRange.x) * value);
        _material.SetFloat("_NoiseColorSize", _colorRange.x + (_colorRange.y - _colorRange.x) * value);
        _material.SetFloat("_BackgroundColorValue", _backgroundColorRange.x + (_backgroundColorRange.y - _backgroundColorRange.x) * value);
    }

    public void SetNoisePartsScale(float value)
    {
        _material.SetFloat("_NoisePartsScale", value);
    }

    public void FixedUpdate()
    {
        if (_value < 0) return;
        
        SetValue(_value);
    }
}