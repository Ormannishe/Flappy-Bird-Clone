// 2025-08-25 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class BirdController : MonoBehaviour
{
    
    public float flapForce = 5f; // The upward force applied when the bird flaps
    // Rotation settings
    public float maxTiltAngle = 90f; // Maximum upward tilt angle
    public float minTiltAngle = -90f; // Maximum downward tilt angle
    public float tiltSpeed = 5f; // Speed at which the bird tilts
    // Sound effects
    public AudioClip flapSound;

    private Rigidbody2D rb;
    private GameManager gameManager;
    private AudioSource audioSource;

    void Start()
    {
        // Get the Rigidbody2D component attached to the bird
        rb = GetComponent<Rigidbody2D>();

        // Find the GameManager in the scene
        gameManager = FindFirstObjectByType<GameManager>();

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check for input (spacebar or mouse click)
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !gameManager.IsGameOver() && !gameManager.IsTitleScreenUp())
        {
            Flap();
        }

        // Adjust the bird's rotation based on its velocity
        AdjustRotation();
    }

    void Flap()
    {
        // Reset the bird's vertical velocity and apply an upward force
        rb.linearVelocity = Vector2.zero; // Reset velocity
        rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);

        if (flapSound != null)
        {
            audioSource.PlayOneShot(flapSound);
        }
    }

    void AdjustRotation()
    {
        // Get the bird's vertical velocity
        float verticalVelocity = rb.linearVelocity.y;

        // Calculate the target angle based on the velocity
        float targetAngle = Mathf.Lerp(minTiltAngle, maxTiltAngle, (verticalVelocity + 10f) / 20f);

        // Smoothly rotate the bird towards the target angle
        float smoothedAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, Time.deltaTime * tiltSpeed);

        // Apply the rotation
        transform.rotation = Quaternion.Euler(0f, 0f, smoothedAngle);
    }
}