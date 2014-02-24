using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

	public bool _invisible;
	private GameObject _globalObj;
	private GlobalController _globalController; 
	private BoxCollider _mainCollider;

	// Use this for initialization
	void Start () {
		_invisible = false;
		_mainCollider = GetComponent<BoxCollider>();
		Screen.showCursor = false;
		_globalObj = GameObject.Find("Global Controller");
		_globalController = _globalObj.GetComponent<GlobalController>();
	}
	
	// Update is called once per frame
	void Update () {

		if (!_globalController._mode3D) {

			Vector3 mousePos = Input.mousePosition;
			Vector3 wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.z, mousePos.z));
			transform.position = new Vector3(wantedPos.x + _mainCollider.size.x/2, transform.position.y, transform.position.z);

		} else {

			if (Input.GetKey(KeyCode.LeftArrow))
				transform.Translate(new Vector3(0.1f, 0, 0));
			else if (Input.GetKey(KeyCode.RightArrow))
				transform.Translate(new Vector3(-0.1f, 0, 0));

			Vector3 mainCamPos = Camera.main.transform.position;
			Camera.main.transform.position = new Vector3(transform.position.x, mainCamPos.y, mainCamPos.z);

		}

		// Determining pass-through behavior
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			_invisible = true;
		} else {
			_invisible = false;
		}

	}
}
