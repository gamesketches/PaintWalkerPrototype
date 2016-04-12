Shader "Custom/FogOfWar" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		LOD 200
		Lighting off
		
		pass {
		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag

		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;

		fixed4 frag(v2f_img i) : SV_Target {
			return tex2D(_MainTex, i.uv);
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
