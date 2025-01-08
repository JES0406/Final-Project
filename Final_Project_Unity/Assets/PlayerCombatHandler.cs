using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine; 

public class PlayerCombatHandler : MonoBehaviour
{
    [SerializeField] private SphereCollider rangeCollider;
    private PlayerScript_Marcos playerScript;
    [SerializeField]  private List<GameObject> enemiesInRange = new List<GameObject>();
    private PlayerInputHandler inputHandler;
    private int? currentTargetIndex = null;

    [SerializeField] private ProjectileFactory projectileFactory;

    [SerializeField] private CrosshairManager crosshairManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyAIScript enemyScript))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    void Start()
    {
        //We need to get the PlayerScript_Marcos component from the player object
        playerScript = GetComponent<PlayerScript_Marcos>();
        rangeCollider = GetComponent<SphereCollider>();
        rangeCollider.radius = playerScript.getAttackRange();
        inputHandler = PlayerInputHandler.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.swapTargetInput)
        {
            GoNextIndex();
            UpdateCrosshair();
        }
        if (inputHandler.shootInput)
        {
            AttackCurrentTarget();
        }
    }
    void GoNextIndex()
    {
        if (currentTargetIndex == null)
        {
            currentTargetIndex = 0;
        }
        else
        {
            currentTargetIndex++;
            if (currentTargetIndex >= enemiesInRange.Count)
            {
                currentTargetIndex = 0;
            }
        }
    }
    void AttackCurrentTarget()
    {
        if (currentTargetIndex != null && enemiesInRange.Count > currentTargetIndex)
        {
            GameObject target = enemiesInRange[currentTargetIndex.Value];
            float damage = playerScript.getAttackDamage();
            bool isHoming = true; // or determine based on player state or weapon
            bool hasExplosionEffect = false; // or based on weapon properties

            projectileFactory.CreateProjectile(ProjectileType.Flame, target, damage, isHoming, hasExplosionEffect);
        }
    }
    void UpdateCrosshair()
    {
        crosshairManager.SetTarget(null);

        if (currentTargetIndex != null)

        {
            if (enemiesInRange.Count > currentTargetIndex)
            {
                GameObject target = enemiesInRange[currentTargetIndex.Value];
                crosshairManager.SetTarget(target.transform);
                Debug.Log("Target set" + target);
            }
        }
    }
}
