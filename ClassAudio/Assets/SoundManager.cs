using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip[] bgmClips, sfxClips;

    // Handle SIngleton Logic
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayBGM(bgmClips[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayBGM(AudioClip bgm)
    {
        AudioSource src = create2DAudioSource(bgm);
        src.Play();
    }

    AudioSource create3DAudioSource()
    {

    }

    AudioSource create2DAudioSource (AudioClip audioClip, float vol=1, bool loop = false)
    {
        AudioSource src = gameObject.AddComponent<AudioSource>();

        src.clip = audioClip;
        src.volume = vol;
        src.loop = true;

        return src;
    }

    
}
