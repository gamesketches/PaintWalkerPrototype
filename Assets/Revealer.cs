using UnityEngine;
using System.Collections;

public class Revealer : MonoBehaviour {


	public int radius;
	private FogOfWarManager manager;
	// Use this for initialization
	void Start () {
		manager = FogOfWarManager.Instance;
		manager.RegisterRevealer(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
