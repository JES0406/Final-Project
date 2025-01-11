using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    [SerializeField] private List<ProjectileTypePrefabPair> projectilePrefabs;

    public static ProjectileFactory instance;

    private Dictionary<ProjectileType, GameObject> projectileDictionary;

    [SerializeField] private GameObject explosionEffect;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(instance);
        }
        projectileDictionary = new Dictionary<ProjectileType, GameObject>();
        foreach (var pair in projectilePrefabs)
        {
            if (!projectileDictionary.ContainsKey(pair.projectileType))
            {
                projectileDictionary.Add(pair.projectileType, pair.prefab);
            }
        }
    }
    public GameObject CreateProjectile(ProjectileType type, GameObject target, float damage, bool isHoming, bool hasExplosionEffect, GameObject? sender)
    {
        if (!projectileDictionary.TryGetValue(type, out GameObject prefab))
        {
            Debug.LogError($"Projectile type {type} not found in dictionary!");
            return null;
        }

        GameObject projectile = Instantiate(prefab);
        Debug.Log(sender); // No quitar, es un misterio pero sin esto no funciona
        Vector3 senderPos = sender != null ? sender.transform.position : Vector3.zero;
        // We spawn it 0.7 units up so it doesnt clip
        projectile.transform.position = new Vector3  (senderPos.x, senderPos.y + 1f, senderPos.z);
        projectile.AddComponent<Projectile>();
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        projectileScript.Initialize(target, damage, isHoming, hasExplosionEffect ? explosionEffect : null, sender.tag);
        SphereCollider sphereCollider = projectile.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        projectile.SetActive(true);

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