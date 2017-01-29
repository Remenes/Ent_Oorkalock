using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsecutiveShot : _ShotBase {

    public int shotAmount;

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
            StartCoroutine(consecutiveShot());
            increaseShotTimer();
        }
    }

    protected override void shootAtTarget() {
        if (!target)
            return;
        bullet.SetActive(true);
        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        Quaternion newRotation = Quaternion.Euler(lookRotation.eulerAngles + new Vector3(90, 0, 0));
        Instantiate(bullet, bullet.transform.position, newRotation);
        bullet.SetActive(false);
    }

    private IEnumerator consecutiveShot() {
        isShooting = true;

        shootAtTarget();
        int shotCounter = 0;
        int newShotAmount = shotAmount - 2; // -2 because consecShot shoots twice already, once above, and once below the loop
        float defaultWaitTime = .25f;
        float waitTime = defaultWaitTime;
        float divisionConstant = 3.5f;

        while (shotCounter <= newShotAmount-1) {
            yield return new WaitForSeconds(waitTime);
            waitTime = (defaultWaitTime / divisionConstant) * (divisionConstant * (newShotAmount - shotCounter)/newShotAmount);
            ++shotCounter;
            shootAtTarget();
        }
        changeBulletAttributes(defaultBulletSpeed * 1.5f, defaultBulletDamage * 2, defaultbulletSize * 7);
        shootAtTarget();
        resetBulletAttributes();

        isShooting = false;
    }
}
