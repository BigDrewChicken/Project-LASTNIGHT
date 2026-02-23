using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    public bool isActive = true;
    private bool hasBeenActivated = false;

    [Header("Spawn Direction")]
    public bool spawnFacingRight = true;  

    private AudioSource audioSource;
    private SpriteRenderer checkpointSprite;

    [Header("Visuals")]
    public Sprite inactiveSprite;
    public Sprite activeSprite;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        checkpointSprite = GetComponent<SpriteRenderer>();

        if (checkpointSprite != null && inactiveSprite != null)
            checkpointSprite.sprite = inactiveSprite;

        if (audioSource != null)
            audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive || hasBeenActivated) return;

        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {

            playerController.SetRespawnPoint(transform.position, spawnFacingRight);

            if (audioSource != null)
                audioSource.Play();

            if (checkpointSprite != null && activeSprite != null)
                checkpointSprite.sprite = activeSprite;

            hasBeenActivated = true;
        }
    }

    public void ResetCheckpoint()
    {
        hasBeenActivated = false;

        if (checkpointSprite != null && inactiveSprite != null)
            checkpointSprite.sprite = inactiveSprite;
    }
}
