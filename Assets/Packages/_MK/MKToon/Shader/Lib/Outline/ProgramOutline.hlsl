﻿//////////////////////////////////////////////////////
// MK Toon Outline Program			       			//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2021 All rights reserved.            //
//////////////////////////////////////////////////////

#ifndef MK_TOON_OUTLINE_ONLY_BASE
	#define MK_TOON_OUTLINE_ONLY_BASE
	
	#include "../Core.hlsl"
	#include "Data.hlsl"
	#include "../Surface.hlsl"
	#include "../Composite.hlsl"

	/////////////////////////////////////////////////////////////////////////////////////////////
	// VERTEX SHADER
	/////////////////////////////////////////////////////////////////////////////////////////////
	VertexOutputOutlineOnly OutlineVert(VertexInputOutlineOnly vertexInput)
	{
		UNITY_SETUP_INSTANCE_ID(vertexInput);
		VertexOutputOutlineOnly vertexOutput;
		INITIALIZE_STRUCT(VertexOutputOutlineOnly, vertexOutput);
		UNITY_TRANSFER_INSTANCE_ID(vertexInput, vertexOutput);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(vertexOutput);

		//texcoords
		#if defined(MK_TCM)
			vertexOutput.uv = vertexInput.texcoord0;
		#endif

		#ifdef MK_OUTLINE_MAP
			_OutlineSize *= tex2Dlod(_OutlineMap, float4(vertexOutput.uv, 0, 0)).r;
		#endif

		#ifdef MK_OUTLINE_NOISE
			_OutlineSize = lerp(_OutlineSize, _OutlineSize * NoiseSimple(normalize(vertexInput.vertex.xyz)), _OutlineNoise);
		#endif

		#ifdef MK_VERTEX_ANIMATION
			vertexInput.vertex.xyz = VertexAnimation(PASS_VERTEX_ANIMATION_ARG(_VertexAnimationMap, PASS_VERTEX_ANIMATION_UV(vertexOutput.uv), _VertexAnimationIntensity, _VertexAnimationFrequency.xyz, vertexInput.vertex.xyz, vertexInput.normal));
		#endif

		#ifdef MK_OUTLINE_FADE
			float dist = distance(CAMERA_POSITION_WORLD , mul(MATRIX_M, float4(vertexInput.vertex.xyz, 1.0)).xyz);
			_OutlineSize *= saturate((dist - _OutlineFadeMin) / (_OutlineFadeMax - _OutlineFadeMin));
		#endif

		#if defined(MK_OUTLINE_HULL_ORIGIN)
			//float4x4 modelMatrix = MATRIX_M;
			//vertexInput.vertex.xyz += SafeNormalize(vertexInput.vertex.xyz) * _OutlineSize * OUTLINE_ORIGIN_SCALE;
			//float3 positionWorld = mul(modelMatrix, float4(vertexInput.vertex.xyz, 1.0)).xyz;
			float3 scaleOrigin = 1 + _OutlineSize * OUTLINE_ORIGIN_SCALE;
			float3x3 scale = float3x3
			(
			 scaleOrigin.x, 0, 0,
			 0, scaleOrigin.y, 0,
			 0, 0, scaleOrigin.z
			);
			float3 positionWorld = mul(scale, vertexInput.vertex.xyz);
			positionWorld = mul(MATRIX_M, float4(positionWorld, 1.0)).xyz;
		#elif defined(MK_OUTLINE_HULL_OBJECT)
			#if defined(MK_OUTLINE_DATA_UV7)
				vertexInput.vertex.xyz += vertexInput.normalBaked * _OutlineSize * OUTLINE_OBJECT_SCALE;
			#else
				vertexInput.vertex.xyz += vertexInput.normal * _OutlineSize * OUTLINE_OBJECT_SCALE;
			#endif
		#endif

		#if defined(MK_OUTLINE_HULL_ORIGIN)
			vertexOutput.svPositionClip = ComputeWorldToClipSpace(positionWorld);
		#else
			//Make it pixel perfect and SCALED on different aspects and resolutions
			half scaledAspect = SafeDivide(REFERENCE_ASPECT.x, SafeDivide(_ScreenParams.x, _ScreenParams.y));
			half scaledResolution = SafeDivide(_ScreenParams.x, REFERENCE_RESOLUTION.x);
			vertexOutput.svPositionClip = ComputeObjectToClipSpace(vertexInput.vertex.xyz);

			#if defined(MK_OUTLINE_DATA_UV7)
				half3 normalBakedClip = ComputeNormalObjectToClipSpace(vertexInput.normalBaked.xyz);
				vertexOutput.svPositionClip.xy += 2 * _OutlineSize * SafeDivide(normalBakedClip.xy, _ScreenParams.xy) * scaledAspect * scaledResolution;
			#else
				half3 normalClip = ComputeNormalObjectToClipSpace(vertexInput.normal.xyz);
				vertexOutput.svPositionClip.xy += 2 * _OutlineSize * SafeDivide(normalClip.xy, _ScreenParams.xy) * scaledAspect * scaledResolution;
			#endif
		#endif

		#if defined(MK_VERTCLR) || defined(MK_POLYBRUSH)
			vertexOutput.color = vertexInput.color;
		#endif

		#if defined(MK_PARALLAX)
			vertexOutput.viewTangent = ComputeViewTangent(ComputeViewObject(vertexInput.vertex.xyz), vertexInput.normal, vertexInput.tangent.xyz, cross(vertexInput.normal, vertexInput.tangent.xyz) * vertexInput.tangent.w * unity_WorldTransformParams.w);
		#endif

		#ifdef MK_FOG
			vertexOutput.fogFactor = FogFactorVertex(vertexOutput.svPositionClip.z);
		#endif

		#ifdef MK_POS_CLIP
			vertexOutput.positionClip = vertexOutput.svPositionClip;
		#endif
		#ifdef MK_POS_NULL_CLIP
			vertexOutput.nullClip = ComputeObjectToClipSpace(0);
		#endif

		#ifdef MK_FLIPBOOK
			vertexOutput.flipbookUV.xy = VERTEX_INPUT.texcoord0.zw;
			vertexOutput.flipbookUV.z = VERTEX_INPUT.texcoordBlend;
		#endif
		return vertexOutput;
	}

	/////////////////////////////////////////////////////////////////////////////////////////////
	// FRAGMENT SHADER
	/////////////////////////////////////////////////////////////////////////////////////////////
	half4 OutlineFrag(VertexOutputOutlineOnly vertexOutput) : SV_Target
	{
		UNITY_SETUP_INSTANCE_ID(vertexOutput);
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(vertexOutput);

		MKSurfaceData surfaceData = ComputeSurfaceData
		(
			PASS_POSITION_WORLD_ARG(0)
			PASS_FOG_FACTOR_WORLD_ARG(vertexOutput.fogFactor)
			PASS_BASE_UV_ARG(float4(vertexOutput.uv.xy, 0, 0))
			PASS_LIGHTMAP_UV_ARG(0)
			PASS_VERTEX_COLOR_ARG(vertexOutput.color)
			PASS_NORMAL_WORLD_ARG(1)
			PASS_VERTEX_LIGHTING_ARG(0)
			PASS_TANGENT_WORLD_ARG(1)
			PASS_VIEW_TANGENT_ARG(vertexOutput.viewTangent)
			PASS_BITANGENT_WORLD_ARG(1)
			PASS_POSITION_CLIP_ARG(vertexOutput.positionClip)
			PASS_NULL_CLIP_ARG(vertexOutput.nullClip)
			PASS_FLIPBOOK_UV_ARG(vertexOutput.flipbookUV)
		);
		Surface surface = InitSurface(surfaceData, PASS_SAMPLER_2D(_AlbedoMap), autoLP4(_OutlineColor.rgb, _AlbedoColor.a), vertexOutput.svPositionClip);
		MKPBSData pbsData = ComputePBSData(surface, surfaceData);
		Composite(surface, surfaceData, pbsData);

		return surface.final;
	}
#endif