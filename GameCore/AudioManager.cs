using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Hudba na pozadí")]
    public AudioSource music;

    public AudioClip[] bgMusicClips;

    [Header("Zvukové efekty")]
    public static AudioSource soundEffectSource; // Tady jednotlivě, protože budou přiřazovány jednotlivě... TODO: zatím přřazeno ručně domyslet lépe


    [Header("Všechny zvukové efekty")]
    public SoundEffectSO[] audioClips;

    private void Awake(){
        // Zajistíme hlavní sound effect source
        if (soundEffectSource == null)
        {
            soundEffectSource = this.gameObject.GetComponent<AudioSource>();

            if (soundEffectSource == null) // Pokud je stále null
            {
                // Tak ho vytvoříme, aby bylo jisto ;)
                soundEffectSource = this.gameObject.AddComponent<AudioSource>();
            }
        }


        // Načteme veškerou bg hudbu
        bgMusicClips = Resources.LoadAll<AudioClip>(Settings.MUSIC_PATH);


        audioClips = Resources.LoadAll<SoundEffectSO>(Settings.SOUND_PATH);

        //Debug.Log(Resources.LoadAll<SoundEffectSO>(Settings.SOUND_PATH));

        // Načteme veškeré zvukové efekty
        foreach (SoundEffectSO ac in audioClips) {
            // Debug.Log(Settings.SOUND_PATH + "/mouse_click");
            if (ac.clip == null)
            {
                if (ac.name == Settings.SoundEffects.mouse_click_1)
                    ac.clip = Resources.Load(Settings.SOUND_PATH + "/mouse_click") as AudioClip;
                else if (ac.name == Settings.SoundEffects.button_press_1)
                    ac.clip = Resources.Load(Settings.SOUND_PATH + "/button_press") as AudioClip;
            }

        }
    }

    public void Play(Settings.SoundEffects soundEffect)  {
        soundEffectSource.PlayOneShot(GetClip(soundEffect).clip);
    }

    public SoundEffectSO GetClip(Settings.SoundEffects soundEffect)
    {
        if (audioClips != null)
        {
            foreach (SoundEffectSO ac in audioClips)
            {
                if (ac.name == soundEffect)
                    return ac;
            }
        }

        return null;
    }


}
