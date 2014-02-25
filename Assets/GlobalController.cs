using UnityEngine;
using System.Collections;

public class GlobalController : MonoBehaviour {

	private int _score;
	private int _pointsPerSec;
	private int _multiplier;
	private int _livesLeft;
	private int _bricksRemaining;
	public bool _paused;
	public bool _win;
	public bool _lose;
	public bool _mode3D;

	private int BRICK_NUM_COLS = 12;
	private int BRICK_NUM_ROWS = 7;
	private float BRICK_WIDTH_BUFFER_SPACE = 0.05f;
	private float BRICK_HEIGHT_BUFFER_SPACE = 0.05f;
	private float MIDDLE_STRIP_TOP_Y = 5.57f;
	private float MIDDLE_STRIP_BOTTOM_Y = 5.51f;
	private int NUM_PLAYER_LIVES = 5;

	// frequently referenced objects
	private GUIText _scoreGUIText;
	private GUIText _livesGUIText;
	private GUIText _multiplierGUIText;
	private GUIText _winLossGUIText;
	private	GameObject _ballObject;
	private BallController _ballController;
	private GameObject _paddleObject;
	private PaddleController _paddleController;
	private Camera _mainCamera;
	private Camera _perspCamera;

	void Start () {
		_paused = false;
		_win = false;
		_lose = false;
		_mode3D = false;

		_score = 0;
		_pointsPerSec = 1;
		_multiplier = 1;
		_livesLeft = NUM_PLAYER_LIVES;
		_bricksRemaining = BRICK_NUM_COLS * BRICK_NUM_ROWS * 2;

		// init score text
		GameObject scoreText = GameObject.Find("Score Text");
		_scoreGUIText = scoreText.GetComponent<GUIText>();
		updateScoreText();

		// init lives text
		GameObject livesText = GameObject.Find("Lives Text");
		_livesGUIText = livesText.GetComponent<GUIText>();
		updateLivesText();

		// init multiplier text
		GameObject multiplierText = GameObject.Find("Multiplier Text");
		_multiplierGUIText = multiplierText.GetComponent<GUIText>();
		updateMultiplierText();

		// init ball reference
		_ballObject = GameObject.Find("Ball");
		_ballController = _ballObject.GetComponent<BallController>();

		// init paddle reference
		_paddleObject = GameObject.Find("Paddle");
		_paddleController = _paddleObject.GetComponent<PaddleController>();

		// init Win Loss message reference
		GameObject winLossText = GameObject.Find("Win Loss Text");
		_winLossGUIText = winLossText.GetComponent<GUIText>();
		_winLossGUIText.text = "";

		// init main and persp cameras
		Camera[] cams = Camera.allCameras;

		for (int i = 0; i < cams.Length; i++) {
			string camName = cams[i].gameObject.name;
			if (camName == "Main Camera") _mainCamera = cams[i];
			else if (camName == "Persp Camera") _perspCamera = cams[i];
		}

		// generate BrickPrefabs
		for (int i = 0; i < BRICK_NUM_COLS; i++) {
			for (int j = 0; j < BRICK_NUM_ROWS; j++) {

				GameObject brick = Instantiate(Resources.Load("BrickPrefab")) as GameObject;
				BoxCollider mainCollider = brick.GetComponent<BoxCollider>();

				float brickWidth = brick.transform.localScale.x * mainCollider.size.x;
				float brickHeight = brick.transform.localScale.y * mainCollider.size.y;

				Vector3 pos = new Vector3(5.4f, 9.4f, 0.0f);
				pos.x -= (brickWidth + BRICK_WIDTH_BUFFER_SPACE) * i;
				pos.y -= (brickHeight + BRICK_HEIGHT_BUFFER_SPACE) * j;

				brick.transform.position = pos;
			}
		}

		for (int i = 0; i < BRICK_NUM_COLS; i++) {
			for (int j = 0; j < BRICK_NUM_ROWS; j++) {
				
				GameObject brick = Instantiate(Resources.Load("BrickPrefab")) as GameObject;
				BoxCollider mainCollider = brick.GetComponent<BoxCollider>();
				
				float brickWidth = brick.transform.localScale.x * mainCollider.size.x;
				float brickHeight = brick.transform.localScale.y * mainCollider.size.y;
				
				Vector3 pos = new Vector3(5.4f, 4.0f, 0.0f);
				pos.x -= (brickWidth + BRICK_WIDTH_BUFFER_SPACE) * i;
				pos.y -= (brickHeight + BRICK_HEIGHT_BUFFER_SPACE) * j;
				
				brick.transform.position = pos;
			}
		}

		InvokeRepeating("multiplyMultiplier", 5, 5);
	}

	private void createWinScreen() {
		_winLossGUIText.text = "You Win!";
	}

	private void createLossScreen() {
		_winLossGUIText.text = "You Lose!";
	}

	public void KillBrick(int pointValue) {
		_bricksRemaining--;
		_score += pointValue;
		updateScoreText();

		if (_bricksRemaining == 0) _win = true;
	}

	public void DockLife() {
		if (_livesLeft > 1) {
			_livesLeft--;
			updateLivesText();
		} else {
			_lose = true;
		}
	}

	public void ResetMultiplier() {
		_multiplier = 1;
	}
	
	private void updateScoreText() {
		_scoreGUIText.text = "Score: " + _score;
	}

	private void updateLivesText() {
		_livesGUIText.text = "Lives: " + _livesLeft;
	}

	private void updateMultiplierText() {
		_multiplierGUIText.text = "Multiplier: x" + _multiplier;
	}

	private void toggle3DMode() {
		_mode3D = !_mode3D;
		if (_mode3D) {
			_mainCamera.tag = "3DCamera";
			_perspCamera.tag = "MainCamera";
			_perspCamera.depth = 1;
			_mainCamera.depth = -1;
		} else {
			_mainCamera.tag = "MainCamera";
			_perspCamera.tag = "3DCamera";
			_perspCamera.depth = -1;
			_mainCamera.depth = 1;
		}
	}

	private void multiplyMultiplier() {
		_multiplier *= 2;
		updateMultiplierText();
	}

	// Update is called once per frame
	void Update () {
	
		_score += _pointsPerSec * _multiplier;
		updateScoreText();

		// Win Condition
		if (_win) Application.LoadLevel("win-screen");

		// Lose Condition
		if (_lose) Application.LoadLevel("loss-screen");	

		// Reset the ball if it hits the middle electric strip, or let it pass through the paddle
		// if the paddle allows it to
		Vector3 ballPos = _ballObject.transform.position;
		if (ballPos.y < MIDDLE_STRIP_TOP_Y && ballPos.y > MIDDLE_STRIP_BOTTOM_Y) {

			// Here, we just need to calculate the paddle's bounds and compare it to the ball's position if the 
			// paddle allows it to. It's assumed the ball's _invisible == true because otherwise it wouldn't be able
			// to reach the middle electric strip's y coordinate range.
			Vector3 pos = _paddleObject.transform.position;
			Vector3 size = _paddleObject.transform.localScale;
			float right = pos.x - size.x/2;
			float left = pos.x + size.x/2;
			if (!(_ballObject.transform.position.x <= left && _ballObject.transform.position.x >= right)) {
				DockLife();
				ResetMultiplier();
				updateMultiplierText();
				_ballController.Reset();
			}
		}

		// Toggle mode
		if (Input.GetKey(KeyCode.Q)) {
			toggle3DMode();
		}

	}
}
