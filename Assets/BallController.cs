using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour {

	public Vector3 _velocity;

	private int BRICK_NUM_COLS = 12;
	private int BRICK_NUM_ROWS = 6;
	private float BRICK_WIDTH_BUFFER_SPACE = 0.05f;
	private float BRICK_HEIGHT_BUFFER_SPACE = 0.05f;

	void Start () {
		_velocity = new Vector3(3.0f, 3.0f, 0.0f);

		for (int i = 0; i < BRICK_NUM_COLS; i++) {
			for (int j = 0; j < BRICK_NUM_ROWS; j++) {
				GameObject brick = Instantiate(Resources.Load("BrickPrefab")) as GameObject;
				BoxCollider mainCollider = brick.GetComponent<BoxCollider>();
				float brickWidth = brick.transform.localScale.x * mainCollider.size.x;
				float brickHeight = brick.transform.localScale.y * mainCollider.size.y;
				Vector3 pos = new Vector3(5.4f, 8.7f, 0.0f);
				pos.x -= (brickWidth + BRICK_WIDTH_BUFFER_SPACE) * i;
				pos.y -= (brickHeight + BRICK_HEIGHT_BUFFER_SPACE) * j;
				brick.transform.position = pos;
			}
		}
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

	void FixedUpdate() {
		rigidbody.MovePosition(rigidbody.position + _velocity * Time.deltaTime);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
