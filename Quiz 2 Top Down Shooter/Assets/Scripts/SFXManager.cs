using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [Header("Audio Clips")]
    public AudioClip shootSound;
    public AudioClip strongShootSound;
    public AudioClip smallShootSound;
    public AudioClip playerDamageSound;
    public AudioClip enemyDamageSound;
    public AudioClip enemyDeathSound;
    public AudioClip levelUpSound;
    public AudioClip healSound;
    public AudioClip switchModeSound;

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlaySound(string clipName)
    {
        switch (clipName)
        {
            case "Shoot":
                audioSource.PlayOneShot(shootSound, 0.4f);
                break;
            case "StrongShoot":
                audioSource.PlayOneShot(strongShootSound);
                break;
            case "SmallShoot":
                audioSource.PlayOneShot(smallShootSound, 0.25f);
                break;
            case "PlayerDamage":
                audioSource.PlayOneShot(playerDamageSound);
                break;
            case "EnemyDamage":
                audioSource.PlayOneShot(enemyDamageSound, 0.5f);
                break;
            case "EnemyDeath":
                audioSource.PlayOneShot(enemyDeathSound);
                break;
            case "LevelUp":
                audioSource.PlayOneShot(levelUpSound);
                break;
            case "Heal":
                audioSource.PlayOneShot(healSound);
                break;
            case "SwitchMode":
                audioSource.PlayOneShot(switchModeSound);
                break;
        }
    }
}
