using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour {

	public Vector3 _velocity;

	void Start () {
		_velocity = new Vector3(3.0f, 3.0f, 0.0f);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == "Wall")
			_velocity.x *= -1;
		else if (col.gameObject.name == "Ceiling")
			_velocity.y *= -1;
		else if (col.gameObject.name == "Paddle")
			_velocity.y *= -1;
		else if (col.gameObject.name == "BrickPrefab(Clone)")
			_velocity.y *= -1;
	}

	public void FixedUpdate() {
		rigidbody.MovePosition(rigidbody.position + _velocity * Time.deltaTime);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
