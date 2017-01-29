using UnityEngine;
using System.Collections;

public class BasicBullet : MonoBehaviour {

    public float bulletSpeed = 2f;
    public int bulletDamage = 25;
    Quaternion lookDirection;
    Vector3 moveDirection;
    Rigidbody2D bulletRB;
    ParticleSystem effects;

    public float lifeTime = 5f;
    private float currentLifeTimer = 0;

    float effectsLife;
    private bool dying = false;
    private float dyingTimer = 0;

    // Use this for initialization
    void Start() {
        lookDirection = transform.localRotation;
        bulletRB = GetComponent<Rigidbody2D>();
        effects = GetComponentInChildren<ParticleSystem>();
        effectsLife = effects.startLifetime;
    }

    // Update is called once per frame
    void Update() {
        if (dying) {
            dieOut();
            return;
        }
        movebullet();
        increaseLifeTime();
    }

    private void increaseLifeTime() {
        currentLifeTimer += Time.deltaTime;
        if (currentLifeTimer > lifeTime)
            startDeath();
    }

    private void movebullet() {
        moveDirection = transform.up.normalized * bulletSpeed * Time.deltaTime;
        Vector3 offset = (transform.position + moveDirection);
        bulletRB.MovePosition(offset);
    }
    
    public void startDeath() {
        dying = true;
        effects.Stop();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void dieOut() {
        //dyingTimer += Time.deltaTime;
        //if (dyingTimer >= effectsLife)
        //    Destroy(gameObject);
        Destroy(gameObject);
    }

    private void doDamage(PlayerHealth enemy) {
        enemy.takeDamage(bulletDamage);
    }

    void OnTriggerEnter2D(Collider2D collided) {
        if (dying)
            return;
        if (collided.tag.Equals("Player")) {
            startDeath();
            PlayerHealth player = collided.GetComponent<PlayerHealth>();
            if (player) {
                doDamage(player);
            }
        }
    }
}
