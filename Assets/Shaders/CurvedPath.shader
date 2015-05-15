Shader "Custom/CurvedPath" {
	Properties {
		_Color ("Tint Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_QOffset ("Offset", Vector) = (0,0,0,0)
		_Dist ("Distance", Float) = 100.0
	}
	SubShader {
		Tags 
		{ 
			"RenderType"="Transparent"
		}
		
		Pass
		{
			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma exclude_renderers ps3 xbox360 flash
			#pragma fragmentoption ARB_precision_hint_forecast
			#pragma vertex vert
			#pragma fragment frag

            uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform fixed4 _Color;
			uniform fixed4 _LightColor0;
			
			float4 _QOffset;
			float _Dist;
			
			struct vertexInput {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;			
			};
			
			struct fragmentInput {
			    float4 pos : SV_POSITION;
			    fixed4 color : COLOR0;
			    half2 uv : TEXCOORD0;
			}; 

			fragmentInput vert (vertexInput v)
			{
			    fragmentInput o;
			    float4 vPos = mul (UNITY_MATRIX_MV, v.vertex);
			    float zOff = vPos.z/_Dist;
			    vPos += _QOffset*zOff*zOff;
			    o.pos = mul (UNITY_MATRIX_P, vPos);
			    o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				
				o.color = _Color;
				
			    return o;
			}

			half4 frag (fragmentInput i) : COLOR
			{
			    return tex2D(_MainTex, i.uv) * i.color;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}