using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public int maxHealth = 100;
    public int currHealth;

    // Use this for initialization
    void Start() {
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update() {

    }

    private void checkHealth() {
        if (currHealth <= 0) {
            Destroy(gameObject);
            GameManager.lost = true;
        }
    }

    public void takeDamage(int damageAmount) {
        currHealth -= damageAmount;
        checkHealth();

    }
}
