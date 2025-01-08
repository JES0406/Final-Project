using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    [SerializeField] private List<ProjectileTypePrefabPair> projectilePrefabs;

    private Dictionary<ProjectileType, GameObject> projectileDictionary;

    [SerializeField] private GameObject explosionEffect;

    private void Awake()
    {
        projectileDictionary = new Dictionary<ProjectileType, GameObject>();
        foreach (var pair in projectilePrefabs)
        {
            if (!projectileDictionary.ContainsKey(pair.projectileType))
            {
                projectileDictionary.Add(pair.projectileType, pair.prefab);
            }
        }
    }
    public GameObject CreateProjectile(ProjectileType type, GameObject target, float damage, bool isHoming, bool hasExplosionEffect)
    {
        if (!projectileDictionary.TryGetValue(type, out GameObject prefab))
        {
            Debug.LogError($"Projectile type {type} not found in dictionary!");
            return null;
        }

        GameObject projectile = Instantiate(prefab);
        projectile.AddComponent<Projectile>();
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        projectileScript.Initialize(target, damage, isHoming, hasExplosionEffect ? explosionEffect : null);

        return projectile;
    }

    [System.Serializable]
    public class ProjectileTypePrefabPair
    {
        public ProjectileType projectileType;
        public GameObject prefab;
    }

}
public enum ProjectileType
{
    Flame
}