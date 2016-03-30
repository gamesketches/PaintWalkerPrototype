using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour {

	public float cameraRaiseTime;
	public float cameraRotateTime;
	public float lastTime;

	GameObject player;
	GameObject playerCamera;

	void Start(){
		player = GameObject.FindWithTag("Player");
		playerCamera = player.transform.FindChild("Main Camera").gameObject;
	}

	void CameraMoveUp(){
		Vector3 raisePos = player.transform.position + Vector3.up * 5;
		Vector3 rotateAngle = new Vector3(90,0,0);
		playerCamera.MoveTo(raisePos, cameraRaiseTime, 0, EaseType.easeInOutQuad);
		playerCamera.RotateTo(rotateAngle,cameraRotateTime, 0, EaseType.easeInOutQuad);
		playerCamera.MoveTo(player.transform.position, cameraRaiseTime, lastTime + cameraRaiseTime, EaseType.easeInOutQuad);
		playerCamera.RotateTo(Vector3.zero,cameraRotateTime,lastTime + cameraRotateTime,EaseType.easeInOutQuad);
	}

	void DeleteGameObject(){
		player.GetComponent<CharacterMovement>().enabled = true;
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider col){
		CameraMoveUp();
		player.GetComponent<CharacterMovement>().enabled = false;
		Invoke("DeleteGameObject",cameraRotateTime + lastTime + cameraRotateTime);
	}
}
