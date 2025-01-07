using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIScript : MonoBehaviour
{
    public Vector3 destination;
    public Vector3 playerLastPosition;
    NavMeshAgent agent;
    Animator animator;

    public GameObject target;
    public bool hasTarget = false;

    public float health = 100f;
    public float attackDamage = 10f;
    public float attackRange = 2f;
    public float attackSpeed = 0.5f;
    public float attackTimer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        CheckPlayerPosition();
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination);
        animator = this.GetComponent<Animator>();
        animator.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerPosition();
        if (!hasTarget)
        {
            animator.SetBool("isMoving", false);
        }
        else if (hasTarget && target != null)
        {
            HandleTargetFollowing();
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void HandleTargetFollowing()
    {
        animator.SetBool("isMoving", true);

        agent.SetDestination(destination);
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            Attack();
            attackTimer = 1 / attackSpeed;
        }
    }

    void Attack()
    {
        if (Vector3.Distance(target.transform.position, gameObject.transform.position) < attackRange)
        {
            // play attack animation
            animator.SetBool("attack", true);

            // damage the target
            // target.GetComponent<PlayerScript_Marcos>().health -= attackDamage;
        }
    }

    void CheckPlayerPosition()
    {
        if (target != null)
        {
            // We raytrace from the enemy to the player to check if there are any obstacles in between
            RaycastHit hit;
            if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    updatePursuit();
                }
                else if(hit.collider.gameObject.tag == "Obstacle")
                {
                    obstacleHandling();
                }
            }
        }
    }

    void updatePursuit()
    {
        playerLastPosition = target.transform.position;
        destination = target.transform.position;
        hasTarget = true;
    }

    void obstacleHandling()
    {
        if (playerLastPosition != Vector3.negativeInfinity)
        {
            destination = playerLastPosition;
            playerLastPosition = Vector3.negativeInfinity;
        }
        else
        {
            hasTarget = false;
        }
    }
}
