using UnityEngine;
using System.Collections;

public class KillBoundaryController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject ball = GameObject.Find("Ball");
		BallController ballController = ball.GetComponent<BallController>();
		ballController.ToggleCollisionWith(gameObject, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
