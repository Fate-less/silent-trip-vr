Shader "Toon/Lit WorldPos Effects Sphere" {
    Properties {     
        _MainTex ("Primary (RGB)", 2D) = "white" {}      
        _SecondTex ("Secondary (RGB)", 2D) = "white" {}
        [Header(Noise)]       
        _NoiseTex("Noise", 2D) = "white"{} 
        _NScale ("Noise Scale", Range(0, 10)) = 1 
        _NoiseCutoff("Noise Radius Cutoff", Range(0.01, 1)) =0.01
        _NoiseStrength("Noise Strength", Range(0, 1)) = 0 
        [Header(Line)]   
        _LineWidth("Line Width", Range(0, 2)) = 0 
        [HDR]_LineColor("Line Color", Color) = (1,1,1,1)
        
        [KeywordEnum(SwapTextures, Appear, Disappear)]_STYLE("Style", Float) = 0
    }
    
    SubShader{
        Tags { "RenderType" = "Transparent" }
        LOD 200            
        
        CGPROGRAM
        
        #pragma surface surf ToonRamp addshadow
        #pragma shader_feature_local _STYLE_SWAPTEXTURES _STYLE_APPEAR _STYLE_DISAPPEAR
        
        
        // custom lighting function that uses a texture ramp based
        // on angle between light direction and normal
        #pragma lighting ToonRamp exclude_path:prepass
        
        
        inline half4 LightingToonRamp (SurfaceOutput s, half3 lightDir, half atten)
        {
            #ifndef USING_DIRECTIONAL_LIGHT
                lightDir = normalize(lightDir);
            #endif
            
            float d = dot(s.Normal, lightDir) ;
            float dChange = fwidth(d);
            float3 lightIntensity = smoothstep(0 , 0 + 0.1, d);
            
            half4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * lightIntensity * (atten * 2);
            c.a = 0;
            return c;
        }

        float3 _Position; // from script
        float _Radius;// from script

        sampler2D _MainTex, _SecondTex;
        sampler2D _NoiseTex;
        float _NoiseCutoff, _NScale;
        float _LineWidth;
        float4 _LineColor;
        float _NoiseStrength;
        
        struct Input {
            float2 uv_MainTex : TEXCOORD0;
            float3 worldPos;// built in value to use the world space position
            float3 worldNormal; // built in value for world normal
            
        };
        
        void surf (Input IN, inout SurfaceOutput o) {
            // sphere
            float3 dis =  distance(_Position.xyz, IN.worldPos);
            float sphereR = 1- saturate(dis / _Radius).r;
            
            // triplanar noise
            float3 blendNormal = saturate(pow(IN.worldNormal * 1.4,4));
            half4 nSide1 = tex2D(_NoiseTex, (IN.worldPos.xy + _Time.x) * _NScale); 
            half4 nSide2 = tex2D(_NoiseTex, (IN.worldPos.xz + _Time.x) * _NScale);
            half4 nTop = tex2D(_NoiseTex, (IN.worldPos.yz + _Time.x) * _NScale);

            float3 noisetexture = nSide1;
            noisetexture = lerp(noisetexture, nTop, blendNormal.x);
            noisetexture = lerp(noisetexture, nSide2, blendNormal.y);
            
            float3 sphereNoise = lerp(noisetexture.r * sphereR , sphereR, _NoiseStrength);

            // cutoff
            float radiusCutoff = step(_NoiseCutoff,sphereNoise);

            // glowing line
            float Line = step(sphereNoise - _LineWidth, _NoiseCutoff) * radiusCutoff ; // line between two textures
            float3 colouredLine= Line * _LineColor; // color the line
            
            // textures
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            half4 c2 = tex2D(_SecondTex, IN.uv_MainTex);
            float3 combinedTex = lerp(c, c2, radiusCutoff);
            
            // final
            float3 resultTex = combinedTex + colouredLine;
            o.Albedo = resultTex;

            o.Emission = colouredLine;

            // alpha clipping
            #if defined(_STYLE_APPEAR)
                clip(radiusCutoff - 0.01);
            #elif defined(_STYLE_DISAPPEAR)
                clip(1-(radiusCutoff - Line) - 0.01);               
            #endif
            o.Alpha = c.a;
            
        }
        ENDCG
        
    }
    
    Fallback "Diffuse"
}