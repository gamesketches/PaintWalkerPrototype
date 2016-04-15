using UnityEngine;
using System.Collections;

public class OnBoardingScript : MonoBehaviour {

	Vector3 targetPosition;
	Vector3 targetRotation;
	GameObject playerCamera;
	bool started;
	// Use this for initialization
	void Start () {
		started = false;
		GameObject player = GameObject.FindWithTag("Player");
		playerCamera = player.transform.FindChild("Main Camera").gameObject;
		targetPosition = gameObject.transform.parent.transform.position;
		targetRotation = gameObject.transform.parent.transform.rotation.eulerAngles;
		Debug.Log(targetPosition);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && !started) {
			playerCamera.MoveTo(targetPosition, 8f, 0, EaseType.easeInOutQuad);
			playerCamera.RotateTo(targetRotation, 3f, 6f, EaseType.easeInOutQuad);
		}
	}

	void rotation() {
	}
}
