using UnityEngine;
using System.Collections;

public class ViveMovement : MonoBehaviour {

	int controllerIndex;
	public GameObject painting;
	// Use this for initialization
	void Start () {
		controllerIndex = (int)gameObject.transform.parent.gameObject.GetComponent<SteamVR_TrackedObject>().index;
	}
	
	// Update is called once per frame
	void Update () {
		var device = SteamVR_Controller.Input(controllerIndex);
		if(device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad)) {
				Vector2 coordinates = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
				Vector3 movementVector = coordinates * Time.deltaTime;
				painting.transform.Translate(movementVector);
		}
	}
}
