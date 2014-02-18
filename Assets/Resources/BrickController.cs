using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	void OnCollisionEnter(Collision col) {
		GameObject globalController = GameObject.Find("Global Controller");
		GlobalController gScript = globalController.GetComponent<GlobalController>();
		gScript.KillBrick();
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
