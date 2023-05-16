using UnityEngine;

public class MeteorCollisionHandler : MonoBehaviour
{
    public HeroKnight player;
    public Enemy enemy;
    public GameObject explosionPrefab; // Prefab of the explosion object
    public GameObject newObjectPrefab; // Prefab of the new object to spawn
    public float damageRadius = 1f; // Radius of the damage area
    public float damageAmount = 40f; // Amount of damage to deal

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

            // Spawn new object at the meteor's position
            Instantiate(newObjectPrefab, transform.position, Quaternion.identity);

            // Deal damage in a radius around the collision point
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);
            foreach (Collider2D collider in colliders)
            {
                // Deal damage to the collider's GameObject
                DealDamage(collider.gameObject);
            }

            // Destroy the meteor
            Destroy(gameObject);

            // Destroy the explosion after 0.3 seconds
            Destroy(explosion, 0.3f);
        }
    }

    private void DealDamage(GameObject target)
    {
        // Apply damage to the target GameObject
        // TODO: Replace this with your own damage handling logic
        // For example, if the target has a Health component, you can reduce its health:
        player.currentHealth -= damageAmount / 5;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damageAmount);
        }
    }
}



/*
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
} */