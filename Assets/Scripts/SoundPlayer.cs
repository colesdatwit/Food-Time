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

        public float volume;

        AudioSource SoundController;

    // Start is called before the first frame update
    void Start()
    {
        SoundController = GetComponent<AudioSource>();
        SoundController.volume = volume;
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
}
