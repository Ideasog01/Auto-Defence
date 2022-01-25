using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blaster", menuName = "New Blaster")]
public class BlasterType : ScriptableObject
{
    [Header("Blaster Settings")]

    public float blasterCooldown;

    public int blasterMaxAmmo;

    public float blasterReloadTime;

    public Mesh blasterMesh;

    public Material blasterMaterial;

    [Header("Projectile Settings")]

    public GameObject projectilePrefab;

    public float projectileSpeed;

    public int projectileDamage;

    public Vector3 projectileSpawnPos;
}
