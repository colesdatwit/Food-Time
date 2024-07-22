using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
        public AudioClip pickUpFoodSound;
        public AudioClip placeFoodSound;
        public AudioClip workFoodSound;
        public AudioClip mixFoodSound;
        public AudioClip cookFoodSound;
        public AudioClip trashFoodSound;
        public AudioClip correct;
        public AudioClip wrong;
        public AudioClip knock;
        public AudioClip bgrMusic;

        public float volume;
        public float musicVolume;

        AudioSource SoundController;

    // Start is called before the first frame update
    void Start()
    {
        SoundController = GetComponent<AudioSource>();
        SoundController.volume = volume;
        PlayBackgroundMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPickUpFood()
    {
        AudioSource.PlayClipAtPoint(pickUpFoodSound, transform.position, volume);
    }
    public void PlayPlaceFood()
    {
        AudioSource.PlayClipAtPoint(placeFoodSound, transform.position, volume);
    }

    public void PlayWorkFood()
    {
        AudioSource.PlayClipAtPoint(workFoodSound, transform.position, volume);
    }

    public void PlayMixFood()
    {
        AudioSource.PlayClipAtPoint(mixFoodSound, transform.position, volume);
    }

    public void PlayCookFood()
    {
        AudioSource.PlayClipAtPoint(cookFoodSound, transform.position, volume);
    }

    public void PlayTrashFood()
    {
        AudioSource.PlayClipAtPoint(trashFoodSound, transform.position, volume*2);
    }

    public void PlayCorrect()
    {
        AudioSource.PlayClipAtPoint(correct, transform.position, volume);
    }

    public void PlayWrong()
    {
        AudioSource.PlayClipAtPoint(wrong, transform.position, volume);
    }

    public void PlayKnock()
    {
        AudioSource.PlayClipAtPoint(knock, transform.position, volume*3);
    }

    public void PlayBackgroundMusic()
    {
        SoundController.volume=musicVolume;
        SoundController.clip = bgrMusic;
        SoundController.loop = true;
        SoundController.Play();
    }

    public void StopPlayingMusic()
    {
        SoundController.Stop();
    }
}
