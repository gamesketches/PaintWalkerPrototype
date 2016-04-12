using UnityEngine;
using System.Collections;

public class FogOfWarCreator : MonoBehaviour {

	public Vector3 startCube;
	public float screenWidth;
	public float screenHeight;
	public Transform voxel;
	// Use this for initialization
	void Start () {
	
		for(float i = startCube.x; i < startCube.x + screenWidth; i += 1.0f) {
			for(float k = startCube.z; k <  startCube.z + screenHeight; k += 1.0f) {
				Instantiate(voxel, new Vector3(i, startCube.y, k), Quaternion.identity);
			}
		}
	}
}
