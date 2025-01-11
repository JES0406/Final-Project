using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;
    private Vector3 targetPosition;
    private float damage = 0f;
    private bool isHoming = false;
    private GameObject? explosionEffect = null;
    private float speed = 10f;
    private string senderTag;

    public void Initialize(GameObject target, float damage, bool isHoming, GameObject? explosionEffect, string senderTag)
    {
        this.target = target;
        this.damage = damage;
        this.isHoming = isHoming;
        this.explosionEffect = explosionEffect;
        this.senderTag = senderTag;

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
    private void OnTriggerEnter(Collider collision)
    {
        // Apply damage only if the collided object is the target
        if (collision.gameObject == target)
        {
            // Apply damage logic
            Debug.Log($"Hit target with {damage} damage.");
        }

        // Trigger explosion effect if applicable
        if (collision.gameObject.tag != senderTag)
        {
            Debug.Log(collision.gameObject);

            if (explosionEffect != null)
            {
                GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(explosion, 1f);
                Debug.Log("Explotó"); explosionEffect = null;
            }
            Destroy(this.gameObject);
            Debug.Log("Explota");
        }
    }

}
