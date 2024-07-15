using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
        public AudioClip placeFoodSound;

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

    public void PlayPlaceFood()
    {
        AudioSource.PlayClipAtPoint(placeFoodSound, transform.position, volume);
    }
}
