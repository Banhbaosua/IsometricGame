Shader "Shader Graphs/VFX_Trail_Shader" {
	Properties {
		[HDR] _Color ("Color", Vector) = (1,1,1,0)
		[NoScaleOffset] _Main_Texture ("Main_Texture", 2D) = "white" {}
		_Main_Texture_Tiling ("Main_Texture_Tiling", Vector) = (1,1,0,0)
		_Main_Texture_Speed ("Main_Texture_Speed", Vector) = (-1,0,0,0)
		_Dissolve_Power ("Dissolve_Power", Range(0, 5)) = 1
		[NoScaleOffset] _Dissolve_Texture ("Dissolve_Texture", 2D) = "white" {}
		_Dissolve_Tiling ("Dissolve_Tiling", Vector) = (1,1,0,0)
		_Dissolve_Speed ("Dissolve_Speed", Vector) = (-1,0,0,0)
		_Distortion_Power ("Distortion_Power", Range(0, 1)) = 0.2
		[NoScaleOffset] _Distortion_Texture ("Distortion_Texture", 2D) = "white" {}
		_Distortion_Tiling ("Distortion_Tiling", Vector) = (1,1,0,0)
		_Distortion_Speed ("Distortion_Speed", Vector) = (1,0,0,0)
		_Alpha ("Alpha", Float) = 1
		_Min_Global_Alpha ("Min_Global_Alpha", Range(0, 1)) = 0
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
}