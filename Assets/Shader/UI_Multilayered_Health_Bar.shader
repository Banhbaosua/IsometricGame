Shader "UI/Multilayered_Health_Bar" {
	Properties {
		[HideInInspector] _MainTex ("Texture", 2D) = "white" {}
		[HideInInspector] _StencilComp ("Stencil Comparison", Float) = 8
		[HideInInspector] _Stencil ("Stencil ID", Float) = 0
		[HideInInspector] _StencilOp ("Stencil Operation", Float) = 0
		[HideInInspector] _StencilWriteMask ("Stencil Write Mask", Float) = 255
		[HideInInspector] _StencilReadMask ("Stencil Read Mask", Float) = 255
		[HideInInspector] _ColorMask ("Color Mask", Float) = 15
		[Toggle(UNITY_UI_ALPHACLIP)] [HideInInspector] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
		[Toggle] _EnableAbnormalStatus ("Enable Abnormal Status", Float) = 0
		_GeneralAbnormalFillAmount ("General Abnormal Fill Amount", Range(0, 1)) = 0
		_DoomFill ("Doom Fill", Float) = 0
		_BleedFill ("Bleed Fill", Float) = 0
		_PoisonFill ("Poison Fill", Float) = 0
		_BurnFill ("Burn Fill", Float) = 0
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
}