Shader "Unlit/Sprite_RadialFill" {
	Properties {
		[Toggle] _HasTexture ("Has Texture", Float) = 0
		_MainTex ("Texture", 2D) = "" {}
		_BGTex ("Tex", 2D) = "" {}
		_Color ("Color (RGBA)", Vector) = (1,1,1,1)
		_BGColor ("Background Color", Vector) = (1,1,1,1)
		_InnerRadius ("Inner Radius", Range(0, 1)) = 0.5
		_Smoothness ("Smoothness", Range(0, 1)) = 0
		_Angle ("Angle", Range(0, 360)) = 0
		_Fill ("Fill", Range(0, 1)) = 1
		[Toggle] _Flip ("Flip", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}