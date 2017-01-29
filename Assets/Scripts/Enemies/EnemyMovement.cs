using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public GameObject background;
    private Collider2D backgroundCollider;
    private GameObject player;
    private Collider2D playerCollider;
    public float speed;
    public float avgWaitTilMove;
    public float waitOffset;

    private Rigidbody2D enemyRB;

	// Use this for initialization
	void Start () {
        background = GameObject.FindGameObjectWithTag("Background");
        backgroundCollider = background.GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider2D>();
        enemyRB = GetComponent<Rigidbody2D>();

        StartCoroutine(moveEnemy());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private Vector3 getRandomLocation() {
        Vector3 newLocation = backgroundCollider.bounds.extents;
        int randNum = Random.Range(0, 2);
        if (randNum == 1 && player)
            newLocation = playerCollider.bounds.extents * 2;
        Vector3 offset = new Vector3(randFullValue(newLocation.x), randFullValue(newLocation.y), 0);
        return offset;
    }
    
    private float randFullValue(float half) {
        return Random.Range(-half, half);
    }

    private IEnumerator moveEnemy() {
        while (true) {
            StartCoroutine(lerpToPosition(getRandomLocation()));
            yield return new WaitForSeconds(avgWaitTilMove + randFullValue(waitOffset));
        }
    }

    //private IEnumerator lerpToPosition(Vector3 newLocation) {
    //    Vector3 startPosition = transform.position;
    //    float distance = Vector3.Distance(transform.position, newLocation);
    //    float maxMoveTimer = distance / speed;
    //    float timeMoved = 0;
    //    Vector3 direction = (newLocation - transform.position).normalized;
    //    Vector3 offset = direction * speed * Time.deltaTime;
    //    while (timeMoved < maxMoveTimer + 3) { // 3 seconds as a just-in-case thing, and to keep them there for 3 extra seconds
    //        timeMoved += Time.deltaTime;
    //        //transform.position = Vector3.Lerp(startPosition, newLocation, timeMoved/maxMoveTimer);
    //        enemyRB.MovePosition();
    //        yield return 0;
    //    }
    //}
    private IEnumerator lerpToPosition(Vector3 newLocation) {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(transform.position, newLocation);
        float maxMoveTimer = distance / speed;
        float timeMoved = 0;

        while (timeMoved < maxMoveTimer + 3) { // 3 seconds as a just-in-case thing, and to keep them there for 3 extra seconds
            distance = Vector3.Distance(transform.position, newLocation);

            if (distance > 2f) {
                Vector3 direction = (newLocation - transform.position).normalized;
                Vector3 offset = direction * speed * Time.deltaTime;
                //transform.position = Vector3.Lerp(startPosition, newLocation, timeMoved/maxMoveTimer);
                enemyRB.MovePosition(transform.position + offset);
            }
            timeMoved += Time.deltaTime;
            yield return 0;
        }
    }
}
