using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour {

	private int LIVES = 5;
	private int POINT_VALUE = 5000;

	private int _livesLeft;
	private GameObject _ball;
	private BallController _ballController;
	private GameObject _globalObject;
	private GlobalController _globalController;
	public AudioClip deathSound;

	// Use this for initialization
	void Start () {
		_livesLeft = LIVES;
		_ball = GameObject.Find("Ball");
		_ballController = _ball.GetComponent<BallController>();
		
		_globalObject = GameObject.Find("Global Controller");
		_globalController = _globalObject.GetComponent<GlobalController>();
	}

	void OnCollisionEnter(Collision col) {
		AudioSource.PlayClipAtPoint(deathSound, gameObject.transform.position);
		//
		if (_livesLeft > 1) {
			_livesLeft--;
			FakeDie();
			int num = Random.Range(0, 25);
			if (num == 3) DropPowerup();
			Invoke("Respawn", 3);
		} else {
			_globalController.KillBrick(POINT_VALUE);
			Destroy(gameObject);
		}
	}

	public void DropPowerup() {
		GameObject powerup = Instantiate(Resources.Load("ExtendPowerupPrefab")) as GameObject;
		powerup.transform.position = gameObject.transform.position;
	}

	public void FakeDie() {
		renderer.enabled = false;
		_ballController.ToggleCollisionWith(gameObject, true);
	}

	public void Respawn() {
		renderer.enabled = true;
		_ballController.ToggleCollisionWith(gameObject, false);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
