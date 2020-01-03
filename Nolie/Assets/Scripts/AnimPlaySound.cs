using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlaySound : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip[] sounds;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySouds(int soundIndex)
    {
        audioSource.PlayOneShot(sounds[soundIndex]);
    }
}
