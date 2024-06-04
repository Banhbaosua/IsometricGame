Shader "Shader Graphs/ToonShading_TransparentWall" {
	Properties {
		_Emission_Power ("Emission_Power", Range(0, 1)) = 0
		[HDR] _Emission_Color ("Emission_Color", Vector) = (0,0,0,0)
		[NoScaleOffset] Mask_R_Emission ("Mask(R = Emission)", 2D) = "black" {}
		[NoScaleOffset] _Emission_Texture ("Emission_Texture", 2D) = "white" {}
		_Emission_Tiling ("Emission_Tiling", Vector) = (1,1,0,0)
		_Emisson_Speed ("Emisson_Speed", Vector) = (0,0,0,0)
		_Specular ("Specular", Range(0, 0.5)) = 0.2
		[NoScaleOffset] _Specular_Mask ("Specular_Mask", 2D) = "white" {}
		_Color ("Color", Vector) = (1,1,1,1)
		[NoScaleOffset] MainTex ("MainTex", 2D) = "white" {}
		Texture_Tiling ("Texture_Tiling", Vector) = (1,1,0,0)
		_Hue ("Hue", Range(0, 1)) = 0
		[HDR] _XP_Aura_Color ("XP_Aura_Color", Vector) = (1,0.7814316,0.03459108,0)
		_XP_Aura_Power ("XP_Aura_Power", Range(0, 1)) = 0
		[NoScaleOffset] _XP_Aura_Texture ("XP_Aura_Texture", 2D) = "white" {}
		Vector2_b4bb9edeb256422d9085c41869d6cd3f ("Aura_Tiling", Vector) = (1,1,0,0)
		Aura_Speed ("Aura_Speed", Vector) = (0,0,0,0)
		[HDR] _Cure_Aura_Color ("Cure_Aura_Color", Vector) = (0.3574302,0.8773585,0.02207184,0)
		_Cure_Aura_Power ("Cure_Aura_Power", Range(0, 1)) = 0
		[NoScaleOffset] _Cure_Aura_Texture ("Cure_Aura_Texture", 2D) = "white" {}
		Vector2_7adb351140ff4d32972d9805e5aef77f ("Cure_Tiling", Vector) = (1,1,0,0)
		Cure_Speed ("Cure_Speed", Vector) = (0,0,0,0)
		_Saturation ("Saturation", Range(0.35, 1)) = 1
		_Damage_Power ("Damage_Power", Range(0, 1)) = 0
		_Cutout_Size ("Cutout Size", Float) = 0.1
		_Falloff_Size ("Falloff Size", Float) = 0.05
		_Cutout_Position ("Cutout Position", Vector) = (0.5,0.5,0,0)
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