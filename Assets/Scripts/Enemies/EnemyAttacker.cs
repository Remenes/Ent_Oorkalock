using UnityEngine;
using System.Collections;

public class EnemyAttacker : EnemyHealth {

    protected GameObject target;
    public GameObject bullet;
    public float shotReloadTime = 1f;
    private float shotTimer;

	// Use this for initialization
	protected void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        base.Start();
        shotTimer = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
        //lookAtTarget();
        adjustShotTimer();
        Shoot();
	}


    protected void lookAtTarget() {
        Vector3 targetTrans = target.transform.position;
        if (target)
            transform.LookAt(targetTrans);
    }

    protected void adjustShotTimer() {
        if (canShoot())
            return;
        shotTimer += Time.deltaTime;
        if (shotTimer >= shotReloadTime) {
            shotTimer = 0;
        }
    }

    protected void Shoot() {
        if (canShoot()) {
            shootAtTarget();
            shotTimer += Time.deltaTime;
        }
    }

    protected bool canShoot() {
        return shotTimer == 0;
    }

    protected void shootAtTarget() {
        if (!target)
            return;
        bullet.SetActive(true);
        //print(Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(90, 0, 0)).eulerAngles);
        //print("1" + Quaternion.Euler(Quaternion.LookRotation(transform.position - target.transform.position).eulerAngles + new Vector3(90, 0, 0)).eulerAngles);
        Quaternion lookDirection = Quaternion.LookRotation(target.transform.position - transform.position);
        Vector3 newLookDirection = Quaternion.Euler(lookDirection.eulerAngles + new Vector3(90, 0, 0)).eulerAngles;
        Instantiate(bullet, bullet.transform.position, Quaternion.Euler(newLookDirection));
        bullet.SetActive(false);
    }
    
}
