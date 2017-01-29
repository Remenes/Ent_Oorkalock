using UnityEngine;
using System.Collections;

public class BasicArrow : MonoBehaviour {

    public float arrowSpeed = 2f;
    public int arrowDamage = 25;
    Quaternion lookDirection;
    Vector3 moveDirection;
    Rigidbody2D arrowRB;
    ParticleSystem effects;

    public float lifeTime = 5f;
    private float currentLifeTimer = 0;

    float effectsLife;
    private bool dying = false;
    private float dyingTimer = 0;

    // Use this for initialization
    void Start() {
        lookDirection = transform.localRotation;
        arrowRB = GetComponent<Rigidbody2D>();
        effects = GetComponentInChildren<ParticleSystem>();
        effectsLife = effects.startLifetime;
    }

    // Update is called once per frame
    void Update() {
        if (dying) {
            dieOut();
            return;
        }
        moveArrow();
        increaseLifeTime();
    }

    private void increaseLifeTime() {
        currentLifeTimer += Time.deltaTime;
        if (currentLifeTimer > lifeTime)
            startDeath();
    }

    private void moveArrow() {
        moveDirection = transform.up * arrowSpeed * Time.deltaTime;
        Vector3 offset = (transform.position + moveDirection);
        arrowRB.MovePosition(offset);
    }

    public void startDeath() {
        dying = true;
        effects.Stop();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void dieOut() {
        dyingTimer += Time.deltaTime;
        if (dyingTimer >= effectsLife)
            Destroy(gameObject);
    }

    private void doDamage(EnemyHealth enemy) {
        enemy.takeDamage(arrowDamage);
    }

    void OnTriggerEnter2D(Collider2D collided) {
        if (dying)
            return;
        if (collided.tag.Equals("Shootable")) {
            startDeath();
            EnemyHealth enemy = collided.GetComponent<EnemyHealth>();
            if (enemy) {
                doDamage(enemy);
            }
        }
    }

}

