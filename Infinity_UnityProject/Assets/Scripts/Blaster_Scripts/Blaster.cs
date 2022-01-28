using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Blaster : MonoBehaviour
{
    public BlasterType blasterType;

    private int _blasterAmmo;

    private bool _isActive;

    [SerializeField]
    private GameObject reloadPrompt;

    [SerializeField]
    private TextMeshProUGUI reloadText;

    private Transform projectileSpawn;

    private void Start()
    {
        _isActive = true;
        _blasterAmmo = blasterType.blasterMaxAmmo;

        projectileSpawn = this.transform.GetChild(0);
        reloadText.text = _blasterAmmo.ToString() + "/" + blasterType.blasterMaxAmmo.ToString();
        reloadPrompt.SetActive(false);

        this.GetComponent<MeshFilter>().mesh = blasterType.blasterMesh;
        this.GetComponent<MeshRenderer>().material = blasterType.blasterMaterial;
    }

    public void ChangeBlaster(BlasterType blasterPickup)
    {
        blasterType = blasterPickup;

        this.GetComponent<MeshFilter>().mesh = blasterType.blasterMesh;
        this.GetComponent<MeshRenderer>().material = blasterType.blasterMaterial;

        projectileSpawn.transform.localPosition = blasterType.projectileSpawnPos;

        _blasterAmmo = blasterType.blasterMaxAmmo;
        reloadText.text = _blasterAmmo.ToString() + "/" + blasterType.blasterMaxAmmo.ToString();

        reloadText.text = _blasterAmmo.ToString() + "/" + blasterType.blasterMaxAmmo.ToString();
    }

    public void PrimaryFire()
    {
        if(_isActive && _blasterAmmo > 0)
        {
            GameObject projectile = Instantiate(blasterType.projectilePrefab, projectileSpawn.position, this.transform.rotation);
            projectile.GetComponent<ProjectileMovement>().SetupProjectile(blasterType.projectileSpeed, blasterType.projectileDamage, true);
            _blasterAmmo--;

            _isActive = false;
            StartCoroutine(Cooldown());
        }

        if(_isActive && _blasterAmmo <= 0)
        {
            ReloadBlaster();
        }

        reloadText.text = _blasterAmmo.ToString() + "/" + blasterType.blasterMaxAmmo.ToString();
    }

    public void ReloadBlaster()
    {
        StartCoroutine(Reload());
        _isActive = false;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(blasterType.blasterCooldown);
        _isActive = true;
    }

    private IEnumerator Reload()
    {
        reloadPrompt.SetActive(true);
        yield return new WaitForSeconds(blasterType.blasterReloadTime);
        _isActive = true;
        _blasterAmmo = blasterType.blasterMaxAmmo;
        reloadPrompt.SetActive(false);
        reloadText.text = _blasterAmmo.ToString() + "/" + blasterType.blasterMaxAmmo.ToString();
    }

   
}
