// 2025-08-25 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        // Move the pipes to the left
        transform.position += speed * Time.deltaTime * Vector3.left;

        // Destroy the pipes if they move off-screen
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}