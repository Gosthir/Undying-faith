/*using UnityEngine;

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
}*/


using UnityEngine;
public class MeteorCollisionHandler : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab of the explosion object
    public GameObject newObjectPrefab; // Prefab of the new object to spawn
    public float damageRadius = 1f; // Radius of the damage area
    public float damageAmount = 10f; // Amount of damage to deal

    private bool hasCollided = false; // Flag to track collision

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided && (collision.gameObject.CompareTag("Player") ||
                             collision.gameObject.CompareTag("Enemy") ||
                             collision.gameObject.CompareTag("Floor")))
        {
            hasCollided = true; // Set collision flag

            // Spawn explosion
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Spawn new object at the meteor's position
            Instantiate(newObjectPrefab, transform.position, Quaternion.identity);

            // Deal damage in a radius around the collision point
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);
            foreach (Collider2D collider in colliders)
            {
                DealDamage(collider.gameObject);
            }

            // Destroy the meteor
            Destroy(gameObject);
        }
    }

    private void DealDamage(GameObject target)
    {
        // TODO: Implement your custom damage dealing logic here
        // You can access the target object and apply the damage as needed
        // For example:
        // - If the target is an enemy, you can reduce its health or trigger a specific behavior
        // - If the target is the player, you can reduce the player's health or apply any other effects
        // - If the target is the floor or any other object, you can decide how it should be affected by the meteor collision
    }
}