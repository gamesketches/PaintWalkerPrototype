using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour {

	CharacterController controller;

	public float jumpSpeed = 20.0f;
	public GameObject fogOfWar;
	//private Mesh fogOfWarMesh;
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
//		rb = GetComponent<Rigidbody>();
		controller = GetComponent<CharacterController>();
		//Color meshColor = fogOfWar.GetComponent<Renderer>().material.color;
		//fogOfWarMesh = fogOfWar.GetComponent<MeshCollider>().sharedMesh;
		//Color[] colors = new Color[fogOfWarMesh.vertexCount];
		//for(int i = 0; i < fogOfWarMesh.vertexCount; i++) {
		//	colors[i] = meshColor;
		//}
		//fogOfWarMesh.colors = colors;
		frame.enabled = false;
		Debug.Log(frame.enabled);
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
			if(Input.GetKeyDown(KeyCode.Space) && controller.isGrounded) {
				jumpVector.y = jumpSpeed;
			}
			if(!controller.isGrounded) {
				if(Input.GetKey(KeyCode.Space) && jumpVector.y < Mathf.Abs(Physics.gravity.y)) {
					jumpVector.y = 9.79f;
				}
				else {
					jumpVector.y -= 1f * Time.deltaTime;
				}
			}
			if(titleScreen.enabled && Input.GetAxis("Jump") != 0) {
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
		//controls[2].enabled = false;
		StartCoroutine(FadeOutFrame());
	}

	IEnumerator FadeOutFrame() {
		float t = 0;
		Text text = frame.GetComponentInChildren<Text>();
		Text headerText = frame.GetComponentsInChildren<Text>()[1];
		Color startColor = frame.color;
		Color textColor = text.color;
		Color fadedColor = frame.color;
		Color fadedTextColor = text.color;
		Color headerTextColor = headerText.color;
		Color fadedHeaderTextColor = headerText.color;
		fadedColor.a = 0;
		fadedTextColor.a = 0;
		fadedHeaderTextColor.a = 0;
		while(t < 1f) {
			frame.color = Color.Lerp(startColor, fadedColor, t);
			text.color = Color.Lerp(textColor, fadedTextColor, t);
			headerText.color = Color.Lerp(headerTextColor, fadedHeaderTextColor, t);
			t += Time.deltaTime * 0.1f;
			yield return null;
		}
	}

	public void DisableFrame() {
		Color frameColor = frame.color;
		frameColor.a = 1;
		frame.color = frameColor;
		Color textColor = frame.GetComponentInChildren<Text>().color;
		Color headerTextColor = frame.GetComponentsInChildren<Text>()[1].color;
		headerTextColor.a = 1;
		textColor.a = 1;
		frame.GetComponentInChildren<Text>().color = textColor;
		frame.GetComponentsInChildren<Text>()[1].color = headerTextColor;
		frame.enabled = false;
		frame.GetComponentInChildren<Text>().text = "";
		frame.GetComponentsInChildren<Text>()[1].text = "";
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		renderer.enabled = true;
		renderer.material.color = Color.white;
		RawImage[] controls = GetComponentsInChildren<RawImage>();
		controls[0].enabled = true;
		controls[1].enabled = true;
		//controls[2].enabled = true;
	}
}
