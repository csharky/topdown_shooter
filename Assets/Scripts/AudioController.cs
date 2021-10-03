using System;
using System.Collections.Generic;
using System.Linq;
using EventSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AudioController : MonoBehaviour, IEventListener<PlayAudioEvent>
{
    public enum SoundType
    {
        Shoot_Rifle
    }

    [SerializeField] private AudioSourceView audioSourcePrefab;
    [SerializeField] private List<AudioClipData> _audioClipsData;
    
    private IDictionary<SoundType, List<AudioClip>> _audioClips;

    private void Start()
    {
        GameEventSystem.current.Register(this);
        Pool.Current.AddToPool(audioSourcePrefab);

        _audioClips = _audioClipsData.ToDictionary(_ => _.Type, _ => _.AudioClips);
    }

    public void Invoke(PlayAudioEvent eventData)
    {
        var audioClips = _audioClips[eventData.Type];
        var prefab = Pool.Current.Get(audioSourcePrefab);
        prefab.AudioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        prefab.AudioSource.Play();
    }
    
    [Serializable]
    internal class AudioClipData
    {
        public SoundType Type;
        public List<AudioClip> AudioClips;
    }

    private void OnDestroy()
    {
        Dispose();
    }

    public void Dispose()
    {
        GameEventSystem.current.Unregister(this);
    }
}