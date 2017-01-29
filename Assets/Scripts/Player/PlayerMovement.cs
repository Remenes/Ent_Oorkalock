using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private float speed;
    public float normalSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashTime = 2f;
    public float dashReload = 3f;
    float dashedReloadTime = 0;

    float horSpeed = 0;
    float verSpeed = 0;
    Rigidbody2D playerRB;

    bool canMove = true;
    bool dashing = false;
    bool canDash = true;

	// Use this for initialization
	void Start () {
        playerRB = GetComponent<Rigidbody2D>();
        resetSpeed();
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove) {
            checkMovements();
        }
        checkActivateDash();
        if (!dashing) {
            movePlayer();
            adjustDashTime();
        }
        else {
            Dash();
        }

        turnPlayer();
        
	}

    void checkMovements() {
        horSpeed = Input.GetAxisRaw("Horizontal");
        verSpeed = Input.GetAxisRaw("Vertical");
    }

    void movePlayer() {
        Vector3 offset = new Vector3(horSpeed, verSpeed, 0).normalized * speed * Time.deltaTime;
        Vector3 newPosition = (transform.position + offset);

        playerRB.MovePosition(newPosition);
    }
    
    void turnPlayer() {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.LookAt(point);
    }

    void stopMovement() {
        horSpeed = 0;
        verSpeed = 0;
    }

    void checkActivateDash() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (canDash && (horSpeed != 0 || verSpeed != 0)) {
                dashing = true;
                canDash = false;
                canMove = false;
                speed = dashSpeed;
            }
        }
        adjustDashTime();
    }
    void adjustDashTime() {
        if (dashedReloadTime != 0) {
            dashedReloadTime += Time.deltaTime;
            if (dashedReloadTime >= dashReload) {
                dashedReloadTime = 0;
                canDash = true;     
            }
        }
    }
    void Dash() {
        dashedReloadTime += Time.deltaTime;
        movePlayer();
        decreaseSpeed(dashSpeed / dashTime);
        if (speed < 1) { 
            dashing = false;
            canMove = true;
            resetSpeed();
        }
    }
    private void decreaseSpeed(float changeSpeed) {
        speed -= changeSpeed * Time.deltaTime;
    }

    void resetSpeed() {
        speed = normalSpeed;
    }
}
