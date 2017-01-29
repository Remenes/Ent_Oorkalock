using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthUI : MonoBehaviour {

    public GameObject player;
    private PlayerHealth playerHealth;
    private Slider slider;
    private GameObject fillArea;
    public GameObject gameOverScreen;

	// Use this for initialization
	void Start () {
        
        playerHealth = player.GetComponent<PlayerHealth>();
        slider = GetComponent<Slider>();
        slider.maxValue = playerHealth.maxHealth;
        slider.value = slider.maxValue;

        fillArea = transform.GetChild(1).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (player) 
            moveUI();
        matchPlayerHealth();
	}

    private void moveUI() {
        Vector3 posBelowPlayer = player.transform.position + new Vector3(0, -player.GetComponent<CircleCollider2D>().radius, 0);
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(posBelowPlayer);
        transform.position = playerScreenPoint;
    }

    private void matchPlayerHealth() {
        slider.value = playerHealth.currHealth;
        fillArea.SetActive(slider.value > 0);
    }
}
