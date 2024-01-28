using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioClip musicMenuOffice;
    [SerializeField] private AudioClip musicGameplay;
    [Header("Gameplay Effects")]
    [SerializeField] private AudioClip waterSplash;
    [SerializeField] private AudioClip playerStun;
    [SerializeField] private AudioClip playerDash;
    [SerializeField] private AudioClip playerDrag;
    [SerializeField] private AudioClip playerDrop;
    [SerializeField] private AudioClip playerThrow;
    [SerializeField] private AudioClip completedObjectClean;
    [SerializeField] private AudioClip warningEvent;
    [SerializeField] private AudioClip breakObject;
    [SerializeField] private AudioClip spawnObject;
    [Header("UI Effects")]
    [SerializeField] private AudioClip completedAllCleans;
    [Header("References")]
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource effects;
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

    public void PlayMusicMainMenu()
    {
        if(music.clip == musicMenuOffice) return;
        music.clip = musicMenuOffice;
        music.Play();
    }
    
    public void PlayMusicGameplay()
    {
        if(music.clip == musicGameplay) return;
        music.clip = musicGameplay;
        music.Play();
    }
    
    public void PlayEffectWaterSplash()
    {
        effects.PlayOneShot(waterSplash);
    }
    
    public void PlayEffectPlayerStun()
    {
        effects.PlayOneShot(playerStun);
    }
    
    public void PlayEffectPlayerDash()
    {
        effects.PlayOneShot(playerDash);
    }
    
    public void PlayEffectPlayerThrow()
    {
        effects.PlayOneShot(playerThrow);
    }
    
    public void PlayEffectPlayerDrag()
    {
        effects.PlayOneShot(playerDrag);
    }
    
    public void PlayEffectPlayerDrop()
    {
        effects.PlayOneShot(playerDrop);
    }
    
    public void PlayEffectCompleteObjectClean()
    {
        effects.PlayOneShot(completedObjectClean);
    }
    
    public void PlayEffectCompleteAllClean()
    {
        effects.PlayOneShot(completedAllCleans);
    }
    
    public void PlayEffectWarningEvent()
    {
        effects.PlayOneShot(warningEvent);
    }
    
    public void PlayEffectSpawnObject()
    {
        effects.PlayOneShot(spawnObject);
    }
    
    public void PlayEffectBreakObject()
    {
        effects.PlayOneShot(breakObject);
    }
    
    public void StopMusic()
    {
        music.Stop();
    }
    public void StopEffects()
    {
        effects.Stop();
    }
}
