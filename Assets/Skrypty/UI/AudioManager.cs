using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    public AudioClip GameMusic;
    public AudioClip Menu;
    public AudioClip Atak1;
    public AudioClip Atak2;
    public AudioClip Atak3;
    public AudioClip Block;
    public AudioClip DashSound;
    public AudioClip Duch;
    public AudioClip EnemyAttack1;
    public AudioClip EnemyAttack2;
    public AudioClip EnemyHit1;
    public AudioClip EnemyHit2;
    public AudioClip EnemyHit3;
    public AudioClip Laser;
    public AudioClip Meteor_Explosion;
    public AudioClip Odepchniecie;


    private void Start()
    {
        musicSource.clip = GameMusic;
        musicSource.Play();
    }


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
