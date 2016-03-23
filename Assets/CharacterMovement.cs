using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	CharacterController controller;

	public float jumpSpeed = 30.0f;
	public GameObject fogOfWar;
	private Mesh fogOfWarMesh;
	private Vector3 jumpVector;
	// Use this for initialization
	void Start () {
		jumpVector = Vector3.zero;
//		rb = GetComponent<Rigidbody>();
		controller = GetComponent<CharacterController>();
		Color meshColor = fogOfWar.GetComponent<Renderer>().material.color;
		fogOfWarMesh = fogOfWar.GetComponent<MeshCollider>().sharedMesh;
		Color[] colors = new Color[fogOfWarMesh.vertexCount];
		for(int i = 0; i < fogOfWarMesh.vertexCount; i++) {
			colors[i] = meshColor;
		}
		fogOfWarMesh.colors = colors;
	}
	
	// Update is called once per frame
	void Update () {
		float vertical = Input.GetAxis("Vertical");
		Vector3 moveVector = (vertical * Camera.main.transform.forward * Time.deltaTime * 2f + Physics.gravity);
		//Debug.Log(jumpVector);
		controller.Move(moveVector + jumpVector);
		if(Input.GetAxis("Horizontal") != 0) {
			transform.Rotate(0f, Input.GetAxis("Horizontal") * Time.deltaTime * 100.0f, 0f);
		}
		if(Input.GetKeyDown(KeyCode.Space)) {
			jumpVector.y = jumpSpeed;
			//Debug.Log(jumpVector.y);
		}
		if(!controller.isGrounded) {
			jumpVector.y -= 1f * Time.deltaTime;
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
}
