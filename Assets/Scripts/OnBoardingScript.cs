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
	private GameObject[] waypoints;
	AudioSource music;
	bool started;
	bool controlsFaded;
	// Use this for initialization
	void Start () {
		music = GetComponent<AudioSource>();
		started = false;
		controlsFaded = false;
		GameObject player = GameObject.FindWithTag("Player");
		waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
		playerCamera = player.transform.FindChild("Main Camera").gameObject;
		player.GetComponent<MeshRenderer>().enabled = false;
		targetPosition = gameObject.transform.parent.transform.position;
		//targetPosition.y += 0.3f;
		targetRotation = gameObject.transform.parent.transform.rotation.eulerAngles;
		targetRotation.x += 20.0f;
		firstTarget = firstObject.transform.position;
		moveImage.enabled = false;
		jumpImage.enabled = false;
		glideImage.enabled = false;
		foreach(GameObject waypoint in waypoints){
			waypoint.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Jump") != 0f && !started) {
			music.Play();
			playerCamera.MoveTo(firstTarget, 9.5f, 0, EaseType.easeInOutQuad);
			playerCamera.RotateTo(targetRotation, 10f, 2f, EaseType.easeInOutQuad);
			started = true;
			GameObject player = GameObject.FindWithTag("Player");
		}
		if(gameObject.transform.position == firstTarget) {
			playerCamera.MoveTo(targetPosition, 9f, 0f, EaseType.easeInOutQuad);
			moveImage.enabled = true;
			jumpImage.enabled = true;
			glideImage.enabled = true;
			StartCoroutine(FadeInControls(moveImage));
			StartCoroutine(FadeInControls(jumpImage));
			StartCoroutine(FadeInControls(glideImage));	
			Invoke("BeginControlFadeOut", 45.0f);
		}
	}

	IEnumerator FadeInControls(RawImage image) {
		if(!controlsFaded) {
			float t = 0;
			Color fadedColor = image.color;
			Color targetColor = image.color;
			
			targetColor.a = 1f;
			while(t < 1f && !controlsFaded) {
				image.color = Color.Lerp(fadedColor, targetColor, t);
				t += 1f / 9f * Time.deltaTime;
				yield return null;
			}
		}
		foreach(GameObject waypoint in waypoints){
			waypoint.SetActive(true);
		}
		GameObject player = GameObject.FindWithTag("Player");
		CharacterMovement playerMovementScript = player.GetComponent<CharacterMovement>();
		playerMovementScript.onboarding = false;
		//player.GetComponent<MeshRenderer>().enabled = true;
		GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().enabled = true;
	}

	IEnumerator FadeOutControls(RawImage image) {
		float t = 0;
		Color startColor = image.color;
		Color targetColor = image.color;

		targetColor.a = 0f;
		while(t < 1f) {
			image.color = Color.Lerp(startColor, targetColor, t);
			t += 1f / 8f * Time.deltaTime;
			yield return null;
		}
	}

	void BeginControlFadeOut() {
		controlsFaded = true;
		StartCoroutine(FadeOutControls(moveImage));
		StartCoroutine(FadeOutControls(jumpImage));
		StartCoroutine(FadeOutControls(glideImage));
	}


}
