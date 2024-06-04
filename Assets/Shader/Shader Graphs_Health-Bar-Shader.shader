Shader "Shader Graphs/Health-Bar-Shader" {
	Properties {
		[NoScaleOffset] _MainTex ("MainTex", 2D) = "white" {}
		_Percent ("Percent", Range(0, 1)) = 1.07
		[NoScaleOffset] _BarTexture ("BarTexture", 2D) = "white" {}
		_BackPercent ("BackPercent", Range(0, 1)) = 0
		[NoScaleOffset] _BackBar ("BackBar", 2D) = "white" {}
		_BackBarVisibility ("BackBarVisibility", Range(0, 1)) = 0
		_Alpha ("Alpha", Range(0, 1)) = 1
		_UVInitialPos ("UVInitialPos", Range(0, 1)) = 0.18
		_UVSize ("UVSize", Range(0, 1)) = 0.79
		[NoScaleOffset] _BarMask ("BarMask", 2D) = "white" {}
		_MaskOrUv ("MaskOrUv", Range(0, 1)) = 0.5
		_EnableAbnormalStatus ("EnableAbnormalStatus", Float) = 0
		_GeneralAbnormalFillAmount ("GeneralAbnormalFillAmount", Range(0, 1)) = 0
		_BurnFill ("BurnFill", Float) = 0
		_PoisonFill ("PoisonFill", Float) = 0
		_BleedFill ("BleedFill", Float) = 0
		_DoomFill ("DoomFill", Float) = 0
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

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
}