using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour {

	public Vector3 _velocity;
	private GameObject _paddle;
	private PaddleController _paddleController;
	private Vector3 RESET_POSITION = new Vector3(0, 5.9f, 0);
	private Vector3 RESET_VELOCITY = new Vector3(3.0f, 3.0f, 0.0f);

	void Start () {
		_paddle = GameObject.Find("Paddle");
		_paddleController = _paddle.GetComponent<PaddleController>();
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
				if (!_paddleController._invisible) _velocity.y *= -1;
				break;
			case "BrickPrefab(Clone)":
				_velocity.y *= -1;
				break;
			default:
				Debug.Log("UNHANDLED GAME OBJECT IN BallController.cs: OnCollisionEnter()");
				break;
		}
//		if (col.gameObject.name == "Wall")
//			_velocity.x *= -1;
//		else if (col.gameObject.name == "Ceiling")
//			_velocity.y *= -1;
//		else if (col.gameObject.name == "Paddle") {
//			if (!_paddleController._invisible) {
//				_velocity.y *= -1;
//			}
//		} else if (col.gameObject.name == "BrickPrefab(Clone)")
//			_velocity.y *= -1;
	}

	public void Reset() {
		_velocity = RESET_VELOCITY;
		rigidbody.position = RESET_POSITION;
	}

	public void FixedUpdate() {
		rigidbody.MovePosition(rigidbody.position + _velocity * Time.deltaTime);
	}


	// Update is called once per frame
	void Update () {

	}
}
