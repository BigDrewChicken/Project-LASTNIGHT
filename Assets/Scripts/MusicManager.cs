using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Music Clips")]
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool isMenuScene = scene.name == "MainMenu" || scene.name == "LevelSelector";

        if (isMenuScene)
        {
            PlayMenuMusic();
        }
        else
        {
            PlayGameplayMusic();
        }
    }

    public void PlayMenuMusic()
    {
        if (audioSource.clip != menuMusic)
        {
            audioSource.clip = menuMusic;
            audioSource.Play();
        }
    }

    public void PlayGameplayMusic(bool restart = true)
    {
        if (restart || audioSource.clip != gameplayMusic)
        {
            audioSource.clip = gameplayMusic;
            audioSource.Play();
        }
    }

    public void PauseMusic()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
    }

    public void UnPauseMusic()
    {
        if (!audioSource.isPlaying)
            audioSource.UnPause();
    }
}
