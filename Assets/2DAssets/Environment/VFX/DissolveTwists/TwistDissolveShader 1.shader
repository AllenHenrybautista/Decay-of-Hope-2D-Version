Shader "Unlit/TwistDissolveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SmoothstepMin("SmoothstepMin", float) = 0
        _SmoothstepMax("SmoothstepMax", float) = 1
        _Intensity("Emission Intensity", float) = 1
        _TwistIntensity("Twist Intensity", float) = 1
        _Opacity("Opacity", float) = 1
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100
        Cull off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float lifetimeNormalized : SINGLE;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 vertexColor : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _SmoothstepMin;
            float _SmoothstepMax;
            float _Intensity;
            float _TwistIntensity;
            float _Opacity;

            void Unity_RadialShear_float(float2 UV, float2 Center, float Strength, float2 Offset, out float2 Out)
            {
                float2 delta = UV - Center;
                float delta2 = dot(delta.xy, delta.xy);
                float2 delta_offset = delta2 * Strength;
                Out = UV + float2(delta.y, -delta.x) * delta_offset + Offset;
            }
            
            v2f vert (appdata_full v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.vertexColor = v.color;
                o.lifetimeNormalized = v.texcoord.z;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float2 uv;
                Unity_RadialShear_float(i.uv, float2(0.5f, 0.5f), i.lifetimeNormalized * _TwistIntensity, float2(0.0f, 0.0f), uv);
                i.uv = uv;
                fixed4 col = tex2D(_MainTex, i.uv);

                float mainAlpha = col.a;
                float vertAlpha = i.vertexColor.a;
                float vertAndTexAlpha = saturate(mainAlpha * vertAlpha);
                float lifetimeNormalized = saturate(i.lifetimeNormalized);

                float dissolvedAlpha = saturate(vertAndTexAlpha - lifetimeNormalized);                
                float clippedDissolve = smoothstep(_SmoothstepMin, _SmoothstepMax, dissolvedAlpha);

                col = i.vertexColor * _Intensity;
                col.a = clippedDissolve * _Opacity;
                
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
