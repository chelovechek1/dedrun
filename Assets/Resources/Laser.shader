Shader "Unlit/Laser"
{
    Properties
    {
        _color("color", Color) = (0,0,0,0)
        _time("time", Float) = 0.
        _pixelSize("Size of pixels", Float) = 1.
        _length("laser len", Float) = 0.
    }
        SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
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

            float4 _color;
            float _time;
            float _pixelSize;
            float4 _MainTex_ST;
            float _length;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * float2(_length, 1.);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float rand(float2 co)
            {
                return frac(sin(dot(co, float2(12.9898, 78.233))) * 43758.5453);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                i.uv = floor(i.uv / _pixelSize) * _pixelSize;
                float opacity = (_color.w * rand(float2(_time, i.uv.x)));
                return float4(_color.xyz, opacity * opacity * opacity);
            }
            ENDCG
        }
    }
}