// 2025-08-28 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerTransform; // The transform of the layer
        public float parallaxSpeed;      // The speed at which this layer moves
    }

    public ParallaxLayer[] layers;       // Array of layers for parallax
    public float layerWidth = 20f;       // Width of each layer for tiling

    void Update()
    {
        foreach (ParallaxLayer layer in layers)
        {
            if (layer.layerTransform != null)
            {
                // Move the layer to the left at its parallax speed
                Vector3 newPosition = layer.layerTransform.position;
                newPosition.x -= layer.parallaxSpeed * Time.deltaTime;
                layer.layerTransform.position = newPosition;

                // Check if the layer has moved out of view and reset its position
                if (layer.layerTransform.position.x <= -layerWidth)
                {
                    newPosition.x += layerWidth * 2f; // Move it to the right to repeat
                    layer.layerTransform.position = newPosition;
                }
            }
        }
    }
}