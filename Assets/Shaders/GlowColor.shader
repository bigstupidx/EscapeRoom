Shader "Custom/GlowColor" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		ZTest LEqual
		ZWrite Off

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;

			v2f vert (appdata v) {
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}

			fixed4 frag (v2f i) : COLOR {
				return fixed4(1, 0.5, 0, 1);
			}
			ENDCG
		}
	}

	SubShader {
		Tags { "RenderType"="Transparent" }
		ZTest LEqual
		ZWrite Off

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata {
				float4 color : COLOR;
       			float2 uv : TEXCOORD0;
       			float4 vertex : POSITION;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
       			float4 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;

			v2f vert (appdata v) {
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag (v2f i) : COLOR {
				return tex2D(_MainTex, i.uv).a * i.color.a * _Color.a * fixed4(1, 0.5, 0, 1);
			}
			ENDCG
		}
	}

	SubShader {
		Tags { "RenderType"="TransparentCutout" }
		ZTest LEqual
		ZWrite Off

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata {
				float4 color : COLOR;
       			float2 uv : TEXCOORD0;
       			float4 vertex : POSITION;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
       			float4 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;

			v2f vert (appdata v) {
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag (v2f i) : COLOR {
				return tex2D(_MainTex, i.uv).a * i.color.a * _Color.a * fixed4(1, 0.5, 0, 1);
			}
			ENDCG
		}
	}


}
