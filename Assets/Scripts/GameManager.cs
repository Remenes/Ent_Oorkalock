using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static bool lost;
    public static bool won;
    public static bool entKilled;
    public GameObject wonScreen;
    public GameObject lostScreen;
    public GameObject diedButSlew;

	// Use this for initialization
	void Start () {
        lost = false;
        won = false;
        entKilled = false;
	}
	
	// Update is called once per frame
	void Update () {
        checkLost();
        checkWon();
	}

    private void checkLost() {
        if (lostScreen.activeSelf != lost) {
            if (entKilled) {
                diedButSlew.SetActive(true);
            } else {
                lostScreen.SetActive(lost);
            }
        }
    }

    private void checkWon() {
        if (wonScreen.activeSelf != won) 
            wonScreen.SetActive(won);
        
    }
}
