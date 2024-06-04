Shader "Shader Graphs/VFX_Distortion_Shader" {
	Properties {
		[NoScaleOffset] _Normal_Texture ("Normal_Texture", 2D) = "bump" {}
		_Tiling ("Tiling", Vector) = (1,1,0,0)
		_Speed ("Speed", Vector) = (0,0,0,0)
		[NoScaleOffset] _Mask ("Mask", 2D) = "white" {}
		_Refraction_Power ("Refraction_Power", Float) = 0
		_Min_Global_Alpha ("Min_Global_Alpha", Range(0, 1)) = 0
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
}