using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour {

	private int POINT_VALUE = 20;
	public AudioClip deathSound;

	// Use this for initialization
	void Start () {
	}

	void OnCollisionEnter(Collision col) {
		AudioSource.PlayClipAtPoint(deathSound, gameObject.transform.position);
		GameObject globalController = GameObject.Find("Global Controller");
		GlobalController gScript = globalController.GetComponent<GlobalController>();
		gScript.KillBrick(POINT_VALUE);
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
