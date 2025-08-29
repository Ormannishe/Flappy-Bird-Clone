// 2025-08-25 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class BirdCollision : MonoBehaviour
{
    public float minY = -5f; // The Y value below which the bird is considered "off-camera"
    public float maxY = 2.75f; // The Y value above which the bird is considered "off-camera"
    public AudioClip bonkSound;
    public AudioClip swooshSound;
    public AudioClip pointSound;
    private bool isGameOver = false;

    private GameManager gameManager;
    private AudioSource audioSource;

    void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindFirstObjectByType<GameManager>();
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check if the bird falls below the minimum Y value
        if (!isGameOver && (transform.position.y < minY || transform.position.y > maxY))
        {
            if (swooshSound != null)
            {
                audioSource.PlayOneShot(swooshSound);
            }

            GameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Trigger game over if the bird collides with anything
        if (!isGameOver)
        {
            if (bonkSound != null)
            {
                audioSource.PlayOneShot(bonkSound);
            }

            GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bird entered a scoring trigger zone
        if (other.CompareTag("ScoreZone"))
        {
            if (pointSound != null)
            {
                audioSource.PlayOneShot(pointSound);
            }

            gameManager.AddScore(1); // Add 1 to the score
        }
    }

    void GameOver()
    {
        isGameOver = true;
        gameManager.GameOver(); // Call the GameManager's GameOver method
    }
}