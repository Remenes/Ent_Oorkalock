using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterShot : _ShotBase {
    
    public int scatterAmount;
    public int scatterWaves;

	// Use this for initialization
	void Start () {
        base.Start();

	}
	
	// Update is called once per frame
	void Update () {
        adjustShotTimer();
	}

    public override void Shoot() {
        if (canShoot()) {
            StartCoroutine(chainScatterShot());
            increaseShotTimer();
        }
    }
    protected override void shootAtTarget() {
        if (!target)
            return;
        bullet.SetActive(true);

        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - transform.position);

        for (int shotCount = 0; shotCount < scatterAmount; ++shotCount) {
            float maxAngle = Mathf.Asin(10 / (bulletScript.bulletSpeed)) * (180/Mathf.PI);
            Vector3 randomAngleOffset = new Vector3(UnityEngine.Random.Range(-maxAngle, maxAngle), 0, 0);
            Quaternion newRotation = Quaternion.Euler(lookRotation.eulerAngles + new Vector3(90, 0, 0) + randomAngleOffset);

            Instantiate(bullet, bullet.transform.position, newRotation);
        }
        bullet.SetActive(false);
    }

    private IEnumerator chainScatterShot() {
        isShooting = true;

        shootAtTarget();
        for (int waves = 1; waves < scatterWaves; ++waves) {
            if (!target)
                break;
            yield return new WaitForSeconds(.45f);
            shootAtTarget();
        }

        isShooting = false;
    }
}
