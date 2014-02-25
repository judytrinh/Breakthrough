using UnityEngine;
using System.Collections;

public class ExtendPowerupController : MonoBehaviour {

	private Vector3 _velocity;
	private GameObject _paddle;
	private PaddleController _paddleController;

	// Use this for initialization
	void Start () {
		_paddle = GameObject.Find("Paddle");
		_paddleController = _paddle.GetComponent<PaddleController>();

		_velocity = new Vector3(0, -0.05f, 0);
		if (gameObject.transform.position.y < _paddle.transform.position.y) {
			_velocity *= -1;
		}
	}

	private void updatePosition() {
		Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + _velocity.y, 0);
		gameObject.transform.position = pos;
	}

	// Update is called once per frame
	void Update () {
		updatePosition();
	}
}
