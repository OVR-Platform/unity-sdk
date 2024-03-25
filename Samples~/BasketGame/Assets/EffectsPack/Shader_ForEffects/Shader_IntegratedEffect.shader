Shader "GAPH Custom Shader/Shader_IntegradedEffect"
{
		Properties {
			[Header(Main)]
				[Space]
					[HDR]_TintColor("Color",Color) = (1,1,1,1)
					[Toggle(IS_USE_SECOND_COLOR)]_SecondColor("Is use second color",int) = 0
					[HDR]_TintColor2("Color2",Color) = (1,1,1,1)
					_MainTex ("Main Tex", 2D) = "white" {}
					_ColorFactor("Color Factor", float) = 1
					[Toggle(IS_TEXTURE_ANIMATE)]_TextureAnimate("Is Texture Animate",int) = 0
						_TextureAnimateSpeed("Texture Animate Speed",float) = 1.0
						_TextureAnimateStyle("Texture Animate Style", Range(0,2)) = 1
					[Toggle(IS_TEXTURE_ANIMATE_ADVANCED)]_TextureAnimateAdvanced("Is Texture Animate Advanced",int) = 0 //Need to check 'Is Texture Animate'
						_MaxIndex("Texture Mix Count",int) = 2
					[Toggle(IS_TEXTURE_BLEND)]_TextureBlend("Is Texture Blend",int) = 0
					[Toggle(IS_UNITY_PARTICLE_INSTANCING_ENABLED)]_ParticleInstancing("Is Unity Paticle Instancing Enable",int) = 0
					[Toggle(IS_ALL_TEXTURE_STRAIGHT_MOVE)]_MixedMove("Is All Texture Straight Move",int) = 0
						_TexPosMove("xPosMove", Range(-1,1)) = 0
			[Header(Soft Particle)]
				[Space]
					[Toggle(IS_SOFT_PARTICLES)]_SoftParticle("Is Soft Particles",int) = 1
						_InvFade("Soft Particle Factor",float) = 1
			[Header(Normal)]
				[Space]
					[Toggle(IS_NORMAL_DISTORTION)]_NormalDistortion("Is Normal Distortion",int) = 0
						_NormalTex("Normal Tex",2D) = "white"{}
						_NormalDistortionFactor("Normal Distortion Factor",float) = 1.0
					[Toggle(IS_NORMAL_ANIMATE)]_NormalAnimate("Is Normal Animate",int) = 0
						_NormalAnimateSpeed("Normal Animate Speed",float) = 1.0
					[Toggle(IS_TEXTURE_NOISE)]_TextureNoise("Is Texture Noise", int)= 0
						_NoiseNormal("Noise Normal",2D) = "white"{}
						_NoiseNormalFactor("NoiseNormalFactor",float) = 1.0
			[Header(Mask Fade)]
				[Space]
					[Toggle(IS_MASK_FADE)]_MaskFade("Is Mask Fade",int) = 0
					[Toggle(IS_USE_TEXANIMATION)]_UseTexAnimation("Is UseTexAnimation",int) = 0
						_FixedMaskTex("Fixed Mask Tex",2D) = "white"{}
						_MaskTex("Mask Tex",2D) = "white"{}
						_MaskOffsetFactor("Mask Offset Factor",float) = 1.0
						_MaskDistortion("Mask Distortion Tex",2D) = "white"{}
						_MaskAnimatedSpeed("_MaskAnimatedSpeed",float) = 1.0
						_MaskCutOut("Mask CutOut",Range(0,1)) = 1
			[Header(Render)]
				[Space]
					[Toggle]_ZWrite("ZWrite On/Off", int) = 0
					[Enum(Culling Off,0, Culling Front, 1, Culling Back, 2)]_Culling("Culling",float) = 2
					[Enum(UnityEngine.Rendering.BlendMode)]_BlendSrc("BlendSrc", float) = 1
					[Enum(UnityEngine.Rendering.BlendMode)]_BlendDst("BlendDst", float) = 1
					_ZTest2("_ZTest2", int) = 2
			[Header(VertexAnimation)]
				[Space]
					[Toggle(IS_VERTEXANIMATION)]_VertexAnimation("Is Vertex Animation", int) = 0
						_NoiseTex("Vertex Animation Noise Map",2D) = "black"{}
						_NoiseValue("Noise Value", Vector) = (1,1,1,0)
						_NoiseScale("Noise Scale", float) = 1
			[Header(RimLight)]
				[Space]
					[Toggle(IS_RIMLIGHT)]_RimLight("Is Rim Light",int) = 0
						[HDR]_RimColor("RimColor",Color) = (1,1,1,1)
						_RimScale("Rim Light Power",float) = 1
						_RimStrength("Rim Light Strength",float) = 1
			[Header(Impact)]
				[Space]
					[Toggle(IS_IMPACT)]_Impact("Is Impact",int) = 0
						_ImpactSize("Impact Size",float) = 0.5
						_ImpactFactor("Impact Factor",float) = 1
			[Header(Texcoord)]
					[Space]
						[Toggle(IS_TEXCOORD_MOVE)]_TexcoordMove("Is Texcoord Move",int) = 0
							_xTexcoordMove("xTexcoordMove", Range(-1,1)) = 0
							_yTexcoordMove("yTexcoordMove", Range(-1,1)) = 0
						_TexcoordMoveStrength("TexcoordMoveStrength",float) = 0
						[Toggle(IS_TEXCOORD_MOVE_USING_CUSTOM)]_TexcoordMoveUsingCustom("Is Texcoord Move Using Custom",int) = 0
			[Header(LinePass)]
				[Space]
					[Toggle(IS_LINEPASS)]_LinePass("Is LinePass", int) = 0
						_TexLength("TexLength",Range(0,1)) = 1.0
					_LinePassTex("LinePassTex", 2D) = "white" {}


		}
			Category{
					Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
					Blend[_BlendSrc][_BlendDst]
					Cull [_Culling]
					ZWrite[_ZWrite]
					Lighting Off
					ZTest[_ZTest2]

					SubShader {
						Pass{

							CGPROGRAM
							#pragma vertex vert
							#pragma fragment frag
							#pragma multi_compile_particles
							#pragma multi_compile_fog
							#pragma multi_compile_instancing

							#pragma shader_feature IS_SOFT_PARTICLES
							#pragma shader_feature IS_USE_SECOND_COLOR
							#pragma shader_feature IS_NORMAL_DISTORTION
							#pragma shader_feature IS_TEXCOORD_MOVE_USING_CUSTOM
							#pragma shader_feature IS_TEXTURE_NOISE
							#pragma shader_feature IS_MASK_FADE
							#pragma shader_feature IS_TEXTURE_BLEND
							#pragma shader_feature IS_TEXTURE_ANIMATE
							#pragma shader_feature IS_TEXTURE_ANIMATE_ADVANCED
							#pragma shader_feature IS_ALL_TEXTURE_STRAIGHT_MOVE
							#pragma shader_feature IS_TEXCOORD_MOVE
							#pragma shader_feature IS_NORMAL_ANIMATE
							#pragma shader_feature IS_VERTEXANIMATION
							#pragma shader_feature IS_RIMLIGHT
							#pragma shader_feature IS_IMPACT
							#pragma shader_feature UNITY_PARTICLE_INSTANCING_ENABLED
							#pragma shader_feature IS_LINEPASS
							#pragma shader_feature IS_USE_TEXANIMATION
							
							#include "UnityCG.cginc"	
							#include "UnityStandardParticleInstancing.cginc"

							sampler2D _MainTex;
							half4 _MainTex_ST;
							sampler2D _CameraDepthTexture;

							#ifdef IS_TEXTURE_ANIMATE_ADVANCED
										int _MaxIndex;
							#endif

							#ifdef IS_NORMAL_DISTORTION
										sampler2D _NormalTex;
										half4 _NormalTex_ST;
										half _NormalDistortionFactor;
							#endif

							#ifdef IS_TEXTURE_NOISE
										sampler2D _NoiseNormal;
										half4 _NoiseNormal_ST;
										half _NoiseNormalFactor;
							#endif

							#ifdef IS_MASK_FADE
										sampler2D _FixedMaskTex;
										half4 _FixedMaskTex_ST;
										sampler2D _MaskTex;
										half4 _MaskTex_ST;
										half _MaskOffsetFactor;
										sampler2D _MaskDistortion;
										half4 _MaskDistortion_ST;
							#endif
							half _MaskCutOut;

							#ifdef IS_ALL_TEXTURE_STRAIGHT_MOVE
										half _TexPosMove;
							#endif

							#ifdef IS_VERTEXANIMATION
										sampler2D _NoiseTex;
										half4 _NoiseTex_ST;
										half _NoiseScale;
							#endif

							#ifdef IS_RIMLIGHT
										half _RimScale;
										half _RimStrength;
							#endif

							#ifdef IS_IMPACT
										half _ImpactSize;
										half _ImpactFactor;
										int _PointSize;
										fixed4 _Points[30];
							#endif

							#ifdef IS_TEXCOORD_MOVE
										half _xTexcoordMove;
										half _yTexcoordMove;
										half _TexcoordMoveStrength;
							#endif
							
							#ifdef IS_LINEPASS
										sampler2D _LinePassTex;
										half4 _LinePassTex_ST;
										half _TexLength;
										half _TexLength2;
							#endif
							

							UNITY_INSTANCING_BUFFER_START(data)
								UNITY_DEFINE_INSTANCED_PROP(half4, _TintColor)
							#define _TintColor_arr data
							#ifdef IS_USE_SECOND_COLOR
									UNITY_DEFINE_INSTANCED_PROP(half4, _TintColor2)
								#define _TintColor2_arr data
							#endif
									UNITY_DEFINE_INSTANCED_PROP(half, _ColorFactor)
								#define _ColorFactor_arr data
							#ifdef IS_RIMLIGHT
									UNITY_DEFINE_INSTANCED_PROP(half4,_RimColor)
								#define _RimColor_arr data
							#endif
							#ifdef IS_VERTEXANIMATION
									UNITY_DEFINE_INSTANCED_PROP(half4,_NoiseValue)
								#define _NoiseValue_arr data
							#endif
							#ifdef IS_TEXTURE_ANIMATE
									UNITY_DEFINE_INSTANCED_PROP(half, _TextureAnimateSpeed)
								#define _TextureAnimateSpeed_arr data
									UNITY_DEFINE_INSTANCED_PROP(int, _TextureAnimateStyle)
								#define _TextureAnimateStyle_arr data
							#endif
							#ifdef IS_NORMAL_ANIMATE
									UNITY_DEFINE_INSTANCED_PROP(half, _NormalAnimateSpeed)
								#define _NormalAnimateSpeed_arr data
							#endif
							#ifdef IS_MASK_FADE
									UNITY_DEFINE_INSTANCED_PROP(half,_MaskAnimatedSpeed)
								#define _MaskAnimatedSpeed_arr data
							#endif
							UNITY_INSTANCING_BUFFER_END(data)

							half _InvFade;
							
							struct appdata_t {
								float4 vertex : POSITION;
								float3 normal : NORMAL;
								half4 color : COLOR;
								#ifdef IS_TEXTURE_BLEND
									half4 texcoord : TEXCOORD0;
									half texcoordBlend : TEXCOORD1;
								#else
									half4 texcoord : TEXCOORD0;
									#ifdef IS_TEXCOORD_MOVE_USING_CUSTOM
									half2 texcoord2 : TEXCOORD1;
									#endif
								#endif
								UNITY_VERTEX_INPUT_INSTANCE_ID
							};

							struct v2f {
								float4 vertex : SV_POSITION;
								half4 color : COLOR;
								half2 maintex : TEXCOORD0;
								#ifdef IS_TEXTURE_BLEND
									half3 maintexBlend : TEXCOORD1;
								#endif
								#ifdef IS_NORMAL_DISTORTION
									half2 normaltex : TEXCOORD2;
								#endif
								#ifdef IS_MASK_FADE
									half2 fixedmasktex : TEXCOORD3;
									half2 masktex : TEXCOORD4;
									half2 masknormaltex: TEXCOORD5;
								#endif
								#ifdef IS_TEXTURE_NOISE
									half2 noisetex : TEXCOORD6;
								#endif
								#ifdef SOFTPARTICLES_ON
									half4 projPos : TEXCOORD7;
								#endif
								UNITY_FOG_COORDS(8)
								#ifdef IS_RIMLIGHT
									half3 viewDir : TEXCOORD9;
									half3 normal : TEXCOORD10;
								#endif
								#ifdef IS_IMPACT
									float3 worldPos : TEXCOORD11;
								#endif
								#ifdef IS_LINEPASS
									float2 linepasscoord : TEXCOORD12;
								#endif
				
								UNITY_VERTEX_INPUT_INSTANCE_ID
								UNITY_VERTEX_OUTPUT_STEREO
							};

							v2f vert (appdata_t i)
							{
								v2f o;
								UNITY_SETUP_INSTANCE_ID(i);
								UNITY_INITIALIZE_OUTPUT(v2f, o);
								UNITY_TRANSFER_INSTANCE_ID(i, o);
								UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
								
								#ifdef IS_VERTEXANIMATION
								float4 Noise = mul(UNITY_MATRIX_M, i.vertex) * UNITY_ACCESS_INSTANCED_PROP(_NoiseValue_arr, _NoiseValue) * float4(0.1f, 0.1f, 1.5f, 1);
								//Set noiseTex with normal tex using time & scale info. time is for animate vertex
								float4 NoiseTex = tex2Dlod(_NoiseTex, Noise + float4(float3(_Time.x / 2, _Time.y / 2, _Time.z * 2) * _NoiseScale * 10, 0));
								//NoiseTex *= tex2Dlod(_NoiseTex, Noise - float4(float3(_Time.x / 2, _Time.y / 2, _Time.z * 2) * _NoiseScale * 10, 0));
								i.vertex = i.vertex * UNITY_ACCESS_INSTANCED_PROP(_NoiseValue_arr, _NoiseValue).w +
									//Add changed noise info with normal value to original object vertex.
									(saturate(NoiseTex) - 0.5f) * (
										//Additionally trigonometric value to original object vertex.
										sin((i.vertex.x + _Time * UNITY_ACCESS_INSTANCED_PROP(_NoiseValue_arr, _NoiseValue).x)* UNITY_ACCESS_INSTANCED_PROP(_NoiseValue_arr, _NoiseValue).y) +
										cos((i.vertex.y + _Time * UNITY_ACCESS_INSTANCED_PROP(_NoiseValue_arr, _NoiseValue).x)* UNITY_ACCESS_INSTANCED_PROP(_NoiseValue_arr, _NoiseValue).y) +
										sin((i.vertex.z + _Time * UNITY_ACCESS_INSTANCED_PROP(_NoiseValue_arr, _NoiseValue).x)* UNITY_ACCESS_INSTANCED_PROP(_NoiseValue_arr, _NoiseValue).y)
										)* UNITY_ACCESS_INSTANCED_PROP(_NoiseValue_arr, _NoiseValue).z*_NoiseScale * 10;
								#endif

								o.vertex = UnityObjectToClipPos(i.vertex);

								half4 originaltex = i.texcoord;

								#ifdef IS_TEXCOORD_MOVE
										#ifdef IS_TEXCOORD_MOVE_USING_CUSTOM
											i.texcoord.y += i.texcoord2.y;
										#else
											i.texcoord.x += _Time * _xTexcoordMove * _TexcoordMoveStrength;
											i.texcoord.y += _Time * _yTexcoordMove * _TexcoordMoveStrength;
										#endif
								#endif

								#ifdef IS_UNITY_PARTICLE_INSTANCING_ENABLED //GPU Rendering
									#ifdef IS_TEXTURE_BLEND
										vertInstancingUVs(i.texcoord.xy, o.maintex, o.,maintexBlend);
									#else
										vertInstancingUVs(i.texcoord, o.maintex);
										o.maintex = TRANSFORM_TEX(o.texcoord, _MainTex);
									#endif
								#else
									#ifdef IS_TEXTURE_BLEND
										o.maintex = i.texcoord.xy;
										o.maintexBlend.xy = i.texcoord.zw;
										o.maintexBlend.z = i.texcoordBlend;
									#else
										o.maintex = TRANSFORM_TEX(i.texcoord, _MainTex);
									#endif
								#endif

								#ifdef IS_NORMAL_DISTORTION
									o.normaltex = TRANSFORM_TEX(i.texcoord, _NormalTex);
								#endif
								#ifdef IS_MASK_FADE
									half4 masktexcoord;
									#ifdef IS_USE_TEXANIMATION
										masktexcoord = i.texcoord;
									#else
										masktexcoord = originaltex;
									#endif
									o.fixedmasktex = TRANSFORM_TEX(masktexcoord, _FixedMaskTex);
									o.masktex = TRANSFORM_TEX(masktexcoord, _MaskTex);
									o.masknormaltex = TRANSFORM_TEX(masktexcoord, _MaskDistortion);
								#endif
								#ifdef IS_TEXTURE_NOISE
									o.noisetex = TRANSFORM_TEX(i.texcoord, _NoiseNormal);
								#endif

								#ifdef IS_SOFTPARTICLES
									#ifdef SOFTPARTICLES_ON
										o.projPos = ComputeNonStereoScreenPos(o.vertex);
										COMPUTE_EYEDEPTH(o.projPos.z);
									#endif
								#endif

								#ifdef IS_RIMLIGHT
										o.viewDir = WorldSpaceViewDir(i.vertex);
										o.normal = UnityObjectToWorldNormal(i.vertex);
								#endif

								#ifdef IS_IMPACT
										o.worldPos = i.vertex;
								#endif

								#ifdef IS_LINEPASS
										half4 originaltexcoord = i.texcoord;
										float length = i.texcoord.z;
										length = lerp(1.0f, 3.0f, length);
										i.texcoord.x *= length;

										float length2 = i.texcoord.w * _TexLength * 2;
										length2 = lerp(1, 0, length2);

										i.texcoord.x -= length2;

										i.texcoord.x = clamp(i.texcoord.x, 0, 1);
										i.texcoord.y = clamp(i.texcoord.y, 0, 1);

										o.linepasscoord = TRANSFORM_TEX(i.texcoord, _LinePassTex);
										i.texcoord = originaltexcoord;
								#endif

								o.color = i.color;

								UNITY_TRANSFER_FOG(o, o.vertex);
								return o;
							}

							half4 frag(v2f i): SV_Target
							{
								UNITY_SETUP_INSTANCE_ID(i);
								UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
								#ifdef IS_SOFTPARTICLES
									half sceneZ = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos))));
									half partZ = i.projPos.z;
									half fade = saturate(_InvFade * (sceneZ - partZ));
									i.color.a *= fade;
								#endif

								#ifdef IS_TEXTURE_NOISE
									half2 noiseTex = tex2D(_NoiseNormal, i.noisetex);
									half2 offset = (noiseTex * 2 - 1) * _NoiseNormalFactor;
									i.maintex.xy += offset;
								#endif

								#ifdef IS_NORMAL_DISTORTION
									#ifdef IS_NORMAL_ANIMATE
										//Mixed Distort Move
										#ifdef IS_ALL_TEXTURE_STRAIGHT_MOVE
											half2 distort = UnpackNormal(tex2D(_NormalTex, i.normaltex +(float(UNITY_ACCESS_INSTANCED_PROP(_NormalAnimateSpeed_arr, _NormalAnimateSpeed)) * _Time / 10)*_TexPosMove)).rg;
											distort *= UnpackNormal(tex2D(_NormalTex, i.normaltex +((float(UNITY_ACCESS_INSTANCED_PROP(_NormalAnimateSpeed_arr, _NormalAnimateSpeed))*_Time / 10) + float2(0.5f, 0.15f))*_TexPosMove)).rg;
											distort *= UnpackNormal(tex2D(_NormalTex, i.normaltex +((float(UNITY_ACCESS_INSTANCED_PROP(_NormalAnimateSpeed_arr, _NormalAnimateSpeed))*_Time / 10) + float2(0.15f, 0.5f))*_TexPosMove)).rg;
										#else
											half2 distort = UnpackNormal(tex2D(_NormalTex, i.normaltex - (float(UNITY_ACCESS_INSTANCED_PROP(_NormalAnimateSpeed_arr, _NormalAnimateSpeed)) * _Time / 10))).rg;
											distort *= UnpackNormal(tex2D(_NormalTex, i.normaltex + (float(UNITY_ACCESS_INSTANCED_PROP(_NormalAnimateSpeed_arr, _NormalAnimateSpeed))*_Time / 10) - float2(-0.25f, -0.15f))).rg;
										#endif
									#else
										half2 distort = UnpackNormal(tex2D(_NormalTex, i.normaltex)).rg;
									#endif
									#ifdef IS_TEXTURE_BLEND
									i.maintex.xy += distort.xy* _NormalDistortionFactor;
									i.maintexBlend.xy += distort.xy* _NormalDistortionFactor;
									#else
									i.maintex.xy += distort.xy* _NormalDistortionFactor;
									#endif
								#endif

								#ifdef IS_TEXTURE_ANIMATE
									#ifdef IS_ALL_TEXTURE_STRAIGHT_MOVE
										half4 tex = tex2D(_MainTex, i.maintex.xy + (float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed)) * _Time / 10)*_TexPosMove);
										tex *= tex2D(_MainTex, i.maintex.xy + ((float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed)) * _Time / 10) + float2(0.25f, -0.25f))*_TexPosMove);
										half4 tex2 = tex2D(_MainTex, i.maintex.xy + ((float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed)) * _Time / 10) + float2(-0.5f, 0.5f))*_TexPosMove) * 2.5f;
										tex = (tex) / 1.5f;
									#else
										#ifdef IS_TEXTURE_ANIMATE_ADVANCED
											half4 tex = half4(1, 1, 1, 1);
											half reversefactor = -1;

											for (uint j = 1; j < uint(_MaxIndex); j++) {
												half movefactor = ( uint(j) / _MaxIndex);
												half timefactor;

												reversefactor *= -1;
												timefactor = (_Time.x + movefactor) *reversefactor;
												tex *= tex2D(_MainTex, i.maintex + movefactor + (float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed))) * float2(timefactor, 0) );
												tex *= tex2D(_MainTex, i.maintex + movefactor + (float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed))) * float2(0, timefactor));
											}
											tex = saturate(pow(tex, 1.0f/_MaxIndex));
										#else
											half4 tex = half4(0, 0, 0, 0);
											if (UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateStyle_arr, _TextureAnimateStyle) == 0)
											{
												tex = tex2D(_MainTex, i.maintex.xy - (float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed)) * _Time / 10));
												tex *= tex2D(_MainTex, i.maintex.xy - (float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed)) * _Time / 10) + float2(0.25f, -0.25f));
												half4 tex2 = tex2D(_MainTex, i.maintex.xy + (float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed)) * _Time / 10));
												tex2 *= tex2D(_MainTex, i.maintex.xy + (float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed)) * _Time / 10) + float2(0.15f, -0.15f));
												tex = (tex + tex2) / 1.5f;
											}
											else if (UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateStyle_arr, _TextureAnimateStyle) == 1)
											{
												tex = tex2D(_MainTex, i.maintex.xy - (float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed)) * _Time / 10));
												tex *= tex2D(_MainTex, i.maintex.xy - (float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed)) * _Time / 10) + float2(-0.25f, -0.25f));
												half4 tex2 = tex2D(_MainTex, i.maintex.xy + (float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed)) * _Time / 10));
												tex2 *= tex2D(_MainTex, i.maintex.xy + (float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed)) * _Time / 10) + float2(0.15f, -0.15f));
												tex *= tex2 * 3.5f;
											}
											else
											{ 
												tex = tex2D(_MainTex, i.maintex.xy - (float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed)) * _Time / 10) + float2(0.25f, -0.25f));
												half4 tex2 = tex2D(_MainTex, i.maintex.xy + (float(UNITY_ACCESS_INSTANCED_PROP(_TextureAnimateSpeed_arr, _TextureAnimateSpeed)) * _Time / 10) + float2(0.15f, -0.15f));
												tex = (tex * tex2)*1.25f + (tex + tex2)*0.5f;
											}
										#endif
									#endif
								#else
									half4 tex = tex2D(_MainTex, i.maintex);
									#ifdef IS_TEXTURE_BLEND
										half4 tex2 = tex2D(_MainTex, i.maintexBlend.xy);
										tex = lerp(tex, tex2, i.maintexBlend.z);
									#endif
								#endif

								#ifdef IS_MASK_FADE
										half fixed_mask = tex2D(_FixedMaskTex, i.fixedmasktex);
										#ifdef IS_ALL_TEXTURE_STRAIGHT_MOVE
											half2 mask_noise = tex2D(_MaskDistortion, i.masknormaltex.xy + ((float(UNITY_ACCESS_INSTANCED_PROP(_MaskAnimatedSpeed_arr, _MaskAnimatedSpeed)) * _Time / 5) + float2(0.25f, 0.25f))*_TexPosMove);
											mask_noise *= tex2D(_MaskDistortion, i.masknormaltex.xy + ((float(UNITY_ACCESS_INSTANCED_PROP(_MaskAnimatedSpeed_arr, _MaskAnimatedSpeed)) * _Time / 4) - float2(0.25f, 0.25f))*_TexPosMove);
											mask_noise *= tex2D(_MaskDistortion, i.masknormaltex.xy + ((float(UNITY_ACCESS_INSTANCED_PROP(_MaskAnimatedSpeed_arr, _MaskAnimatedSpeed))* _Time / 20) - float2(0.25f, 0.25f))*_TexPosMove);
											half2 mask_offset = mask_noise * _MaskOffsetFactor;
										#else
											half2 mask_noise = tex2D(_MaskDistortion, i.masknormaltex.xy - (float(UNITY_ACCESS_INSTANCED_PROP(_MaskAnimatedSpeed_arr, _MaskAnimatedSpeed)) * _Time / 10) + float2(0.25f, 0.25f));
											mask_noise *= tex2D(_MaskDistortion, i.masknormaltex.xy + (float(UNITY_ACCESS_INSTANCED_PROP(_MaskAnimatedSpeed_arr, _MaskAnimatedSpeed)) * _Time / 10) - float2(0.5f, 0.5f));
											half2 mask_offset = mask_noise * _MaskOffsetFactor;
										#endif
										i.masktex.xy += mask_offset;
										#ifdef IS_USE_SECOND_COLOR
											half mask_a = saturate(tex2D(_MaskTex, i.masktex) - (1- saturate(i.color.a*_MaskCutOut))) * fixed_mask * (float(UNITY_ACCESS_INSTANCED_PROP(_TintColor2_arr, _TintColor2).a));
										#else
											half mask_a = saturate(tex2D(_MaskTex, i.masktex) - (1 - saturate(i.color.a*_MaskCutOut))) * fixed_mask * (float(UNITY_ACCESS_INSTANCED_PROP(_TintColor_arr, _TintColor).a));
										#endif
								#else
									#ifdef IS_USE_SECOND_COLOR
										half mask_a = tex.a *_MaskCutOut * i.color.a * float(UNITY_ACCESS_INSTANCED_PROP(_TintColor2_arr, _TintColor2).a);
									#else
										half mask_a = tex.a *_MaskCutOut * i.color.a * float(UNITY_ACCESS_INSTANCED_PROP(_TintColor_arr, _TintColor).a);
									#endif
								#endif

								#ifdef IS_USE_SECOND_COLOR
									half4 res = tex * float4(i.color.rgb, 1) * float4(UNITY_ACCESS_INSTANCED_PROP(_TintColor2_arr, _TintColor2).rgb, 1) * float(UNITY_ACCESS_INSTANCED_PROP(_ColorFactor_arr, _ColorFactor));
								#else
									half4 res = tex * float4(i.color.rgb,1) * float4(UNITY_ACCESS_INSTANCED_PROP(_TintColor_arr, _TintColor).rgb,1) * float(UNITY_ACCESS_INSTANCED_PROP(_ColorFactor_arr, _ColorFactor));
								#endif
								half alpha = mask_a *  float(UNITY_ACCESS_INSTANCED_PROP(_ColorFactor_arr, _ColorFactor));
								res.a = saturate(pow(alpha, 2.0f));

								#ifdef IS_RIMLIGHT
									half rim = 1.0 - saturate(dot(normalize(i.viewDir), i.normal));
									res.rgb += float3(UNITY_ACCESS_INSTANCED_PROP(_RimColor_arr, _RimColor).rgb * pow(rim, _RimScale) * _RimStrength);
								#endif

								#ifdef IS_IMPACT
									float3 objPos =i.worldPos;
									float Impact_alpha = 0.0f;
									for (unsigned int index = 0; index < _Points.Length; ++index)
									{
										float Impact = pow(saturate(frac(1.0 - saturate((_Points[index].w*_ImpactSize) - distance(_Points[index].xyz, objPos.xyz))))*saturate(1.0 - _Points[index].w),2);
										Impact_alpha += Impact * _ImpactFactor;
										Impact_alpha = pow(Impact_alpha, 1.1f);
									}

									res.a += Impact_alpha * 5.0f;
								#endif

								#ifdef IS_LINEPASS
									half4 linepasstex = tex2D(_LinePassTex, i.linepasscoord.xy);
									return res *= linepasstex;
								#endif
								UNITY_APPLY_FOG_COLOR(i.fogCoord, res, half4(0, 0, 0, 0));
								return res;
							}
							ENDCG
					}
				}
		}
}