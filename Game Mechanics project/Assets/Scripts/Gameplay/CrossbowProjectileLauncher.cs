using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowProjectileLauncher : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;
    [SerializeField] private GameObject projectilePrefab;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);

        // Flip the projectile's facing direction and movement based on the direction the character is facing at time of launch
        // Since the crossbow faces left by default, flip the direction in the function below.
        Vector3 originalScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(
            originalScale.x * transform.localScale.x < 0 ? 1 : -1,
            originalScale.y,
            originalScale.z
            );
    }
}
