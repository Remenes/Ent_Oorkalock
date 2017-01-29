using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacker : EnemyAttacker {

    private ConsecutiveShot consecShot;
    private ShatterShot shatShot;
    private SpawnEnemies spawnEnemies;

    private List<_ShotBase> specialShots;
    public float specialAttackDefaultTime;
    public float specialAttackTimeRandomOffset;
    private float specialAttackReloadTime;
    private float specialAttackTimer;

    public float spawnEnemyReloadTime;
    
	// Use this for initialibzation
	void Start () {
        base.Start();
        consecShot = GetComponent<ConsecutiveShot>();
        shatShot = GetComponent<ShatterShot>();
        spawnEnemies = GetComponent<SpawnEnemies>();

        randomizeSpecialAttackTime();
        specialAttackTimer = Time.deltaTime;

        specialShots = new List<_ShotBase> { consecShot, shatShot };
        StartCoroutine(spawnEnemy());
    }
	
	// Update is called once per frame
	void Update () {
        //lookAtTarget();
        adjustShotTimer();
        adjustSpecialAttackTimer();
        if (target) {
            if (canUseSpecial()) { // Inc special attack timer after using any special ability
                useSpecialAbilities();
            } else if (!isUsingSpecial()) {
                Shoot();
            }

        }
        
	}

    private bool isUsingSpecial() {
        foreach (_ShotBase specialShot in specialShots) {
            if (specialShot.isShooting)
                return true;
        }
        return false;
    }

    protected override void onDeath() {
        GameManager.entKilled = true;
    }

    private void useSpecialAbilities() {
        List<int> usedShotsIndexes = new List<int> ();
        while (usedShotsIndexes.Count < specialShots.Count) {
            int randIndex = UnityEngine.Random.Range(0, specialShots.Count);
            if (usedShotsIndexes.Contains(randIndex))
                break;
            if (specialShots[randIndex].canShoot()) {
                specialShots[randIndex].Shoot();
                incSpecialAttTimer();
                break;
            } else {
                usedShotsIndexes.Add(randIndex);
            }
        }
    }

    private void randomizeSpecialAttackTime() {
        specialAttackReloadTime = Random.Range(specialAttackDefaultTime - specialAttackTimeRandomOffset, specialAttackDefaultTime + specialAttackTimeRandomOffset);
    }

    private bool canUseSpecial() {
        return specialAttackTimer == 0;
    }

    void adjustSpecialAttackTimer() {
        if (!canUseSpecial()) {
            incSpecialAttTimer();
            if (specialAttackTimer > specialAttackReloadTime) {
                specialAttackTimer = 0;
                randomizeSpecialAttackTime();
            }
        }
    }

    private void incSpecialAttTimer() {
        specialAttackTimer += Time.deltaTime;
    }


    private IEnumerator spawnEnemy() {
        float constDecrease = 3.3f;
        while (true) {
            float waitTime = (spawnEnemyReloadTime / constDecrease) * Mathf.Max(1, constDecrease * (transform.parent.childCount - 1) / 3);
            print(waitTime);
            yield return new WaitForSeconds(waitTime);
            if (!target)
                break;
            spawnEnemies.spawnEnemy();
        }
    }
}
