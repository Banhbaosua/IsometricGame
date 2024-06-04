Shader "Shader Graphs/Liquid_Shader" {
	Properties {
		_Specular ("Specular", Range(0, 1)) = 0.5
		_Color ("Color", Vector) = (1,1,1,0)
		_Fade ("Fade", Float) = 10
		_Emission ("Emission", Range(0, 1)) = 1
		[NoScaleOffset] _Gradient_Color ("Gradient_Color", 2D) = "white" {}
		[NoScaleOffset] _Texture_Noise ("Texture_Noise", 2D) = "white" {}
		_Tilling ("Tilling", Vector) = (1,1,0,0)
		_Speed ("Speed", Vector) = (0,0,0,0)
		_Voronoi_Scale ("Voronoi_Scale", Float) = 5
		_Voronoi_Power ("Voronoi_Power", Float) = 4
		_Voronoi_Speed ("Voronoi_Speed", Vector) = (0,0.5,0,0)
		_Voronoi_Agitation ("Voronoi_Agitation", Float) = 2
		_Voronoi_Tiling ("Voronoi_Tiling", Vector) = (1,1,0,0)
		_Normal_Scale ("Normal_Scale", Float) = 10
		_Normal_Strenghth ("Normal_Strenghth", Float) = 0.05
		_Normal_Speed ("Normal_Speed", Vector) = (0,0.5,0,0)
		_WobbleSpeed ("WobbleSpeed", Float) = 1
		_WobbleFrequency ("WobbleFrequency", Float) = 1
		_Wobble_Distance ("Wobble_Distance", Float) = 1
		_Wobble_Amount ("Wobble_Amount", Range(0, 1)) = 0
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