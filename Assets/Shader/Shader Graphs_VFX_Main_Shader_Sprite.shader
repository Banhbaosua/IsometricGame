Shader "Shader Graphs/VFX_Main_Shader_Sprite" {
	Properties {
		_Emission ("Emission", Float) = 1
		[NoScaleOffset] _Texture ("Texture", 2D) = "white" {}
		Vector2_af07d33c91554b21b115296a31e6dc00 ("Speed", Vector) = (-5,0,0,0)
		_Sheet_Width ("Sheet_Width", Float) = 2
		_Sheet_Height ("Sheet_Height", Float) = 8
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