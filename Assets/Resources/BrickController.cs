using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour {

	private int POINT_VALUE;

	// Use this for initialization
	void Start () {
		POINT_VALUE = 20;
	}

	void OnCollisionEnter(Collision col) {
		GameObject globalController = GameObject.Find("Global Controller");
		GlobalController gScript = globalController.GetComponent<GlobalController>();
		gScript.KillBrick(POINT_VALUE);
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
