// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
// ref --> https://wiki.unity3d.com/index.php/Silhouette-Outlined_Diffuse
// ref 2 --> https://www.youtube.com/watch?v=YCi4BnnQV2s

/*
Shader "Custom/See Through"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _Outline ("Outline width", Range (0.0, 1)) = .005
    }

    CGINCLUDE
    #include "UnityCG.cginc"

    struct appdata
    {
        float4 vertex : POSITION;
        float3 normal : NORMAL;
    };

    struct v2f
    {
        float4 pos : POSITION;
        float4 color : COLOR;
    };

    uniform float _Outline;
    uniform float4 _OutlineColor;

    v2f vert(appdata v)
    {
        // just make a copy of incoming vertex data but scaled according to normal direction
        v2f o;
        o.pos = UnityObjectToClipPos(v.vertex);

        float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
        float2 offset = TransformViewToProjection(norm.xy);

        o.pos.xy += offset * o.pos.z * _Outline;
        o.color = _OutlineColor;
        return o;
    }
    ENDCG

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
        }

        Pass
        {
            Name "BASE"
            Cull Back
            Blend Zero One

            // uncomment this to hide inner details:
            //Offset -8, -8

            SetTexture [_OutlineColor] {
            ConstantColor (0,0,0,0)
            Combine constant
            }
        }

        // note that a vertex shader is specified here but its using the one above
        Pass
        {
            Name "OUTLINE"
            Tags
            {
                "LightMode" = "Always"
            }
            Cull Front

            // you can choose what kind of blending mode you want for the outline
            Blend SrcAlpha OneMinusSrcAlpha // Normal
            //Blend One One // Additive
            //Blend One OneMinusDstColor // Soft Additive
            //Blend DstColor Zero // Multiplicative
            //Blend DstColor SrcColor // 2x Multiplicative

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            half4 frag(v2f i) :COLOR
            {
                return i.color;
            }
            ENDCG
        }


    }

    Fallback "Diffuse"
}
*/

Shader "Custom/See Through"
{
    Properties
    {
        _MainTex ("Albedo Texture", 2D) = "white" {}
        _TintColor("Tint Color", Color) = (1,1,1,1)
        _Transparency("Transparency", Range(0.0,0.5)) = 0.25
        _CutoutThresh("Cutout Threshold", Range(0.0,1.0)) = 0.2
        _Distance("Distance", Float) = 1
        _Amplitude("Amplitude", Float) = 1
        _Speed ("Speed", Float) = 1
        _Amount("Amount", Range(0.0,1.0)) = 1
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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
            float4 _MainTex_ST;
            float4 _TintColor;
            float _Transparency;
            float _CutoutThresh;
            float _Distance;
            float _Amplitude;
            float _Speed;
            float _Amount;

            v2f vert (appdata v)
            {
                v2f o;
                // v.vertex.x += sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * _Amount;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) + _TintColor;
                col.a = _Transparency;
                clip(col.r - _CutoutThresh);
                return col;
            }
            ENDCG
        }
    }
}