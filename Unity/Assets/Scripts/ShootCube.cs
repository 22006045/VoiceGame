using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCube : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPos;
    [SerializeField ]private float bulletSpeed;
    [SerializeField] private float timeBetweenShots;

    private void Start() => StartCoroutine(ShootForward(timeBetweenShots));
    private IEnumerator ShootForward(float nextShot)
    {
        Debug.Log("Shooting Bullet");
        GameObject b = Instantiate(bullet, shootPos.position, transform.rotation) as GameObject;
        b.GetComponent<Rigidbody>().AddForce(shootPos.forward * bulletSpeed);
        StartCoroutine(DestroyBullet(b,10f));
        yield return new WaitForSeconds(nextShot);
    }

    private IEnumerator DestroyBullet(GameObject bullet , float bulletDuration)
    {
        Destroy(bullet, bulletDuration);
        yield return null;
    }

}
