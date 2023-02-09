using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    AudioMixer _audioMixer;
    public void SetMusicVolume(float volume)
    {
        Debug.Log(volume);
        _audioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetMasterVolume(float volume)
    {
        Debug.Log(volume);
        _audioMixer.SetFloat("MasterVolume", volume);
    }
    public void SetSFXcVolume(float volume)
    {
        Debug.Log(volume);
        _audioMixer.SetFloat("SFXVolume", volume);
    }
}
