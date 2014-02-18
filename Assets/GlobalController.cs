using UnityEngine;
using System.Collections;

public class GlobalController : MonoBehaviour {

	private int _score;
	private int _livesLeft;
	private int _bricksRemaining;
	public bool _paused;
	public bool _win;
	public bool _lose;

	private int BRICK_NUM_COLS = 12;
	private int BRICK_NUM_ROWS = 6;
	private float BRICK_WIDTH_BUFFER_SPACE = 0.05f;
	private float BRICK_HEIGHT_BUFFER_SPACE = 0.05f;

	// frequently referenced objects
	private GUIText _scoreGUIText;
	private GUIText _livesGUIText;
	private GUIText _winLossGUIText;
	private	GameObject _ballObject;
	private BallController _ballController;

	void Start () {
		_paused = false;
		_win = false;
		_lose = false;

		_score = 0;
		_livesLeft = 3;
		_bricksRemaining = BRICK_NUM_COLS * BRICK_NUM_ROWS;

		// init score text
		GameObject scoreText = GameObject.Find("Score Text");
		_scoreGUIText = scoreText.GetComponent<GUIText>();
		updateScoreText();

		// init lives text
		GameObject livesText = GameObject.Find("Lives Text");
		_livesGUIText = livesText.GetComponent<GUIText>();
		updateLivesText();

		// init ball reference
		_ballObject = GameObject.Find("Ball");
		_ballController = _ballObject.GetComponent<BallController>();

		// init Win Loss message reference
		GameObject winLossText = GameObject.Find("Win Loss Text");
		_winLossGUIText = winLossText.GetComponent<GUIText>();
		_winLossGUIText.text = "";

		// generate BrickPrefabs
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
		//createReplayButton();
	}

	private void createWinScreen() {
		_winLossGUIText.text = "You Win!";
		//createReplayButton();
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
		if (_livesLeft > 0) {
			_livesLeft--;
			updateLivesText();
		} else {
			_lose = true;
		}
	}
	
	private void updateScoreText() {
		_scoreGUIText.text = "Score: " + _score;
	}

	private void updateLivesText() {
		_livesGUIText.text = "Lives: " + _livesLeft;
	}

	// Update is called once per frame
	void Update () {

		// Win Condition
		if (_win) Application.LoadLevel("win-screen");

		// Lose Condition
		if (_lose) Application.LoadLevel("loss-screen");	

		// reset the ball if it falls below the paddle 
		if (_ballObject.transform.position.y < 0.8f) {
			DockLife();
			_ballController.Reset();
		}
	}
}
