using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	CharacterController controller;
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
			float vertical = Input.GetAxis("Vertical");
			controller.Move(vertical * Camera.main.transform.forward * Time.deltaTime * 10f + Physics.gravity);
		if(Input.GetAxis("Horizontal") != 0) {
			transform.Rotate(0f, Input.GetAxis("Horizontal") * Time.deltaTime * 100.0f, 0f);
		}
	}
}
