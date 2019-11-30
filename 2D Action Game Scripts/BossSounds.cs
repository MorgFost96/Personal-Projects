using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour {

    private AudioSource source;

    public AudioClip[] clips;

    public float TimeBetweenSoundEffects;
    private float nexSoundEffectTime;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Time.time >= nexSoundEffectTime)
        {
            int RandomNumber = Random.Range(0, clips.Length);
            source.clip = clips[RandomNumber];
            source.Play();
            nexSoundEffectTime = Time.time + TimeBetweenSoundEffects;
        }
    }
}
