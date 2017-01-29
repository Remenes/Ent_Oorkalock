using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _ShotBase : MonoBehaviour {

    public float shotReloadTime = 1f;
    protected float shotTimer;
    public GameObject bullet;
    public GameObject target;
    protected BasicBullet bulletScript;

    public bool isShooting = false;

    protected float defaultBulletSpeed;
    protected int defaultBulletDamage;
    protected Vector3 defaultbulletSize;

    protected void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
        bulletScript = bullet.GetComponent<BasicBullet>();
        defaultBulletSpeed = bulletScript.bulletSpeed;
        defaultBulletDamage = bulletScript.bulletDamage;
        defaultbulletSize = bullet.transform.localScale;
    }

    public bool canShoot() {
        return shotTimer == 0;
    }
    protected void adjustShotTimer() {
        if (!canShoot()) {
            shotTimer += Time.deltaTime;
            if (shotTimer >= shotReloadTime)
                shotTimer = 0;
        }
    }
    protected void increaseShotTimer() {
        shotTimer += Time.deltaTime;
    }
    protected void changeBulletAttributes(float newSpeed, int newDamage, Vector3 newSize) {
        bulletScript.bulletDamage = newDamage;
        bulletScript.bulletSpeed = newSpeed;
        bullet.transform.localScale = newSize;
    }

    protected void resetBulletAttributes() {
        bulletScript.bulletDamage = defaultBulletDamage;
        bulletScript.bulletSpeed = defaultBulletSpeed;
        bullet.transform.localScale = defaultbulletSize;
    }

    public abstract void Shoot();
    protected abstract void shootAtTarget();

}
