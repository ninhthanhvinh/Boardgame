using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider audioSlider;
    [SerializeField] Slider sfxSlider;

    public void SetMusicVolumn()
    {
        float volumn = audioSlider.value;
        audioMixer.SetFloat("musicVolumn", Mathf.Log10(volumn) * 20);
    }

    public void SetSFXVolumn()
    {
        float volumn = sfxSlider.value;
        audioMixer.SetFloat("sfxVolumn", Mathf.Log10(volumn) * 20);
    }

}
