using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Input.mousePosition;
		Vector3 wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.z, mousePos.z));
		transform.position = new Vector3(wantedPos.x, transform.position.y, transform.position.z);
	}
}
