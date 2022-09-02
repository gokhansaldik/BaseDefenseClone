//////////////////////////////////////////////////////
// MK Toon Common					       			//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2021 All rights reserved.            //
//////////////////////////////////////////////////////

#ifndef MK_TOON_COMMON
	#define MK_TOON_COMMON

	#if defined(MK_URP)
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
	#elif defined(MK_LWRP)
		#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
	#else
		#include "UnityCG.cginc"
	#endif

	#include "Config.hlsl"
	#include "Pipeline.hlsl"

	/////////////////////////////////////////////////////////////////////////////////////////////
	// COMMON
	/////////////////////////////////////////////////////////////////////////////////////////////
	inline float Stutter(float t, float f)
	{
		return frac(SafeDivide(round(t * f), f));
	}
	inline float2 Stutter(float t, float2 f)
	{
		return frac(SafeDivide(round(t * f), f));
	}
	inline float3 Stutter(float t, float3 f)
	{
		return frac(SafeDivide(round(t * f), f));
	}

	inline half SoftFade(float near, float far, float4 ndc)
	{
		//near OR far has to be > 0.0
		float sceneDepth = ComputeLinearDepth(SampleDepth(SafeDivide(ndc.xy,ndc.w)));
		float depth = ComputeLinearDepth(SafeDivide(ndc.z, ndc.w));

		//Remap to 0 - 1
		far = Rcp(far - near);
		return saturate(far * ((sceneDepth - near) - depth));
	}

	inline half CameraFade(float near, float far, float4 ndc)
	{
		float depth = ComputeLinearDepth(SafeDivide(ndc.z, ndc.w));

		//Remap to 0 - 1
		far = Rcp(far - near);
		return saturate((depth - near) * far);
	}

	inline void MixAlbedoDetail(inout half3 albedo, DECLARE_TEXTURE_2D_ARGS(tex, samplerTex), float2 uv, float3 blendUV)
	{
		half4 detail = SAMPLE_TEX2D_FLIPBOOK(tex, samplerTex, uv, blendUV);
		detail.rgb *= _DetailColor.rgb;
		#if defined(MK_DETAIL_BLEND_MIX)
			albedo = lerp(albedo, detail.rgb, _DetailMix * detail.a);
		#elif defined(MK_DETAIL_BLEND_ADD)
			albedo += lerp(0.0h, detail.rgb, _DetailMix * detail.a);
		#else //MK_DETAIL_BLEND_MULTIPLY
			albedo *= lerp(1.0h, detail.rgb, _DetailMix * detail.a);
		#endif
	}
	
	inline half2 Parallax(half3 viewTangent, half height, half parallax, half bias)
	{
		return SafeDivide(viewTangent.xy, viewTangent.z + bias) * (height * parallax - parallax * 0.5);
	}

	inline half3 UnpackRawNormal(half4 rawNormal, half bumpiness)
	{
		half3 unpackedNormal;
		#if defined(UNITY_NO_DXT5nm)
			unpackedNormal = rawNormal.rgb * 2.0 - 1.0;
		#else
			rawNormal.r *= rawNormal.a;
			unpackedNormal = half3(2.0 * rawNormal.a - 1.0, 2.0 * rawNormal.g - 1.0, 0.0);
		#endif
		unpackedNormal.xy *= bumpiness;
		#if !defined(UNITY_NO_DXT5nm)
			//unpackedNormal.z = sqrt(1.0 - dot(unpackedNormal.xy, unpackedNormal.xy));
			unpackedNormal.z = 1.0 - 0.5 * dot(unpackedNormal.xy, unpackedNormal.xy); //approximation
		#endif
		return unpackedNormal;
	}

	inline half2 UnpackDudv(DECLARE_TEXTURE_2D_ARGS(dudvMap, samplerTex), float2 uv, float3 blendUV)
	{
		//somehow a range of [-1, 1] is not possible unless the texture is packed as a normal map
		//therefore its encoded as a normal map and should also be imported as a normal map
		//Normal map or Dudv map can be used
		return UnpackRawNormal(SAMPLE_TEX2D_FLIPBOOK(dudvMap, samplerTex, uv, blendUV), 1).rg;
	}

	inline half3 UnpackNormalMap(DECLARE_TEXTURE_2D_ARGS(normalMap, samplerTex), float2 uv, float3 blendUV, half bumpiness)
	{
		half4 rawNormal = SAMPLE_TEX2D_FLIPBOOK(normalMap, samplerTex, uv, blendUV);
		return UnpackRawNormal(rawNormal, bumpiness);
	}

	inline half3 UnpackNormalMap(DECLARE_TEXTURE_2D_ARGS(normalMap, samplerTex), float2 uv, half bumpiness)
	{
		half4 rawNormal = SampleTex2D(PASS_TEXTURE_2D(normalMap, samplerTex), uv);
		return UnpackRawNormal(rawNormal, bumpiness);
	}

	inline half3 NormalMappingWorld(DECLARE_TEXTURE_2D_ARGS(normalMap, samplerTex), float2 uv, float3 blendUV, half bumpiness, half3x3 tbn)
	{
		return SafeNormalize(mul(UnpackNormalMap(PASS_TEXTURE_2D(normalMap, samplerTex), uv, blendUV, bumpiness), tbn));
	}

	inline half3 NormalMappingWorld(DECLARE_TEXTURE_2D_ARGS(normalMap, samplerTex), float2 uvMain, float3 blendUV, half bumpiness, DECLARE_TEXTURE_2D_ARGS(detailNormalMap, samplerTex2), float2 uvDetail, half bumpinessDetail, half3x3 tbn)
	{
		half3 normalTangent = UnpackNormalMap(PASS_TEXTURE_2D(normalMap, samplerTex), uvMain, blendUV, bumpiness);
		half3 normalDetailTangent = UnpackNormalMap(PASS_TEXTURE_2D(detailNormalMap, samplerTex2), uvDetail, blendUV, bumpinessDetail);
		return SafeNormalize(mul(SafeNormalize(half3(normalTangent.xy + normalDetailTangent.xy, lerp(normalTangent.z, normalDetailTangent.z, 0.5))), tbn));
	}

	//threshold based lighting type
	inline half Cel(half threshold, half smoothnessMin, half smoothnessMax, half value)
	{
		#ifdef MK_LOCAL_ANTIALIASING
			half ddxy = fwidth(value);
			return smoothstep(threshold - smoothnessMin - ddxy, threshold + smoothnessMax + ddxy, value);
		#else
			return smoothstep(threshold - smoothnessMin, threshold + smoothnessMax, value);
		#endif
	}

	/*
	inline half SmoothFloor(half v, half smoothness, half threshold)
	{
		threshold = 1.0 - threshold;
		return ((cos(max((frac(v + threshold) - (1 - smoothness)) / smoothness, 0.5) * PI_TWO) + 1) * 0.5) + floor(v + threshold);
	}
	*/

	//level based lighting type
	inline half Banding(half v, half levels, half smoothnessMin, half smoothnessMax, half threshold, half fade)
	{	
		/*
		levels--;
		half banding = v * lerp(1, 3, fade);
		return saturate(SafeDivide(SmoothFloor(banding * levels, smoothnessMin + smoothnessMax, threshold), levels));
		*/
		
		levels--;
		threshold = lerp(threshold, threshold * levels, fade);
		half vl = v * lerp(1, levels, fade);
		half levelStep = Rcp(levels);

		half bands = Cel(threshold, smoothnessMin, smoothnessMax, vl);
		bands += Cel(levelStep + threshold, smoothnessMin, smoothnessMax, vl);
		bands += Cel(levelStep * 2 + threshold, smoothnessMin, smoothnessMax, vl) * step(3, levels);
		bands += Cel(levelStep * 3 + threshold, smoothnessMin, smoothnessMax, vl) * step(4, levels);
		bands += Cel(levelStep * 4 + threshold, smoothnessMin, smoothnessMax, vl) * step(5, levels);
		bands += Cel(levelStep * 5 + threshold, smoothnessMin, smoothnessMax, vl) * step(6, levels);

		return bands * levelStep;
		//return lerp(bands, Cel(threshold, smoothnessMin, smoothnessMax, v), smoothnessMin + smoothnessMax); // * step(threshold, 0)
		//return ceil(v * levels) / levels;
	}

	//Rampcolor when dissolving
	inline half3 DissolveRamp(half dissolveValue, DECLARE_TEXTURE_2D_ARGS(dissolveBorderRampTex, samplerTex), half4 dissolveBorderColor, half dissolveBorderSize, half dissolveAmount, half2 uv, half3 baseCol)
	{
		half sv = dissolveBorderSize * dissolveAmount;
		return lerp(baseCol, dissolveBorderColor.rgb * SampleTex2D(PASS_TEXTURE_2D(dissolveBorderRampTex, samplerTex), half2(dissolveValue * Rcp(sv), T_V)).rgb, dissolveBorderColor.a * step(dissolveValue, sv));
	}
	//Color when dissolving
	inline half3 DissolveColor(half dissolveValue, half4 dissolveBorderColor, half dissolveBorderSize, half dissolveAmount, half3 baseCol)
	{
		return lerp(baseCol, dissolveBorderColor.rgb, dissolveBorderColor.a * step(dissolveValue, dissolveBorderSize * dissolveAmount));
	}

	//Contrast - Saturation - Brightness
	inline half3 ColorGrading(half3 color, half brightness, half saturation, half contrast)
	{
		half3 bc = color * brightness;
		half i = dot(bc, REL_LUMA);
		#ifdef MK_FORWARD_ADD_PASS
			color = lerp(half3(0.0, 0.0, 0.0), lerp(half3(i, i, i), bc, saturation), contrast);
		#else
			color = lerp(half3(0.5, 0.5, 0.5), lerp(half3(i, i, i), bc, saturation), contrast);
		#endif
		return color;
	}

	inline float NoiseSimple(float3 v)
	{
		return frac(sin( dot(v, REL_LUMA * 123456.54321)) * 987654.56789);
	}

	inline half Drawn(half value, half artistic, half artisticClampMin, half artisticClampMax)
	{			
		//currently implemented as soft pattern, see repo for hard pattern prototype
		#ifdef MK_LOCAL_ANTIALIASING
			half ddxy = fwidth(value);
			return lerp(artisticClampMin, 1, value) * smoothstep(artistic - HALF_MIN - ddxy, artistic + ddxy, clamp(value, artisticClampMin, artisticClampMax));
			//return lerp(artisticClampMin, 1, value) * smoothstep(artistic - T_H - ddxy, artistic, clamp(value, artisticClampMin, artisticClampMax));
		#else
			return lerp(artisticClampMin, 1, value) * smoothstep(artistic - HALF_MIN, artistic, clamp(value, artisticClampMin, artisticClampMax));
		#endif
	}
	inline half Drawn(half value, half artistic, half artisticClampMax)
	{			
		return Drawn(value, artistic, 0, artisticClampMax);
	}

	inline half Hatching(half3 dark, half3 bright, half value, half threshold)
	{
		//value of 0 = black, no strokes visible
		half stepMax = clamp(value, threshold, 1.0h) * 6.0h;
		half3 darkCoeff, brightCoeff;
		#ifdef MK_LOCAL_ANTIALIASING
			half ddxy = fwidth(value);
			darkCoeff = saturate(stepMax - half3(0, 1, 2) - ddxy); //half3(0, 1, 2));  7 step
			brightCoeff = saturate(stepMax - half3(3, 4, 5) - ddxy);
		#else
			darkCoeff = saturate(stepMax - half3(0, 1, 2)); //half3(0, 1, 2));  7 step
			brightCoeff = saturate(stepMax - half3(3, 4, 5));
		#endif

		//step wise coeff
		darkCoeff.xy -= darkCoeff.yz;
		darkCoeff.z -= brightCoeff.x;
		brightCoeff.xy -= brightCoeff.yz;
		//last step = 0 (7max)
		
		//lerped coeff
		//darkCoeff = lerp(darkCoeff, half3(darkCoeff.yz, brightCoeff.x), 0.5);
		//brightCoeff = lerp(brightCoeff, half3(brightCoeff.yz, 0), 0.5);

		half3 d = dark * darkCoeff;
		half3 b = bright * brightCoeff;

		return d.b + d.g + d.r + b.b + b.g + b.r + bright.r * max(0, value - 1.0);
	}

	inline half Sketch(half vMin, half vMax, half value)
	{
		#ifdef MK_LOCAL_ANTIALIASING
			half ddxy = fwidth(value);
			return max(lerp(vMin - T_V - ddxy, vMax, value), 0);
		#else
			return max(lerp(vMin - T_V, vMax, value), 0);
		#endif
	}
	inline half Sketch(half vMax, half value)
	{
		return lerp(0, vMax, value);
	}

	//Half Lambert - Valve
	inline half HalfWrap(half value, half wrap)
	{
		return FastPow2(value * wrap + (1.0 - wrap));
	}

	inline half HalfWrap(half value)
	{
		return FastPow2(value * 0.5 + 0.5);
	}

	//Unity based HSV - RGB
	inline half3 RGBToHSV(half3 c)
	{
		const half4 K = half4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
		half4 p = lerp(half4(c.bg, K.wz), half4(c.gb, K.xy), step(c.b, c.g));
		half4 q = lerp(half4(p.xyw, c.r), half4(c.r, p.yzx), step(p.x, c.r));
		half d = q.x - min(q.w, q.y);
		const half e = 1.0e-4;
		return half3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
	}
	inline half3 HSVToRGB(half3 c)
	{
		const half4 K = half4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
		half3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
		return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
	}

	inline float3 VertexAnimationSine(float3 positionObject, half intensity, half3 frequency)
	{
		#ifdef MK_VERTEX_ANIMATION_STUTTER
			positionObject += sin((positionObject.zxx + Stutter(_Time.y, frequency.zyx)) * frequency.zyx) * intensity;
		#else
			positionObject += sin((positionObject.zxx + _Time.y) * frequency.zyx) * intensity;
		#endif
		return positionObject;
	}

	inline float3 VertexAnimationPulse(float3 positionObject, half3 normalObject, half intensity, half3 frequency)
	{
		#ifdef MK_VERTEX_ANIMATION_STUTTER
			//positionObject += SafeNormalizenormalObject * sin(Stutter(_Time.y, frequency.xyz) * frequency.xyz) * intensity;
			float3 scaleAnimation = 1.0 + sin(Stutter(_Time.y, frequency.xyz) * frequency.xyz) * intensity;
			float3x3 scale = float3x3
			(
			 scaleAnimation.x, 0, 0,
			 0, scaleAnimation.y, 0,
			 0, 0, scaleAnimation.z
			);
			positionObject = mul(scale, positionObject.xyz);
		#else
			//positionObject += normalObject * sin((_Time.y) * frequency.xyz) * intensity;
			float3 scaleAnimation = 1.0 + sin((_Time.y) * frequency.xyz) * intensity;
			float3x3 scale = float3x3
			(
			 scaleAnimation.x, 0, 0,
			 0, scaleAnimation.y, 0,
			 0, 0, scaleAnimation.z
			);
			positionObject = mul(scale, positionObject.xyz);
		#endif
		return positionObject;
	}

	inline float3 VertexAnimationNoise(float3 positionObject, half3 normalObject, half intensity, half3 frequency)
	{
		#ifdef MK_VERTEX_ANIMATION_STUTTER
			positionObject += normalObject * sin(Stutter(NoiseSimple(positionObject) * _Time.y, frequency.xyz) * frequency.xyz) * intensity;
		#else
			positionObject += normalObject * sin((NoiseSimple(positionObject) * _Time.y) * frequency.xyz) * intensity;
		#endif
		return positionObject;
	}

	#if !defined(MK_VERTEX_ANIMATION_SINE)
		#define PASS_VERTEX_ANIMATION_ARG(vertexAnimationMap, uv, intensity, frequency, positionObject, normalObject) vertexAnimationMap, uv, intensity, frequency, positionObject, normalObject
	#else
		#define PASS_VERTEX_ANIMATION_ARG(vertexAnimationMap, uv, intensity, frequency, positionObject, normalObject) vertexAnimationMap, uv, intensity, frequency, positionObject
	#endif

	inline float3 VertexAnimation
	(
		sampler2D vertexAnimationMap
		, float2 uv
		, half intensity
		, float3 frequency
		, float3 positionObject
		#ifndef MK_VERTEX_ANIMATION_SINE
			, half3 normalObject
		#endif
	)
	{
		#ifdef MK_VERTEX_ANIMATION_MAP
			intensity *= tex2Dlod(vertexAnimationMap, float4(uv, 0, 0)).r;
		#endif
		#if defined(MK_VERTEX_ANIMATION_PULSE)
			return VertexAnimationPulse(positionObject, normalObject, intensity, frequency);
		#elif defined(MK_VERTEX_ANIMATION_NOISE)
			return VertexAnimationNoise(positionObject, normalObject, intensity, frequency);
		#else
			return VertexAnimationSine(positionObject, intensity, frequency);
		#endif
	}
#endif