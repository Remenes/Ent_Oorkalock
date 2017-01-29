using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth;
    protected int currHealth;
    
	// Use this for initialization
	protected void Start () {
        resetHealth();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    protected void checkHealth() {
        if (currHealth <= 0) {
            onDeath();

            if (checkPlayerWinState())
                enableWinState();
            Destroy(gameObject);
            
        }
    }

    protected virtual void onDeath() {

    }

    public void takeDamage(int damageAmount) {
        currHealth -= damageAmount;
        checkHealth();

    }

    public void resetHealth() {
        currHealth = maxHealth;
    }

    protected bool checkPlayerWinState() {
        return transform.parent.childCount == 1;
    }

    protected void enableWinState() {
        GameManager.won = true;
    }
}
