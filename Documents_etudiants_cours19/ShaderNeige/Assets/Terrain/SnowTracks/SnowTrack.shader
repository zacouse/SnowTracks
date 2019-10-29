﻿Shader "Custom/SnowTrack"
{
	Properties
	{
		_Tess("Tesselation", Range(1,32)) = 4
		_SnowColor("Snow Color", Color) = (1,1,1,1)
		_SnowText("Snow (RGB)",2D) = "white" {}
		_GroundColor("Ground Color", Color) = (1,1,1,1)
		_GroundText("Ground (RGB)",2D) = "white" {}
		_Splat("Splat",2D) = "black" {}
		_Displacement("Displacement",Range(0,1)) = 0.3
		_Glossiness("Glossiness",Range(0,1)) = 0.5
		_Metallic("Metallic",Range(0,1)) = 0.0
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
			// apply fog
			UNITY_APPLY_FOG(i.fogCoord, col);
			return col;
		}
		ENDCG
	}
	}
}
