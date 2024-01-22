using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float shootingDelay;
    public float rotationSpeed;

    private PlayerAnimatorController animatorController;
    public bool isShooting = false;

    void Start()
    {
        animatorController = GetComponent<PlayerAnimatorController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isShooting)
        {
            StartCoroutine(ShootCoroutine());
            animatorController.TriggerAttackAnimation();
        }
    }

    IEnumerator ShootCoroutine()
    {
        isShooting = true;

        Vector3 mousePos = GetMouseWorldPosition();
        yield return new WaitForSeconds(shootingDelay);

        Vector3 shootDirection = (mousePos - bulletSpawnPoint.position).normalized;
        RotatePlayerTowards(shootDirection);

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        float angle = Mathf.Atan2(shootDirection.x, shootDirection.z) * Mathf.Rad2Deg; // Determines which way to fly
        bullet.transform.rotation = Quaternion.Euler(new Vector3(-90, angle, 0)); // Set Rotation
        bulletRb.velocity = new Vector3(shootDirection.x, 0f, shootDirection.z) * bulletSpeed; //Impulse
        

        yield return new WaitForSeconds(1f);
        isShooting = false;
    }

    void RotatePlayerTowards(Vector3 targetDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0f, targetDirection.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    Vector3 GetMouseWorldPosition() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);
        }

        // If the ray does not hit the ground plane, return Vector3.zero
        return Vector3.zero;
    }
}