using System;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioSourceView : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public AudioSource AudioSource => audioSource;
}