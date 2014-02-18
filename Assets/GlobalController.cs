﻿using UnityEngine;
using System.Collections;

public class GlobalController : MonoBehaviour {

	private int _score;
	private int _livesLeft;
	private int _bricksRemaining;
	public bool _paused;

	private int BRICK_NUM_COLS = 12;
	private int BRICK_NUM_ROWS = 6;
	private float BRICK_WIDTH_BUFFER_SPACE = 0.05f;
	private float BRICK_HEIGHT_BUFFER_SPACE = 0.05f;

	void Start () {
		_score = 0;
		_livesLeft = 3;
		_bricksRemaining = BRICK_NUM_COLS * BRICK_NUM_ROWS;

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
	}

	public void KillBrick() {
		_bricksRemaining--;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
