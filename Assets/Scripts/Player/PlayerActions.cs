using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

    public float basicShotReloadTime = 1f;
    private float basicShotTimer = 0f;

    private GameObject basicArrow;

	// Use this for initialization
	void Start () {
        basicArrow = transform.FindChild("BasicArrow").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        PlayerAction();
        reload();
	}

    void PlayerAction() {
        if (Input.GetMouseButton(0)) {
            if (basicShotTimer == 0) { 
                Shoot_BasicArrow();
                basicShotTimer += Time.deltaTime;
            }
        }
    }

    void reload() {
        if (basicShotTimer > 0) {
            basicShotTimer += Time.deltaTime;
            if (basicShotTimer > basicShotReloadTime)
                basicShotTimer = 0;
        }
    }

    void Shoot_BasicArrow() {
        basicArrow.SetActive(true);
        Vector3 shotRotation = transform.localRotation.eulerAngles + new Vector3(90, 0, 0);
        Instantiate(basicArrow, basicArrow.transform.position, Quaternion.Euler(shotRotation));
        basicArrow.SetActive(false);
    }
}
