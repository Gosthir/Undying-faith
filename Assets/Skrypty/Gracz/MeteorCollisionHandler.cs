using UnityEngine;

public class MeteorCollisionHandler : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab of the explosion object

    private bool hasCollided = false; // Flag to track collision

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided && (collision.gameObject.CompareTag("Player") ||
                             collision.gameObject.CompareTag("Enemy") ||
                             collision.gameObject.CompareTag("Floor")))
        {
            hasCollided = true; // Set collision flag

            // Spawn explosion
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Destroy the explosion after a delay
            Destroy(explosion, 0.3f);

            // Destroy the meteor
            Destroy(gameObject);
        }
    }
}