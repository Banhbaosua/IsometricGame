Shader "Shader Graphs/VFX_SnakeIdle" {
	Properties {
		_Opacity ("Opacity", Range(0, 1)) = 1
		_Emission_Power ("Emission_Power", Range(0, 1)) = 0
		[HDR] _Emission_Color ("Emission_Color", Vector) = (0,0,0,0)
		[NoScaleOffset] _Emission_Texture ("Emission_Texture", 2D) = "white" {}
		Vector2_5cc9ec8f76f948eab1452ca2c4d01060 ("Emission_Tiling", Vector) = (1,1,0,0)
		_Emisson_Speed ("Emisson_Speed", Vector) = (0,0,0,0)
		_Color ("Color", Vector) = (1,1,1,1)
		[NoScaleOffset] MainTex ("MainTex", 2D) = "white" {}
		Texture_Tiling ("Texture_Tiling", Vector) = (1,1,0,0)
		[NoScaleOffset] Mask_R_Emission ("Mask(R = Emission)", 2D) = "black" {}
		_Toon_Ramp_Smoothness ("Toon Ramp Smoothness", Range(0, 0.5)) = 0.5
		_Toon_Ramp_Offset ("Toon Ramp Offset", Range(0, 1)) = 0.5
		[HDR] _Aura_Color ("Aura_Color", Vector) = (1,0.7814316,0.03459108,0)
		_Aura_Power ("Aura_Power", Range(0, 1)) = 0
		[NoScaleOffset] _Aura_Texture ("Aura_Texture", 2D) = "white" {}
		_Aura_Tiling ("Aura_Tiling", Vector) = (1,1,0,0)
		Aura_Speed ("Aura_Speed", Vector) = (0,0,0,0)
		_Saturation ("Saturation", Range(0.35, 1)) = 1
		_Speed ("Speed", Float) = 4
		_Frequency ("Frequency", Float) = 3
		_Amplitude ("Amplitude", Float) = 0.3
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
	//CustomEditor "ShaderGraph.PBRMasterGUI"
}