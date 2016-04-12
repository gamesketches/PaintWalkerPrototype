using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FogOfWarManager : MonoBehaviour {

	#region Private
	[SerializeField]
	private int _textureSize = 256;
	[SerializeField]
	private Color _fogOfWarColor;
	[SerializeField]
	private LayerMask _fogOfWarLayer;

	private Texture2D _texture;
	private Color[] _pixels;
	private List<Revealer> _revealers;
	private int _pixelsPerUnit;
	private Vector2 _centerPixel;

	private static FogOfWarManager _instance;
	#endregion

	#region Public
	public static FogOfWarManager Instance
	{
		get
		{
			return _instance;
		}
	}
	#endregion

	private void Awake() {
		_instance = this;

		var renderer = GetComponent<Renderer>();
		Material fogOfWarMat = null;
		if(renderer != null) {
			fogOfWarMat = renderer.material;
		}

		if(fogOfWarMat == null) {
			Debug.LogError("Material for Fog of War not found!");
			return;
		}

		_texture = new Texture2D(_textureSize, _textureSize, TextureFormat.RGBA32, false);
		_texture.wrapMode = TextureWrapMode.Clamp;

		_pixels = _texture.GetPixels();
		ClearPixels();

		fogOfWarMat.mainTexture = _texture;

		_revealers = new List<Revealer>();

		_pixelsPerUnit = Mathf.RoundToInt(_textureSize/ transform.lossyScale.x);

		_centerPixel = new Vector2(_textureSize * 0.5f, _textureSize * 0.5f);
	}

	public void RegisterRevealer(Revealer revealer) {
		_revealers.Add(revealer);
	}

	private void ClearPixels() {
		for(var i = 0; i < _pixels.Length; i++) {
			_pixels[i] = _fogOfWarColor;
		}
	}

	private void CreateCircle(int originX, int originY, int radius) {
		for(var y = -radius * _pixelsPerUnit; y <= radius * _pixelsPerUnit; ++y) {
			for(var x = -radius * _pixelsPerUnit; x <= radius * _pixelsPerUnit; ++x) {
				if(x * x + y * y <= (radius * _pixelsPerUnit) * (radius * _pixelsPerUnit)) {
					_pixels[(originY + y) * _textureSize + originX + x] = new Color(0, 0, 0, 0);
				}
			}
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//ClearPixels();

		foreach(var revealer in _revealers) {
			Camera otherCamera = Camera.allCameras[1];
			var screenPoint = otherCamera.WorldToScreenPoint(revealer.transform.position);
			var ray = otherCamera.ScreenPointToRay(screenPoint);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 1000f, _fogOfWarLayer.value)) {
				var translatedPos = hit.point - transform.position;
				Debug.Log(translatedPos.y);

				var pixelPosX = Mathf.RoundToInt(translatedPos.x * _pixelsPerUnit + _centerPixel.x);
				var pixelPosY = Mathf.RoundToInt(translatedPos.y * _pixelsPerUnit + _centerPixel.y);

				CreateCircle(pixelPosX, pixelPosY, revealer.radius);
			}
			Debug.DrawRay(ray.origin, ray.direction * 1000f);
		}

		CreateCircle(50, 50, 10);
		_texture.SetPixels(_pixels);
		_texture.Apply(false);
	}
}
