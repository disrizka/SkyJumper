using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;

    // Sound Effects
    public AudioClip flapSound;
    public AudioClip hitSound;
    private AudioSource audioSource;

    // Batas layar
    private float topBound;
    private float bottomBound;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        
        // Jika tidak ada AudioSource, tambahkan
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Settings untuk AudioSource
        audioSource.playOnAwake = false;
        audioSource.volume = 0.5f;
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        
        // Hitung batas layar
        Camera mainCamera = Camera.main;
        topBound = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        bottomBound = mainCamera.ScreenToWorldPoint(Vector3.zero).y;
    }

    private void Update()
    {
        // Input untuk melompat
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
            
            // Play flap sound
            PlaySound(flapSound);
        }
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
                
                // Play flap sound
                PlaySound(flapSound);
            }
        }

        // Aplikasi gravity
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Cek batas atas - Game Over jika melewati
        if (transform.position.y > topBound)
        {
            GameOver();
        }

        // Cek batas bawah - Game Over jika jatuh
        if (transform.position.y < bottomBound)
        {
            GameOver();
        }
    }

    private void AnimateSprite()
    {
        spriteIndex++;
        
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        
        if (spriteRenderer != null && sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            GameOver();
        }
        else if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }

    private void GameOver()
    {
        // Play hit sound
        PlaySound(hitSound);
        
        FindObjectOfType<GameManager>().GameOver();
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}