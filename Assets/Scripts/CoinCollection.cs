using UnityEngine;
using UnityEngine.UI; // use UI namespace
using System.Collections;

public class CoinCollection : MonoBehaviour {

	// create variable to increment score
	private int score;

	// variable to store the text for score
	public Text scoreText;

	// Use this for initialization
	void Start () {
		score = 0;
		setScoreText ();
	}

	void OnTriggerEnter( Collider other) {
		if (other.tag == "Coin") {
			score += 5;
			Destroy(other.gameObject);
		}
		if (other.tag == "building") {
			score -= 1;
		}
		setScoreText ();
	}

	void setScoreText(){
		scoreText.text = "Score: " + score.ToString();
	}

}