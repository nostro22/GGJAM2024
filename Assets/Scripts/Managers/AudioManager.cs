using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioClip musicTraffic;
    [SerializeField] private AudioClip musicFootball;
    [SerializeField] private AudioClip musicAmbientFootball;
    [Header("Gameplay Effects")]
    [SerializeField] private AudioClip ballHit;
    [SerializeField] private AudioClip carRun;
    [SerializeField] private AudioClip explotionGranade;
    [SerializeField] private AudioClip playerHitWithCar;
    [SerializeField] private AudioClip ballgoal;
    [Header("References")]
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource effects;
    [SerializeField] private AudioSource musicAmbient;
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMusicFootball()
    {
        if(music.clip == musicTraffic) return;
        music.clip = musicFootball;
        music.Play();
    }
    
    public void PlayMusicAmbientFootball()
    {
        if(musicAmbient.clip == musicAmbientFootball) return;
        musicAmbient.clip = musicAmbientFootball;
        musicAmbient.Play();
    }
    
    public void PlayMusicTraffic()
    {
        if(music.clip == musicTraffic) return;
        music.clip = musicTraffic;
        music.Play();
    }
    
    public void PlayEffectBallHit()
    {
        effects.PlayOneShot(ballHit);
    }
    
    public void PlayEffectCarRun()
    {
        effects.PlayOneShot(carRun);
    }
        
    public void PlayEffectBombGranade()
    {
        effects.PlayOneShot(explotionGranade);
    }
    
    public void PlayEffectGoal()
    {
        effects.PlayOneShot(ballgoal);
    }

    public void PlayEffectPlayerHitWithCar()
    {
        effects.PlayOneShot(explotionGranade);
    }

    
    public void StopMusic()
    {
        music.Stop();
        musicAmbient.Stop();
    }
    public void StopEffects()
    {
        effects.Stop();
    }
}
