using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour {

	CharacterController controller;

	public float jumpSpeed = 20.0f;
	public GameObject fogOfWar;
	private Vector3 jumpVector;
	public RawImage frame;
	public RawImage titleScreen;
	public RawImage titleScreenBackground;
	public Camera otherCamera;
	public bool onboarding;
	// Use this for initialization
	void Start () {
		onboarding = false;
		otherCamera.enabled = false;
		jumpVector = Vector3.zero;
		controller = GetComponent<CharacterController>();
		frame.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene(0);
		}
		if(!onboarding) {

			float vertical = Input.GetAxis("Vertical");
			Vector3 moveVector = (vertical * Camera.main.transform.forward * Time.deltaTime * 2f + Physics.gravity);
			controller.Move(moveVector + jumpVector);
			if(Input.GetAxis("Horizontal") != 0) {
				transform.Rotate(0f, Input.GetAxis("Horizontal") * Time.deltaTime * 100.0f, 0f);
			}
			if(Input.GetAxis("Jump") != 0f && controller.isGrounded) {
				jumpVector.y = jumpSpeed;
			}
			if(!controller.isGrounded) {
				if(Input.GetAxis("Jump") != 0f && jumpVector.y < Mathf.Abs(Physics.gravity.y)) {
					jumpVector.y = 9.79f;
				}
				else {
					jumpVector.y -= 1f * Time.deltaTime;
				}
			}
			if(titleScreen.enabled && Input.GetAxis("Jump") != 0f && !onboarding) {
				StartCoroutine(FadeOutTitleScreen());
				otherCamera.enabled = true;
				onboarding = true;
				jumpVector.y = 0;
			}
			Ray theRay = new Ray(Camera.main.transform.position, Vector3.up * 100f);
			RaycastHit hit;

			if(Physics.Raycast(theRay, out hit)) {
				hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0f);
			}
		}
	}

	IEnumerator FadeOutTitleScreen() {
		float t = 0;
		Color fadedColor1 = titleScreen.color;
		Color fadedColor2 = titleScreenBackground.color;
		Color startColor = titleScreen.color;
		Color startColor2 = titleScreenBackground.color;
		fadedColor1.a = 0;
		fadedColor2.a = 0;
		while(t < 1f) {
			titleScreen.color = Color.Lerp(startColor, fadedColor1, t);
			titleScreenBackground.color = Color.Lerp(startColor2, fadedColor2, t);
			t += Time.deltaTime;
			yield return null;
		}
		titleScreen.enabled = false;
		titleScreenBackground.enabled = false;
	}

	public void EnableFrame(string info){
		frame.enabled = true;
		frame.GetComponentInChildren<Text>().text = info;
		frame.GetComponentsInChildren<Text>()[1].text = "Sunflowers";
		GetComponent<MeshRenderer>().enabled = false;
		RawImage[] controls = GetComponentsInChildren<RawImage>();
		controls[0].enabled = false;
		controls[1].enabled = false;
		StartCoroutine(FadeOutFrame());
	}

	IEnumerator FadeOutFrame() {
		float t = 0;
		foreach(Text text in frame.GetComponentsInChildren<Text>()){
			StartCoroutine(FadeOutTextElement(text));
		}
		Color startColor = frame.color;
		Color fadedColor = frame.color;
		fadedColor.a = 0;
		while(t < 1f) {
			frame.color = Color.Lerp(startColor, fadedColor, t);
			t += Time.deltaTime * 0.1f;
			yield return null;
		}
	}

	IEnumerator FadeOutTextElement(Text theText) {
		Color startColor = theText.color;
		Color fadedColor = startColor;
		fadedColor.a = 0;
		float t = 0;
		while(t < 1f) {
			theText.color = Color.Lerp(startColor, fadedColor, t);
			t += Time.deltaTime * 0.1f;
			yield return null;
		}
	}

	public void DisableFrame() {
		Color frameColor = frame.color;
		frameColor.a = 1;
		frame.color = frameColor;
		foreach(Text text in frame.GetComponentsInChildren<Text>()) {
			Color tempColor = text.color;
			tempColor.a = 1;
			text.color = tempColor;
			text.text = "";
		}
		frame.enabled = false;
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		renderer.enabled = true;
		renderer.material.color = Color.white;
		RawImage[] controls = GetComponentsInChildren<RawImage>();
		controls[0].enabled = true;
		controls[1].enabled = true;
	}
}
