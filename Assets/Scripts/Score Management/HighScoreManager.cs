using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreManager : MonoBehaviour {

	// variable to store the text for score
	public Text score1;
	public Text score2;
	public Text score3;
	public Text score4;
	public Text score5;


	// Use this for initialization
	void Start () {
		setScore (score1, 1, PlayerPrefs.GetInt ("HighScore1"));
		setScore (score2, 2, PlayerPrefs.GetInt ("HighScore2"));
		setScore (score3, 3, PlayerPrefs.GetInt ("HighScore3"));
		setScore (score4, 4, PlayerPrefs.GetInt ("HighScore4"));
		setScore (score5, 5, PlayerPrefs.GetInt ("HighScore5"));

	}

	void setScore(Text scoreText, int number, int score){
		scoreText.text = number.ToString() + ".) " + score.ToString();
	}


}
