using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sounds[] musicSounds, sfxSounds, sfxLoopSounds;
    public AudioSource musicSource, sfxSource, sfxLoopSource;

    public Slider sliderMaster;
    public float sliderValueMaster;
    public Image muteImageMaster;

    //public Slider sliderMusic;
    //public float sliderValueMusic;
    //public Image muteImageMusic;

    //public Slider sliderSfx;
    //public float sliderValueSfx;
    //public Image muteImageSfx;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        sliderMaster.value = PlayerPrefs.GetFloat("volumenMaster", 0.5f);
        AudioListener.volume = sliderMaster.value;
        CheckMuteMaster();

        //sliderMusic.value = PlayerPrefs.GetFloat("volumenMusic", 0.5f);
        //musicSource.volume = sliderMusic.value;
        //CheckMuteMusic();

        //sliderSfx.value = PlayerPrefs.GetFloat("volumenSfx", 0.5f);
        //sfxSource.volume = sliderSfx.value;
        //sfxLoopSource.volume = sliderSfx.value;
        //wtSource.volume = sliderSfx.value;
        //ambientSource.volume = sliderMusic.value;

        //CheckMuteSfx();
    }

    private void Update()
    {
    }

    #region "Menu Opciones"
    public void ChangeSliderMaster(float value)
    {
        sliderValueMaster = value;
        PlayerPrefs.SetFloat("volumenMaster", sliderValueMaster);
        AudioListener.volume = sliderValueMaster;
        musicSource.volume = sliderValueMaster;
        sfxSource.volume = sliderValueMaster;
        sfxLoopSource.volume = sliderValueMaster;
        CheckMuteMaster();
    }

    //public void ChangeSliderMusic(float value)
    //{
    //    sliderValueMusic = value;
    //    PlayerPrefs.SetFloat("volumenMusic", sliderValueMusic);
    //    musicSource.volume = sliderValueMusic;
    //    ambientSource.volume = sliderValueMusic;
    //    CheckMuteMusic();
    //}

    //public void ChangeSliderSfx(float value)
    //{
    //    sliderValueSfx = value;
    //    PlayerPrefs.SetFloat("volumenSfx", sliderValueSfx);
    //    sfxSource.volume = sliderValueSfx;
    //    sfxLoopSource.volume = sliderValueSfx;
    //    wtSource.volume = sliderValueSfx;

    //    CheckMuteSfx();
    //}

    public void CheckMuteMaster()
    {
        if (sliderValueMaster == 0)
        {
            muteImageMaster.enabled = true;
        }
        else
        {
            muteImageMaster.enabled = false;
        }
    }

    //public void CheckMuteMusic()
    //{
    //    if (sliderValueMusic == 0)
    //    {
    //        muteImageMusic.enabled = true;
    //    }
    //    else
    //    {
    //        muteImageMusic.enabled = false;
    //    }
    //}
    //public void CheckMuteSfx()
    //{
    //    if (sliderValueSfx == 0)
    //    {
    //        muteImageSfx.enabled = true;
    //    }
    //    else
    //    {
    //        muteImageSfx.enabled = false;
    //    }
    //}
    #endregion
    public void PlayMusic(string name)
    {
        Sounds s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sounds s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlaySFXLoop(string name)
    {
        Sounds s = Array.Find(sfxLoopSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxLoopSource.clip = s.clip;
            sfxLoopSource.Play();
        }
    }

    public void PauseSFX()
    {
        sfxSource.Pause();
    }
    public void ResumeSFX()
    {
        sfxSource.UnPause();
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    }

    public void PauseSFXLoop()
    {
        sfxLoopSource.Pause();
    }
    public void ResumeSFXLoop()
    {
        sfxLoopSource.UnPause();
    }
    //public void StopSFXLoop()
    //{
    //    StartCoroutine(FadeOut(sfxLoopSource, sliderSfx));
    //    //sfxLoopSource.Stop();
    //}

    public void PauseMusic()
    {
        musicSource.Pause();
    }
    public void ResumeMusic()
    {
        musicSource.UnPause();
    }
    //public void StopMusic()
    //{
    //    StartCoroutine(FadeOut(musicSource, sliderMusic));
    //    //musicSource.Stop();
    //}

    //public void ButtonEnter()
    //{
    //    PlaySFX("ButtonEnter");
    //}
    //public void ButtonSelected()
    //{
    //    PlaySFX("ButtonSelected");
    //}

    public bool IsClipPlaying(string clipName)
    {
        Sounds s = Array.Find(musicSounds, x => x.name == clipName);
        if (s == null)
        {
            return false;
        }

        return musicSource.clip == s.clip && musicSource.isPlaying;
    }

    //private IEnumerator Fade(AudioSource audioSource)
    //{
    //    float timeToFade = 0.25f;
    //    float timeElapsed = 0;
    //    float startVolume = sliderMusic.value;

    //    while (timeElapsed < timeToFade)
    //    {
    //        audioSource.volume = Mathf.Lerp(startVolume, 0, timeElapsed / timeToFade);
    //        timeElapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    audioSource.Stop();
    //    //musicSource.clip = musicClip;
    //    audioSource.Play();

    //    timeElapsed = 0;

    //    while (timeElapsed < timeToFade)
    //    {
    //        audioSource.volume = Mathf.Lerp(0, startVolume, timeElapsed / timeToFade);
    //        timeElapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //}

    //private IEnumerator FadeOut(AudioSource audioSource, Slider sliderValue)
    //{
    //    float timeToFade = 0.35f;
    //    float timeElapsed = 0;
    //    float startVolume = sliderValue.value;

    //    while (timeElapsed < timeToFade)
    //    {
    //        audioSource.volume = Mathf.Lerp(startVolume, 0, timeElapsed / timeToFade);
    //        timeElapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    audioSource.Stop();

    //    audioSource.volume = sliderValue.value;
    //    yield return null;
    //}
}
