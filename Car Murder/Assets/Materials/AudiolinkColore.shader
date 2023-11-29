// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|diff-9609-OUT,spec-358-OUT,gloss-1813-OUT,emission-8598-OUT;n:type:ShaderForge.SFN_Slider,id:358,x:32070,y:32846,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1813,x:32070,y:32934,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Metallic_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8,max:1;n:type:ShaderForge.SFN_AudioLink,id:7117,x:31533,y:32368,varname:node_7117,prsc:2|UVIN-8821-OUT;n:type:ShaderForge.SFN_TexCoord,id:4755,x:30814,y:32139,varname:node_4755,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Append,id:8821,x:31357,y:32368,varname:node_8821,prsc:2|A-8018-OUT,B-3522-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3522,x:31112,y:32443,ptovrint:False,ptlb:V,ptin:_V,varname:node_3522,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Time,id:4295,x:30814,y:32012,varname:node_4295,prsc:2;n:type:ShaderForge.SFN_Add,id:8018,x:31112,y:32289,varname:node_8018,prsc:2|A-4295-T,B-4755-U;n:type:ShaderForge.SFN_Add,id:8598,x:32019,y:32507,varname:node_8598,prsc:2|A-9609-OUT,B-5704-OUT;n:type:ShaderForge.SFN_Multiply,id:5704,x:31748,y:32336,varname:node_5704,prsc:2|A-9609-OUT,B-7117-RGB,C-8546-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8546,x:31500,y:32641,ptovrint:False,ptlb:GlowStrength,ptin:_GlowStrength,varname:node_8546,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Hue,id:4194,x:31568,y:31943,varname:node_4194,prsc:2|IN-324-OUT;n:type:ShaderForge.SFN_Multiply,id:9609,x:31859,y:31873,varname:node_9609,prsc:2|A-4650-RGB,B-4194-OUT;n:type:ShaderForge.SFN_Color,id:4650,x:31804,y:31584,ptovrint:False,ptlb:grey,ptin:_grey,varname:node_4650,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_AudioLink,id:6180,x:31322,y:31679,varname:node_6180,prsc:2|UVIN-4237-OUT;n:type:ShaderForge.SFN_TexCoord,id:9490,x:30412,y:31542,varname:node_9490,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:6473,x:30713,y:31445,varname:node_6473,prsc:2|A-9490-U,B-5286-OUT;n:type:ShaderForge.SFN_Multiply,id:1571,x:30713,y:31593,varname:node_1571,prsc:2|A-9490-V,B-2768-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5286,x:30432,y:31437,ptovrint:False,ptlb:Mult1,ptin:_Mult1,varname:node_5286,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:2768,x:30412,y:31723,ptovrint:False,ptlb:Mult2,ptin:_Mult2,varname:node_2768,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:1759,x:30962,y:31428,varname:node_1759,prsc:2|A-9758-OUT,B-6473-OUT;n:type:ShaderForge.SFN_Add,id:1443,x:30962,y:31607,varname:node_1443,prsc:2|A-1571-OUT,B-2862-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2862,x:30713,y:31747,ptovrint:False,ptlb:Add2,ptin:_Add2,varname:node_2862,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:9758,x:30713,y:31376,ptovrint:False,ptlb:Add1,ptin:_Add1,varname:node_9758,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:4237,x:31153,y:31585,varname:node_4237,prsc:2|A-1759-OUT,B-1443-OUT;n:type:ShaderForge.SFN_Add,id:9966,x:31464,y:31449,varname:node_9966,prsc:2|A-1880-OUT,B-6180-R;n:type:ShaderForge.SFN_Slider,id:1880,x:31016,y:31323,ptovrint:False,ptlb:HueBias,ptin:_HueBias,varname:node_1880,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:324,x:31633,y:31388,varname:node_324,prsc:2|A-7638-OUT,B-9966-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7638,x:31399,y:31191,ptovrint:False,ptlb:MultBias,ptin:_MultBias,varname:node_7638,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;proporder:358-1813-3522-8546-4650-5286-2768-2862-9758-1880-7638;pass:END;sub:END;*/

Shader "Shader Forge/AudiolinkColore" {
    Properties {
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Gloss ("Gloss", Range(0, 1)) = 0.8
        _V ("V", Float ) = 0
        _GlowStrength ("GlowStrength", Float ) = 0
        _grey ("grey", Color) = (0.5,0.5,0.5,1)
        _Mult1 ("Mult1", Float ) = 0
        _Mult2 ("Mult2", Float ) = 0
        _Add2 ("Add2", Float ) = 0
        _Add1 ("Add1", Float ) = 0
        _HueBias ("HueBias", Range(0, 1)) = 0
        _MultBias ("MultBias", Float ) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            SamplerState sampler_AudioGraph_Point_Repeat;
            Texture2D<float4> _AudioTexture;
            uniform float4 _AudioTexture_TexelSize;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Metallic)
                UNITY_DEFINE_INSTANCED_PROP( float, _Gloss)
                UNITY_DEFINE_INSTANCED_PROP( float, _V)
                UNITY_DEFINE_INSTANCED_PROP( float, _GlowStrength)
                UNITY_DEFINE_INSTANCED_PROP( float4, _grey)
                UNITY_DEFINE_INSTANCED_PROP( float, _Mult1)
                UNITY_DEFINE_INSTANCED_PROP( float, _Mult2)
                UNITY_DEFINE_INSTANCED_PROP( float, _Add2)
                UNITY_DEFINE_INSTANCED_PROP( float, _Add1)
                UNITY_DEFINE_INSTANCED_PROP( float, _HueBias)
                UNITY_DEFINE_INSTANCED_PROP( float, _MultBias)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float _Gloss_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Gloss );
                float gloss = _Gloss_var;
                float perceptualRoughness = 1.0 - _Gloss_var;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float _Metallic_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Metallic );
                float3 specularColor = _Metallic_var;
                float specularMonochrome;
                float4 _grey_var = UNITY_ACCESS_INSTANCED_PROP( Props, _grey );
                float _MultBias_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MultBias );
                float _HueBias_var = UNITY_ACCESS_INSTANCED_PROP( Props, _HueBias );
                float _Add1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Add1 );
                float _Mult1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Mult1 );
                float _Mult2_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Mult2 );
                float _Add2_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Add2 );
                float3 node_6180 = (_AudioTexture.Sample(sampler_AudioGraph_Point_Repeat, float2(float2((_Add1_var+(i.uv0.r*_Mult1_var)),((i.uv0.g*_Mult2_var)+_Add2_var)))));
                float3 node_9609 = (_grey_var.rgb*saturate(3.0*abs(1.0-2.0*frac((_MultBias_var*(_HueBias_var+node_6180.r))+float3(0.0,-1.0/3.0,1.0/3.0)))-1));
                float3 diffuseColor = node_9609; // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 node_4295 = _Time;
                float _V_var = UNITY_ACCESS_INSTANCED_PROP( Props, _V );
                float3 node_7117 = (_AudioTexture.Sample(sampler_AudioGraph_Point_Repeat, float2(float2((node_4295.g+i.uv0.r),_V_var))));
                float _GlowStrength_var = UNITY_ACCESS_INSTANCED_PROP( Props, _GlowStrength );
                float3 emissive = (node_9609+(node_9609*node_7117.rgb*_GlowStrength_var));
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            SamplerState sampler_AudioGraph_Point_Repeat;
            Texture2D<float4> _AudioTexture;
            uniform float4 _AudioTexture_TexelSize;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Metallic)
                UNITY_DEFINE_INSTANCED_PROP( float, _Gloss)
                UNITY_DEFINE_INSTANCED_PROP( float, _V)
                UNITY_DEFINE_INSTANCED_PROP( float, _GlowStrength)
                UNITY_DEFINE_INSTANCED_PROP( float4, _grey)
                UNITY_DEFINE_INSTANCED_PROP( float, _Mult1)
                UNITY_DEFINE_INSTANCED_PROP( float, _Mult2)
                UNITY_DEFINE_INSTANCED_PROP( float, _Add2)
                UNITY_DEFINE_INSTANCED_PROP( float, _Add1)
                UNITY_DEFINE_INSTANCED_PROP( float, _HueBias)
                UNITY_DEFINE_INSTANCED_PROP( float, _MultBias)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float _Gloss_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Gloss );
                float gloss = _Gloss_var;
                float perceptualRoughness = 1.0 - _Gloss_var;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float _Metallic_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Metallic );
                float3 specularColor = _Metallic_var;
                float specularMonochrome;
                float4 _grey_var = UNITY_ACCESS_INSTANCED_PROP( Props, _grey );
                float _MultBias_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MultBias );
                float _HueBias_var = UNITY_ACCESS_INSTANCED_PROP( Props, _HueBias );
                float _Add1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Add1 );
                float _Mult1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Mult1 );
                float _Mult2_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Mult2 );
                float _Add2_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Add2 );
                float3 node_6180 = (_AudioTexture.Sample(sampler_AudioGraph_Point_Repeat, float2(float2((_Add1_var+(i.uv0.r*_Mult1_var)),((i.uv0.g*_Mult2_var)+_Add2_var)))));
                float3 node_9609 = (_grey_var.rgb*saturate(3.0*abs(1.0-2.0*frac((_MultBias_var*(_HueBias_var+node_6180.r))+float3(0.0,-1.0/3.0,1.0/3.0)))-1));
                float3 diffuseColor = node_9609; // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            SamplerState sampler_AudioGraph_Point_Repeat;
            Texture2D<float4> _AudioTexture;
            uniform float4 _AudioTexture_TexelSize;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Metallic)
                UNITY_DEFINE_INSTANCED_PROP( float, _Gloss)
                UNITY_DEFINE_INSTANCED_PROP( float, _V)
                UNITY_DEFINE_INSTANCED_PROP( float, _GlowStrength)
                UNITY_DEFINE_INSTANCED_PROP( float4, _grey)
                UNITY_DEFINE_INSTANCED_PROP( float, _Mult1)
                UNITY_DEFINE_INSTANCED_PROP( float, _Mult2)
                UNITY_DEFINE_INSTANCED_PROP( float, _Add2)
                UNITY_DEFINE_INSTANCED_PROP( float, _Add1)
                UNITY_DEFINE_INSTANCED_PROP( float, _HueBias)
                UNITY_DEFINE_INSTANCED_PROP( float, _MultBias)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UNITY_SETUP_INSTANCE_ID( i );
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _grey_var = UNITY_ACCESS_INSTANCED_PROP( Props, _grey );
                float _MultBias_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MultBias );
                float _HueBias_var = UNITY_ACCESS_INSTANCED_PROP( Props, _HueBias );
                float _Add1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Add1 );
                float _Mult1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Mult1 );
                float _Mult2_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Mult2 );
                float _Add2_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Add2 );
                float3 node_6180 = (_AudioTexture.Sample(sampler_AudioGraph_Point_Repeat, float2(float2((_Add1_var+(i.uv0.r*_Mult1_var)),((i.uv0.g*_Mult2_var)+_Add2_var)))));
                float3 node_9609 = (_grey_var.rgb*saturate(3.0*abs(1.0-2.0*frac((_MultBias_var*(_HueBias_var+node_6180.r))+float3(0.0,-1.0/3.0,1.0/3.0)))-1));
                float4 node_4295 = _Time;
                float _V_var = UNITY_ACCESS_INSTANCED_PROP( Props, _V );
                float3 node_7117 = (_AudioTexture.Sample(sampler_AudioGraph_Point_Repeat, float2(float2((node_4295.g+i.uv0.r),_V_var))));
                float _GlowStrength_var = UNITY_ACCESS_INSTANCED_PROP( Props, _GlowStrength );
                o.Emission = (node_9609+(node_9609*node_7117.rgb*_GlowStrength_var));
                
                float3 diffColor = node_9609;
                float specularMonochrome;
                float3 specColor;
                float _Metallic_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Metallic );
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, _Metallic_var, specColor, specularMonochrome );
                float _Gloss_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Gloss );
                float roughness = 1.0 - _Gloss_var;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
