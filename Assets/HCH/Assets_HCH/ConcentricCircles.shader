// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ConcentricCircles" {
    Properties {
        _OrigineX ("PosX Origine", Range(0,1)) = 0.5
        _OrigineY ("PosY Origine", Range(0,1)) = 0.5
        _Speed ("Speed", Range(-100,100)) = 60.0
        _CircleNbr ("Circle quantity", Range(10,1000)) = 60.0
    }
 
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma target 3.0
 
            float _OrigineX;
            float _OrigineY;
            float _Speed;
            float _CircleNbr;
 
            struct vertexInput {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
            };
 
            struct fragmentInput{
                float4 position : SV_POSITION;
                float4 texcoord0 : TEXCOORD0;
            };
 
            fragmentInput vert(vertexInput i) {
                fragmentInput o;
                o.position = UnityObjectToClipPos(i.vertex);
                o.texcoord0 = i.texcoord0;
                return o;
            }
 
            fixed4 frag(fragmentInput i) : SV_Target {
                float time = _Time.x * _Speed;  
                float xdist = _OrigineX - i.texcoord0.x;
                float ydist = _OrigineY - i.texcoord0.y;

                float distanceToCenter = (xdist * xdist + ydist * ydist) * _CircleNbr;
                float value = sin(distanceToCenter + time);

                // ���� ���� 0�� 1 ���̷� ����
                value = (value + 1.0) * 0.5; // -1 ~ 1 ������ 0 ~ 1�� ��ȯ

                // ���� ���� ����
                return fixed4(value, value, value, 1.0); // ȸ������ ����
            }
            ENDCG
        }
    }
}