using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour {

	CharacterController controller;

	public float jumpSpeed = 30.0f;
	public GameObject fogOfWar;
	//private Mesh fogOfWarMesh;
	private Vector3 jumpVector;
	public RawImage frame;
	public RawImage titleScreen;
	public RawImage titleScreenBackground;
	public Camera otherCamera;
	// Use this for initialization
	void Start () {
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
		if(titleScreen.enabled && Input.GetAxis("Jump") != 0) {
			StartCoroutine(FadeOutTitleScreen());
			otherCamera.enabled = true;
		}
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

		Ray theRay = new Ray(Camera.main.transform.position, Vector3.up * 100f);
		RaycastHit hit;

		if(Physics.Raycast(theRay, out hit)) {
			hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0f);
/*			Debug.Log(fogOfWarMesh.vertices.Length);
			Debug.Log(hit.triangleIndex);
			Debug.Log(fogOfWarMesh.vertices[fogOfWarMesh.triangles[hit.triangleIndex]]);
			Transform hitTransform = hit.collider.transform;
			fogOfWarMesh.colors[fogOfWarMesh.triangles[hit.triangleIndex]].a = 0f;
			Color[] colorArray = fogOfWarMesh.colors;
			colorArray[fogOfWarMesh.triangles[hit.triangleIndex]] = new Color(0f, 0f, 0f, 0f);
			fogOfWarMesh.colors = colorArray;
			Debug.Log(fogOfWarMesh.colors[fogOfWarMesh.triangles[hit.triangleIndex]]);
			Vector3 hitPoint = hitTransform.TransformPoint(fogOfWarMesh.vertices[fogOfWarMesh.triangles[hit.triangleIndex]]);
			Debug.Log(hitPoint);*/
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
	}

	public void DisableFrame() {
		frame.enabled = false;
		frame.GetComponentInChildren<Text>().text = "";
	}
}
