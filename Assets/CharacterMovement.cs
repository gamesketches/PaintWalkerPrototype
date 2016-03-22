using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	CharacterController controller;

	public float jumpSpeed = 30.0f;
	private Vector3 jumpVector;
	// Use this for initialization
	void Start () {
		jumpVector = Vector3.zero;
//		rb = GetComponent<Rigidbody>();
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
			float vertical = Input.GetAxis("Vertical");
			Vector3 moveVector = (vertical * Camera.main.transform.forward * Time.deltaTime * 2f + Physics.gravity);
			Debug.Log(jumpVector);
		controller.Move(moveVector + jumpVector);
		if(Input.GetAxis("Horizontal") != 0) {
			transform.Rotate(0f, Input.GetAxis("Horizontal") * Time.deltaTime * 100.0f, 0f);
		}
		if(Input.GetKeyDown(KeyCode.Space)) {
			jumpVector.y = jumpSpeed;
			Debug.Log(jumpVector.y);
		}
		if(!controller.isGrounded) {
			jumpVector.y -= 1f * Time.deltaTime;
		}

	}
}
