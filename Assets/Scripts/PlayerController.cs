using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 8f;
    public float jumpingPower = 16f;

    private float horizontalInput;
    [SerializeField] private bool isFacingRight = true; 
    private bool canMove = true;

    [Header("Ground Check")]
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;
    public Vector2 groundCheckSize = new Vector2(0.6f, 0.1f);

    [Header("Hazard Settings")]
    [SerializeField] private LayerMask spikeLayer;

    [Header("Respawn Settings")]
    [SerializeField] private Vector3 respawnPoint = new Vector3(-7.515f, -3.616f, 0f);
    private bool respawnFacingRight;

    [Header("Death Settings")]
    public GameObject deathAnimationPrefab;
    public AudioClip deathSFX;
    public float respawnDelay = 1f;

    [Header("Components")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    private bool deathSfxPlayed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetFacingDirection(isFacingRight, true);
        respawnFacingRight = isFacingRight;
    }

    private void Update()
    {
        if (!canMove) return;

        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (animator != null)
            animator.SetFloat("HorizontalInput", Mathf.Abs(horizontalInput));

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);

        HandleFlip();
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canMove) return;

        if (((1 << collision.gameObject.layer) & spikeLayer) != 0)
            StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        canMove = false;

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        rb.linearVelocity = Vector2.zero;
        horizontalInput = 0f;

        if (animator != null)
            animator.enabled = false;

        if (MusicManager.instance != null)
            MusicManager.instance.PauseMusic();

        if (deathAnimationPrefab != null)
            Instantiate(deathAnimationPrefab, transform.position, Quaternion.identity);

        if (!deathSfxPlayed && deathSFX != null)
        {
            AudioSource.PlayClipAtPoint(deathSFX, transform.position);
            deathSfxPlayed = true;
        }

        yield return new WaitForSeconds(respawnDelay);

        RespawnPlayer();
    }

    private void RespawnPlayer()
    {
        transform.position = respawnPoint;
        SetFacingDirection(respawnFacingRight, true);

        rb.linearVelocity = Vector2.zero;

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        if (animator != null)
            animator.enabled = true;

        if (MusicManager.instance != null)
            MusicManager.instance.UnPauseMusic();

        canMove = true;
        deathSfxPlayed = false;
    }

    private void HandleFlip()
    {
        if (!canMove) return;

        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
            SetFacingDirection(!isFacingRight);
    }

    public void SetFacingDirection(bool faceRight, bool force = false)
    {
        if (!force && isFacingRight == faceRight) return;

        isFacingRight = faceRight;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (isFacingRight ? 1 : -1);
        transform.localScale = scale;
    }

    public void SetRespawnPoint(Vector3 newRespawnPoint, bool facingRight)
    {
        respawnPoint = newRespawnPoint;
        respawnFacingRight = facingRight;
    }

    public Vector3 GetRespawnPoint() => respawnPoint;

    public void StopPlayer()
    {
        canMove = false;
        horizontalInput = 0f;

        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        if (animator != null)
            animator.SetFloat("HorizontalInput", 0f);
    }
}
