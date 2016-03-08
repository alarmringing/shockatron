using UnityEngine;
using System.Collections;

public class PrefabSelfDestroyer : MonoBehaviour {


	public float countDown = 4f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		countDown -= Time.deltaTime;
	
		if(countDown < 0) Object.Destroy(this.gameObject); //after countdown below zero, destroy self
	}
}
