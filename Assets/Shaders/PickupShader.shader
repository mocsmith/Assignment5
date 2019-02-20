Shader "Unlit/MorganSmithShader 1"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

			#define mod(x,y) (x-y*floor(x/y))
			// Some useful functions
			float3 mod289(float3 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
			float2 mod289(float2 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
			float3 permute(float3 x) { return mod289(((x*34.0) + 1.0)*x); }


			//SCALE
			float2x2 scale(float2 _scale) {
				float2x2 e = float2x2(_scale.x, 0.0, 0.0, _scale.y);
				return e;
			}

			//SNOISE
			float snoise(float2 v) {

				// Precompute values for skewed triangular grid
				const float4 C = float4(0.211324865405187,
					// (3.0-sqrt(3.0))/6.0
					0.366025403784439,
					// 0.5*(sqrt(3.0)-1.0)
					-0.577350269189626,
					// -1.0 + 2.0 * C.x
					0.024390243902439);
				// 1.0 / 41.0

			// First corner (x0)
				float2 i = floor(v + dot(v, C.yy));
				float2 x0 = v - i + dot(i, C.xx);

				// Other two corners (x1, x2)
				float2 i1 = float2 (0.0,0.0);
				i1 = (x0.x > x0.y) ? float2(1.0, 0.0) : float2(0.0, 1.0);
				float2 x1 = x0.xy + C.xx - i1;
				float2 x2 = x0.xy + C.zz;

				// Do some permutations to avoid
				// truncation effects in permutation
				i = mod289(i);
				float3 p = permute(
					permute(i.y + float3(0.0, i1.y, 1.0))
					+ i.x + float3(0.0, i1.x, 1.0));

				float3 m = max(0.5 - float3(
					dot(x0, x0),
					dot(x1, x1),
					dot(x2, x2)
				), 0.0);

				m = m * m;
				m = m * m;

				// Gradients:
				//  41 pts uniformly over a line, mapped onto a diamond
				//  The ring size 17*17 = 289 is close to a multiple
				//      of 41 (41*7 = 287)

				float3 x = 2.0 * frac(p * C.www) - 1.0;
				float3 h = abs(x) - 0.5;
				float3 ox = floor(x + 0.5);
				float3 a0 = x - ox;

				// Normalise gradients implicitly by scaling m
				// Approximation of: m *= inversesqrt(a0*a0 + h*h);
				m *= 1.79284291400159 - 0.85373472095314 * (a0*a0 + h * h);

				// Compute final noise value at P
				float3 g = float3 (0.0,0.0,0.0);
				g.x = a0.x  * x0.x + h.x  * x0.y;
				g.yz = a0.yz * float2(x1.x, x2.x) + h.yz * float2(x1.y, x2.y);
				return 130.0 * dot(m, g);
			}

			#define OCTAVES 4

			// Ridged multifractal
			// See "Texturing & Modeling, A Procedural Approach", Chapter 12
			float ridge(float h, float offset) {
				h = abs(h);     // create creases
				h = offset - h; // invert so creases are at top
				h = h * h;      // sharpen creases
				return h;
			}

			float ridgedMF(float2 p) {
				float lacunarity = 2.0;
				float gain = 0.5;
				float offset = 0.9;

				float sum = 0.0;
				float freq = 1.0, amp = 0.5;
				float prev = 1.0;
				for (int i = 0; i < OCTAVES; i++) {
					float n = ridge(snoise(p*freq), offset);
					sum += n * amp;
					sum += n * amp*prev;  // scale by previous octave
					prev = n;
					freq *= lacunarity;
					amp *= gain;
				}
				return sum;
			}
			float3 hsb2rgb(in float3 c) {
				float3 rgb = clamp(abs(mod(c.x*6.0 + float3(0.0, 4.0, 2.0),
					6.0) - 3.0) - 1.0,
					0.0,
					1.0);
				rgb = rgb * rgb*(3.0 - 2.0*rgb);
				return c.z * lerp(float3 (1.0,1.0,1.0), rgb, c.y);
			}

			#define PI 3.14159265359
			fixed4 frag(v2f i) : SV_Target
			{
				// Normalized pixel coordinates (from 0 to 1)
				float2 uv = i.uv;
				float time = _Time.y;

				//fixed aspect radio
				float fx = _ScreenParams.x / _ScreenParams.y;
				//uv.x *= fx;

				float2 p = float2(0.5*fx, 0.5) - uv;

				//define radius and angle
				float r = length(p);
				float a = atan2(p.y, p.x);

				//Make a snoise
				float n = snoise(float2(uv.x*100., uv.y*100. + _Time.y)*0.002);

				//This is where the magic happens : A ridgedMF inside a ridgedMF
				float e = ridgedMF(float2(0.5, 0.5)
					*(ridgedMF(float2(uv.x*0.8, uv.y*0.5 - time * 0.1)))
					+ n
					* ridgedMF(float2(uv.x*1., uv.y + time * 0.002)
						*ridgedMF(float2(uv.x*0.5, uv.y*0.5 + time * 0.02))));


				//VERSION 2 :
				/*e = ridgedMF(float2(uv.x*0.5*uv.y,uv.y*0.5)
						 *(ridgedMF(float2(uv.x*2.2,uv.y*2.0+time*0.01)))
						 +n
						 *ridgedMF(float2(uv.x,uv.y)*ridgedMF(float2(uv.x*0.5,uv.y*0.5-time*0.002))));
				*/
				//simplify version : 
				//e = ridgedMF(float2(ridgedMF(uv)));


				//Some colors: 
				float3 col1 = float3(0.1, 0.1, 0.4);
				float3 col2 = float3(1.9, 0.8, 1. - r);

				//mixing color1 and color2 with the pattern
				float3 fin = lerp(col1, col2, e);
				//fin = float3(smoothstep(0.2,0.9,fin));
				//fin = hsb2rgb(float3(e,1.0,1));

				return float4(fin, 1.0);
			}

            ENDCG
        }
    }
}
