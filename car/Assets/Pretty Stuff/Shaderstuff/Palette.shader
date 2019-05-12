Shader "Hidden/Palette"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_InputTexture ("Palette", 2D) = "white" {}
		_Size ("Size", Float) = 64
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			sampler2D _InputTexture;
			float _Size;
			

            float4 frag (v2f i) : SV_Target
            {
				fixed4 pixelCol = tex2D(_MainTex, i.uv);
				float minDist = 1000000.0;
				fixed4 color = fixed4(0., 0., 0.4, 1.);

				for (int x = 0; x < _Size; x++)
				{
					fixed4 paletteColor = tex2D(_InputTexture, float2(x/_Size, 0));
					float dist = 0;
					//dist = 1 - dot(paletteColor, pixelCol) / sqrt(dot(paletteColor, paletteColor) * dot(pixelCol, pixelCol));
					fixed4 diff = pixelCol - paletteColor;
					dist = dot(diff, diff);

					if(dist < minDist)
					{
						minDist = dist;
						color = paletteColor;
					}
				}
				return color;
            }
            ENDCG
        }
    }
}
