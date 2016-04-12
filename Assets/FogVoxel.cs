using UnityEngine;
using System.Collections;

public class FogVoxel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider collider){
		Debug.Log("Collision");
		if(collider.gameObject.tag == "Fog") {
			Debug.Log("should be destroyed");
			Destroy(collider.gameObject);
		}
		else{
			Debug.Log(collider.gameObject.tag);
		}
	}
}
