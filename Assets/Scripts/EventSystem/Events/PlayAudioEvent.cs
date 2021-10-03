namespace EventSystem
{
    public struct PlayAudioEvent : IEventData
    {
        public AudioController.SoundType Type { get; set; }
    }
}