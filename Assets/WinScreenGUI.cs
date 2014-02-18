using UnityEngine;
using System.Collections;

public class WinScreenGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	void OnGUI() {
		GUILayout.BeginArea (new Rect (10, Screen.height / 2 + 100, Screen.width - 10, 200));

		if (Application.loadedLevelName == "win-screen") {
			GameObject textObj = GameObject.Find("Message Text");
			textObj.GetComponent<GUIText>().text = "You Win!";
		}

		if (Application.loadedLevelName == "loss-screen") {
			GameObject textObj = GameObject.Find("Message Text");
			textObj.GetComponent<GUIText>().text = "You Lose! Try Again?";
		}

		if ((GUILayout.Button("Play"))) {
			Application.LoadLevel("scene01-level");
		}

		if (GUILayout.Button("Exit")) {
			Application.Quit();
			Debug.Log ("Application.Quit() only works in build, not in editor.");
		}
		
		GUILayout.EndArea();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
