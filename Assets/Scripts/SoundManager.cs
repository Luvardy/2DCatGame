using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public AudioSource action;
    public AudioSource cardPlace;
    public AudioSource enemy;
    public AudioSource allSwitch;

    public AudioSource ballCat;
    public AudioSource longCat;
    public AudioSource SamuraiCat;

    public static SoundManager instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;

    }

    public void PlaySingle(AudioClip clip)
    {
        action.clip = clip;
        action.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        action.clip = clips[randomIndex];
        action.Play();
    }

    public void PlaySingelCard(AudioClip clip)
    {
        cardPlace.clip = clip;
        cardPlace.Play();
    }

    public void PlaySingelEnemy(AudioClip clip)
    {
        enemy.clip = clip;
        enemy.Play();
    }

    public void PlaySingelSwitch(AudioClip clip)
    {
        allSwitch.clip = clip;
        allSwitch.Play();
    }

    public void PlaySingelBall(AudioClip clip)
    {
        ballCat.clip = clip;
        ballCat.Play();
    }

    public void PlaySingelLong(AudioClip clip)
    {
        longCat.clip = clip;
        longCat.Play();
    }

    public void PlaySingelSamurai(AudioClip clip)
    {
        SamuraiCat.clip = clip;
        SamuraiCat.Play();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}