Shader "Custom/IrisTransitionShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("Radius", Range(0,2)) = 0  // Start at 0 to keep it invisible initially
        _EdgeSoftness ("Edge Softness", Range(0,1)) = 0.2
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
    Blend SrcAlpha OneMinusSrcAlpha
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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Radius;
            float _EdgeSoftness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

           fixed4 frag (v2f i) : SV_Target
{
    // Calculate distance from center
    float2 uv = i.uv - 0.5;
    float dist = 1.0 - length(uv) / 0.5; // Invert distance for edge-to-center effect

    // Calculate mask with softness
    float mask = smoothstep(_Radius, _Radius - _EdgeSoftness, dist);
    
    // Set color to black with mask transparency for blending
    fixed4 col = fixed4(0, 0, 0, mask); // Black color with variable alpha

    return col;
}
            ENDCG
        }
    }
}
