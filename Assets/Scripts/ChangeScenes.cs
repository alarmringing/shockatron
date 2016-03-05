using UnityEngine;
using System.Collections;

public class ChangeScenes : MonoBehaviour {

	// loads demo 1
	public void play_game() {
		Application.LoadLevel ("Tron Planet");
	}
	// loads demo 2
	public void show_scores() {
		Application.LoadLevel ("HighScores");
	}
	// loads demo 2
	public void main_menu() {
		Application.LoadLevel ("MainScreen");
	}

	// exits the game
	public void exit_game() {
		Application.Quit ();
	}
		
}
