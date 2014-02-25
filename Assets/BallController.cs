using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour {

	public Vector3 _velocity;
	private Vector3 RESET_POSITION = new Vector3(0, 6.0f, 0);
	private Vector3 RESET_VELOCITY = new Vector3(3.0f, 3.0f, 0.0f);

	void Start () {
		Reset();
	}

	void OnCollisionEnter(Collision col) {
		string gameObjName = col.gameObject.name;
		switch(gameObjName) {
			case "Wall":
				_velocity.x *= -1;
				break;
			case "Ceiling":
				_velocity.y *= -1;
				break;
			case "Floor":
				_velocity.y *= -1;
				break;
			case "Paddle":
					_velocity.y *= -1;
				break;
			case "BrickPrefab(Clone)":
				_velocity.y *= -1;
				break;
			default:
				Debug.Log("UNHANDLED GAME OBJECT IN BallController.cs: OnCollisionEnter()");
				Debug.Log (col.gameObject.name);
				break;
		}
	}

	public void Reset() {
		_velocity = RESET_VELOCITY;
		rigidbody.position = RESET_POSITION;
	}

	// Wrapped function allowing Ball-object collisions to be ignored
	public void ToggleCollisionWith(GameObject collidingObj, bool ignore) {
		Physics.IgnoreCollision(gameObject.collider, collidingObj.collider, ignore);
	}

	public void FixedUpdate() {
		rigidbody.MovePosition(rigidbody.position + _velocity * Time.deltaTime);
	}


	// Update is called once per frame
	void Update () {
	}
}
