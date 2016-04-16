using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnBoardingScript : MonoBehaviour {

	Vector3 targetPosition;
	Vector3 targetRotation;
	GameObject playerCamera;
	public RawImage moveImage;
	public RawImage jumpImage;
	public RawImage glideImage;
	public GameObject firstObject;
	private Vector3 firstTarget;
	bool started;
	// Use this for initialization
	void Start () {
		started = false;
		GameObject player = GameObject.FindWithTag("Player");
		playerCamera = player.transform.FindChild("Main Camera").gameObject;
		targetPosition = gameObject.transform.parent.transform.position;
		targetRotation = gameObject.transform.parent.transform.rotation.eulerAngles;
		firstTarget = firstObject.transform.position;
		moveImage.enabled = false;
		jumpImage.enabled = false;
		glideImage.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && !started) {
			playerCamera.MoveTo(firstTarget, 8f, 0, EaseType.easeInOutQuad);
			playerCamera.RotateTo(targetRotation, 3f, 6f, EaseType.easeInOutQuad);
			started = true;
		}
		if(gameObject.transform.position == firstTarget) {
			playerCamera.MoveTo(targetPosition, 8f, 0f, EaseType.easeInOutQuad);
			moveImage.enabled = true;
			jumpImage.enabled = true;
			glideImage.enabled = true;
			StartCoroutine(FadeInControls(moveImage));
			StartCoroutine(FadeInControls(jumpImage));
			StartCoroutine(FadeInControls(glideImage));	
		}
	}

	IEnumerator FadeInControls(RawImage image) {
			float t = 0;
			Color fadedColor = image.color;
			Color targetColor = image.color;
			
			targetColor.a = 1f;
			while(t < 1f) {
				image.color = Color.Lerp(fadedColor, targetColor, t);
				t += 1f / 8f * Time.deltaTime;
				yield return null;
			}
	}
}
