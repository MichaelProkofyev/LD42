Shader "Custom/ScrollingNoise"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("NoiseTexture", 2D) = "white" {}
        _WarpedTex("Warped tex", 2D) = "white" {}
        _WarpAmount("Warp amount", Float)  = 0
        _DirectionX("Direction X", Float)  = 0
        _DirectionY("Direction Y", Float)  = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

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
            sampler2D _NoiseTex;
            sampler2D _WarpedTex;
            float _WarpAmount;

            float _DirectionX;
            float _DirectionY;

			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the noise
                float2 noise_uv = i.uv + float2(_DirectionX, _DirectionY) * _Time.y;
                fixed2 noise_value = tex2D(_NoiseTex, noise_uv);

                fixed4 col = tex2D(_WarpedTex, i.uv + noise_value * _WarpAmount);


				// apply fog
				return col;
			}
			ENDCG
		}
	}
}
