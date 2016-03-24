using UnityEngine;
using System.Collections;

public class playerNear_effects : MonoBehaviour {

	//NoiseAndScratches noise;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onTriggerEnter(Collider col)
	{
		if(col.tag == "BadBuilding")
		{
			col.GetComponent<Renderer>().material.color = Color.red;
		}
	}

	void onTriggerLeave(Collider col)
	{
		if(col.tag == "BadBuilding")
		{
			col.GetComponent<Renderer>().material.color = Color.blue;
		}
	}
}
