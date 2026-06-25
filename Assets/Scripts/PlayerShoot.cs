using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public Animator animator;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("shoot");
            Shoot(this.gameObject);
        }

    }

    void Shoot(GameObject player)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Destroy bullet after set time
        Destroy(bullet, 2f);
        
    }
}
