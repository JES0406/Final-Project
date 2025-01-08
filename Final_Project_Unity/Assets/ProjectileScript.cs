using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;
    private Vector3 targetPosition;
    private float damage = 0f;
    private bool isHoming = false;
    private GameObject? explosionEffect = null;
    private float speed = 10f;

    public void Initialize(GameObject target, float damage, bool isHoming, GameObject? explosionEffect)
    {
        this.target = target;
        this.damage = damage;
        this.isHoming = isHoming;
        this.explosionEffect = explosionEffect;
    }

    private void Update()
    {
        if (target != null)
        {
            if (isHoming)
            {
                // Homing logic: Move towards the target
                targetPosition = target.transform.position;
            }
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Apply damage only if the collided object is the target
        if (collision.gameObject == target)
        {
            // Apply damage logic
            Debug.Log($"Hit target with {damage} damage.");
        }

        // Trigger explosion effect if applicable
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(explosion, 1f);
        }

        // Destroy the projectile upon any collision
        Destroy(gameObject);
    }

}
