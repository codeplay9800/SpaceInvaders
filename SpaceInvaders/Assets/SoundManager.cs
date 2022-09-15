using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public List<AudioClip> movementSounds;
    public AudioClip playerShoot;
    public AudioClip BossMove;
    public AudioClip playerDie;
    public AudioClip AlienDie;


    public AudioSource movementSoundSrc;
    public AudioSource playerAudioSrc;
    public AudioSource BossAudioSrc;
    public AudioSource MiscAudioSrc;
    int currSound = 0;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        InitSingleton();
    }

    void InitSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMoveSound()
    {
        currSound = (currSound + 1) % movementSounds.Count;
        movementSoundSrc.clip = movementSounds[currSound];
        movementSoundSrc.Play();
    }

    public void PlayerShootSound()
    {
        playerAudioSrc.clip = playerShoot;
        playerAudioSrc.Play();
    }

    public void PlayerDieSound()
    {
        MiscAudioSrc.clip = playerDie;
        MiscAudioSrc.Play();
    }
    public void AlienDieSound()
    {
        MiscAudioSrc.clip = AlienDie;
        MiscAudioSrc.Play();
    }

    public void PlayBossSound()
    {
        BossAudioSrc.clip = BossMove;
        BossAudioSrc.Play();
    }
    public void StopBossSound()
    {
        BossAudioSrc.Stop();
    }

}
