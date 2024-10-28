using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public MissileProjectile projectilePrefab;

    public float damage;
    public float projectileSpeed;
    public float projectileScale;
    public float shotInterval;

    public float MaxDist;

    private void Start()
    {
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            Fire();
        }
    }

    private void Fire()
    {
        Vector2 randomPos = Random.insideUnitCircle * MaxDist;

        MissileProjectile proj = Instantiate(projectilePrefab);

        proj.damage = this.damage;
        proj.duration = 1 / projectileSpeed;
        proj.transform.localScale = proj.transform.localScale * projectileScale;
        proj.transform.localPosition = randomPos;
        proj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-20f, 20));
    }
}

