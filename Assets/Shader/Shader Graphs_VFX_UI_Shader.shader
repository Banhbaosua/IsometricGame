Shader "Shader Graphs/VFX_UI_Shader" {
	Properties {
		_Emission ("Emission", Range(0, 5)) = 1
		_Color ("Color", Vector) = (1.498039,1.498039,1.498039,0)
		[NoScaleOffset] _Main_Texture ("Main_Texture", 2D) = "white" {}
		[HDR] _Noise_Color ("Noise_Color", Vector) = (1,1,1,0)
		[NoScaleOffset] _Noise ("Noise", 2D) = "bump" {}
		_Noise_Opacity ("Noise_Opacity", Range(0, 1)) = 0
		_Noise_Tiling ("Noise_Tiling", Vector) = (1,1,0,0)
		_Noise_Speed ("Noise_Speed", Vector) = (0,0,0,0)
		[NoScaleOffset] _Dissolve_Texture ("Dissolve_Texture", 2D) = "white" {}
		_Dissolve ("Dissolve", Range(0, 1)) = 0.5
		_Dissolve_Size ("Dissolve_Size", Range(0, 1)) = 0
		_Opacity ("Opacity", Range(0, 1)) = 0.2
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