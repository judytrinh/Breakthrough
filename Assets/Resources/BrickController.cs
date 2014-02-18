using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	void OnCollisionEnter(Collision col) {
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
