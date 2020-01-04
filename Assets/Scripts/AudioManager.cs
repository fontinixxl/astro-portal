using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Drag a reference to the audio source which will play the sound effects.
    public AudioSource efxSource;
    public static AudioManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySfx(AudioClip clip)
    {
        efxSource.clip = clip;

        efxSource.Play();
    }

}
