Shader "Shader Graphs/Grass_Shader" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,0)
		[NoScaleOffset] _Normal ("Normal", 2D) = "white" {}
		[NoScaleOffset] _Main_Texture ("Main_Texture", 2D) = "white" {}
		_NormalStrenght ("NormalStrenght", Float) = 1
		_Smoothness ("Smoothness", Float) = 0
		_AmbientOcclusion ("AmbientOcclusion", Float) = 1
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