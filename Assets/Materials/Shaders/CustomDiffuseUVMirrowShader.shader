Shader "Custom/DiffuseUVMirrowShader" {
 Properties {
     _Color ("Main Color", Color) = (1,1,1,1)
     _MainTex ("Base (RGB)", 2D) = "white" {}
 }
 SubShader {
     Tags { "RenderType"="Opaque" }
     LOD 200
 
 CGPROGRAM
 #pragma surface surf Lambert
 
 sampler2D _MainTex;
 float4 _Color;
 
 struct Input {
     float2 uv_MainTex;
 };
 
 void surf (Input IN, inout SurfaceOutput o) {
     float2 t = frac(IN.uv_MainTex*0.5)*2.0;
     float2 length = {1.0,1.0};
     float2 mirrorTexCoords = length-abs(t-length);
     half4 c = tex2D(_MainTex,  mirrorTexCoords) * _Color;
     o.Albedo = c.rgb;
     o.Alpha = c.a;
 }
 ENDCG
 }
 
 Fallback "VertexLit"
 }