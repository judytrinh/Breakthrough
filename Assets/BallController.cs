using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	
	public Vector3 VELOCITY;
	
	void Start () {
		VELOCITY = new Vector3 (3.0f, 3.0f, 0.0f);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == "Wall")
			VELOCITY.x *= -1;
		else if (col.gameObject.name == "Ceiling")
			VELOCITY.y *= -1;
		else if (col.gameObject.name == "Paddle")
			VELOCITY.y *=-1;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(VELOCITY * Time.deltaTime);
	}
}
