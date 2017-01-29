using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    private delegate void EnemyType();

    public GameObject spawnColliderObject;
    private Collider2D spawnCollider;

    public GameObject enemy;
    private BasicBullet enemyBulletScript;
    private EnemyMovement enemyMovement;
    private EnemyAttacker enemyAttacker;
    private EnemyHealth enemyHealth;

    //Defaults
    private float defaultEnemySpeed;
    private float defaultWaitTilMove;
    private int defaultEnemyHealth;
    private Vector3 defaultEnemySize;

    // Use this for initialization
    void Start () {
        spawnCollider = spawnColliderObject.GetComponent<Collider2D>();
        enemyMovement = enemy.GetComponent<EnemyMovement>();
        enemyAttacker = enemy.GetComponent<EnemyAttacker>();
        enemyHealth = enemy.GetComponent<EnemyHealth>();

        enemyBulletScript = enemy.transform.FindChild("EnemyBasicBullet").GetComponent<BasicBullet>();
        defaultEnemySpeed = enemyMovement.speed;

        defaultEnemyHealth = enemyHealth.maxHealth;
        defaultEnemySize = enemy.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public Vector3 getRandomSpawnLocation() {
        Vector3 halfSizeBounds = spawnCollider.bounds.extents;
        Vector3 offset = new Vector3(randFullValue(halfSizeBounds.x), randFullValue(halfSizeBounds.y), 0);
        return transform.position + offset;
    }

    private float randFullValue(float half) {
        return Random.Range(-half, half);
    }

    public void spawnEnemy() {
        enemy.SetActive(true);
        List<EnemyType> list = new List<EnemyType> { setEnemyType1, setEnemyType2, setEnemyType3, setEnemyType4 };
        list[Random.Range(0, list.Count)]();
        enemyMovement.speed = defaultEnemySpeed + Random.Range(0, defaultEnemySpeed * (2/3));
        Instantiate(enemy, getRandomSpawnLocation(), enemy.transform.rotation, transform.parent);
        resetEnemyAttributes();
        enemy.SetActive(false);
    }

    private void setEnemyType1() { // BulletSprayer
        enemyBulletScript.bulletDamage = 1;
        enemyBulletScript.bulletSpeed = 20f;
        enemyAttacker.shotReloadTime = .14f;
        enemyHealth.maxHealth = 75;
    }

    private void setEnemyType2() { // Medium
        enemyBulletScript.bulletDamage = 2;
        enemyBulletScript.bulletSpeed = 22f;
        enemyAttacker.shotReloadTime = .28f;
    }

    private void setEnemyType3() { // Sniper
        enemyBulletScript.bulletDamage = 4;
        enemyBulletScript.bulletSpeed = 33f;
        enemyAttacker.shotReloadTime = .9f;
    }

    private void setEnemyType4() { // Heavy
        enemyBulletScript.bulletDamage = 2;
        enemyBulletScript.bulletSpeed = 28f;
        enemyAttacker.shotReloadTime = .4f;
        enemyHealth.maxHealth = 175;
        enemy.transform.localScale *= 1.4f;
    }

    private void resetEnemyAttributes() {
        enemyHealth.maxHealth = defaultEnemyHealth;
        enemy.transform.localScale = defaultEnemySize;
    }
}
