using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour {

	// loads demo 1
	public void play_tron() {
		PlayerPrefs.SetInt ("currentScore", 0);
		SceneManager.LoadScene ("Tron Planet_jihee");
	}
	public void play_dontStop() {
		PlayerPrefs.SetInt ("currentScore", 0);
		SceneManager.LoadScene ("M_Jackson Planet");
	}
	// loads demo 2
	public void show_scores() {
		SceneManager.LoadScene ("Goals");
	}
	// loads demo 2
	public void main_menu() {
		SceneManager.LoadScene ("MainScreen");
	}
	// load any scene by string
	public void load_scene(string SceneName){
		SceneManager.LoadScene (SceneName);
	}

	// exits the game
	public void exit_game() {
		Application.Quit ();
	}
		
	// controls volume throughout the game
	public void volumne_control(float sliderValue){
		AudioListener.volume = sliderValue;
	}

	// controls the inversion of the y axis on loading the game
	public void toggle_inversion(bool toggleSelected){

		int invert = (toggleSelected) ? (-1) : (1);
		PlayerPrefs.SetInt ("inversion", invert);
	}
}
