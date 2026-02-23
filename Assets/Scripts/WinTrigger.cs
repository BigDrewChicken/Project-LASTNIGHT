using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    [Header("Player")]
    public GameObject player; 

    private bool triggered = false;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (other.gameObject != player) return;

        triggered = true;


        PlayerController controller = player.GetComponent<PlayerController>();
        if (controller != null)
            controller.StopPlayer();

 
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        if (audioSource != null)
            audioSource.Play();

        UnlockNextLevel();

        StartCoroutine(DestroyAfterSound());
    }

    private void UnlockNextLevel()
    {

        string sceneName = SceneManager.GetActiveScene().name; 
        if (sceneName.StartsWith("Level "))
        {
            string numberPart = sceneName.Substring(6); 
            if (int.TryParse(numberPart, out int currentLevel))
            {
                int nextLevel = currentLevel + 1;

                if (nextLevel <= 5) 
                {
                    LevelProgressManager.instance.UnlockLevel(nextLevel);
                }
            }
        }
    }

    private IEnumerator DestroyAfterSound()
    {
        float delay = 0.5f;
        if (audioSource != null && audioSource.clip != null)
            delay = audioSource.clip.length;

        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
