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
		player.GetComponent<CharacterMovement>().EnableFrame();
	}

	void DeleteGameObject(){
		player.GetComponent<CharacterMovement>().enabled = true;
		player.GetComponent<CharacterMovement>().DisableFrame();
		ChangeColor();
	}

	void OnTriggerEnter(Collider col){
		CameraMoveUp();
		player.GetComponent<CharacterMovement>().enabled = false;
		hideObject(0.0f);
		Invoke("DeleteGameObject",cameraRotateTime + lastTime + cameraRotateTime);
	}

	IEnumerator CheckInput() {
		while(Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f 
			&& Input.GetAxis("Jump") == 0f) {
			yield return null;
		}
		DeleteGameObject();
	}

	void ChangeColor() {
		Renderer[] myRenderers = GetComponentsInChildren<Renderer>();
		for(int i = 0; i < myRenderers.Length; i++) {
			for(int k = 0; k < myRenderers[i].materials.Length; k++) {
				if(myRenderers[i].materials[k].color != Color.white) {
					myRenderers[i].materials[k].color = Color.green;
				}
			}
		}
		hideObject(0.2f);
	}

	void hideObject(float alpha) {
		Renderer[] myRenderers = GetComponentsInChildren<Renderer>();
		for(int i = 0; i < myRenderers.Length; i++) {
			for(int k = 0; k < myRenderers[i].materials.Length; k++) {
				if(myRenderers[i].materials[k].color != Color.white) {
					myRenderers[i].materials[k].color = Color.green;
				}
				Color alphaedOutColor = myRenderers[i].materials[k].color;
				alphaedOutColor.a = alpha;
				myRenderers[i].materials[k].color = alphaedOutColor;
			}
		}
	}
}
