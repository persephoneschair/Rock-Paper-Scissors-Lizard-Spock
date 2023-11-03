Shader"Custom/Background"
{
    Properties
    {
        _MainTex ("iChannel0", 2D) = "white" {}
        _SecondTex ("iChannel1", 2D) = "white" {}
        _ThirdTex ("iChannel2", 2D) = "white" {}
        _FourthTex ("iChannel3", 2D) = "white" {}
        _Mouse ("Mouse", Vector) = (0.5, 0.5, 0.5, 0.5)
        [ToggleUI] _GammaCorrect ("Gamma Correction", Float) = 1
        _Resolution ("Resolution (Change if AA is bad)", Range(1, 1024)) = 1
    }
    SubShader
    {
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

            // Built-in properties
sampler2D _MainTex;
float4 _MainTex_TexelSize;
sampler2D _SecondTex;
float4 _SecondTex_TexelSize;
sampler2D _ThirdTex;
float4 _ThirdTex_TexelSize;
sampler2D _FourthTex;
float4 _FourthTex_TexelSize;
float4 _Mouse;
float _GammaCorrect;
float _Resolution;

            // GLSL Compatability macros
#define glsl_mod(x,y) (((x)-(y)*floor((x)/(y))))
#define texelFetch(ch, uv, lod) tex2Dlod(ch, float4((uv).xy * ch##_TexelSize.xy + ch##_TexelSize.xy * 0.5, 0, lod))
#define textureLod(ch, uv, lod) tex2Dlod(ch, float4(uv, 0, lod))
#define iResolution float3(_Resolution, _Resolution, _Resolution)
#define iFrame (floor(_Time.y / 60))
#define iChannelTime float4(_Time.y, _Time.y, _Time.y, _Time.y)
#define iDate float4(2020, 6, 18, 30)
#define iSampleRate (44100)
#define iChannelResolution float4x4(                      \
                _MainTex_TexelSize.z,   _MainTex_TexelSize.w,   0, 0, \
                _SecondTex_TexelSize.z, _SecondTex_TexelSize.w, 0, 0, \
                _ThirdTex_TexelSize.z,  _ThirdTex_TexelSize.w,  0, 0, \
                _FourthTex_TexelSize.z, _FourthTex_TexelSize.w, 0, 0)

            // Global access to uv data
static v2f vertex_output;

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}

float3 palette(float t)
{
    float3 a = float3(0.5, 0.5, 0.5);
    float3 b = float3(0.5, 0.5, 0.5);
    float3 c = float3(1., 1., 1.);
    float3 d = float3(0.263, 0.416, 0.557);
    return a + b * cos(6.28318 * (c * t + d));
}

float4 frag(v2f __vertex_output) : SV_Target
{
    vertex_output = __vertex_output;
    float4 fragColor = 0;
    float2 fragCoord = vertex_output.uv * _Resolution;
    float2 uv = (fragCoord * 2. - iResolution.xy) / iResolution.y;
    float2 uv0 = uv;
    float3 finalColor = ((float3) 0.);
    for (float i = 0.; i < 4.; i++)
    {
        uv = frac(uv * 1.5) - 0.5;
        float d = length(uv) * exp(-length(uv0));
        float3 col = palette(length(uv0) + i * 0.4 + _Time.y * 0.4);
        d = sin(d * 8. + _Time.y) / 8.;
        d = abs(d);
        d = pow(0.01 / d, 1.2);
        finalColor += col * d;
    }
    fragColor = float4(finalColor, 1.);
    if (_GammaCorrect)
        fragColor.rgb = pow(fragColor.rgb, 2.2);
    return fragColor;
}
            ENDCG
        }
    }
}
