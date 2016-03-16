using UnityEngine;
using System.Collections;

public class ChangeScenes : MonoBehaviour {

	// loads demo 1
	public void play_tron() {
		PlayerPrefs.SetInt ("currentScore", 0);
		Application.LoadLevel ("Tron Planet_jihee");
	}
	public void play_dontStop() {
		PlayerPrefs.SetInt ("currentScore", 0);
		Application.LoadLevel ("M_Jackson Planet");
	}
	// loads demo 2
	public void show_scores() {
		Application.LoadLevel ("HighScores");
	}
	// loads demo 2
	public void main_menu() {
		Application.LoadLevel ("MainScreen");
	}
	// load any scene by string
	public void load_scene(string SceneName){
		Application.LoadLevel (SceneName);
	}

	// exits the game
	public void exit_game() {
		Application.Quit ();
	}
		
}
