using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SoundSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start() 
    {
        musicSlider.value = PlayerPrefs.GetFloat("BG", 0);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 0);
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("BG", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }

    void OnDisable()
    {
        float BG = 0;
        float SFX = 0;

        audioMixer.GetFloat("BG", out BG);
        audioMixer.GetFloat("SFX", out SFX);

        PlayerPrefs.SetFloat("BG", BG);
        PlayerPrefs.SetFloat("SFX", SFX);
        PlayerPrefs.Save();
    }
}