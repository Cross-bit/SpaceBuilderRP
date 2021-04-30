using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Sound effect", menuName = "SoundEffect")]
public class SoundEffectSO : ScriptableObject
{
    [Tooltip("Název nahrávky")]
    new public Settings.SoundEffects name;
    [Tooltip("Nahrávka")]
    public AudioClip clip;
    [Tooltip("AudioMixer")]
    public AudioMixer mixer;
}

