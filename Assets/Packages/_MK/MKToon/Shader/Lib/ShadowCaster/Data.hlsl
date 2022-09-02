﻿//////////////////////////////////////////////////////
// MK Toon ShadowCaster Data			       		//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2021 All rights reserved.            //
//////////////////////////////////////////////////////

#ifndef MK_TOON_SHADOWCASTER_IO
	#define MK_TOON_SHADOWCASTER_IO

	#include "../Core.hlsl"
	
	/////////////////////////////////////////////////////////////////////////////////////////////
	// INPUT
	/////////////////////////////////////////////////////////////////////////////////////////////
	struct VertexInputShadowCaster
	{
		float4 vertex : POSITION;
		half3 normal : NORMAL;
		#ifdef MK_PARALLAX
			half4 tangent : TANGENT;
		#endif
		#if defined(MK_VERTCLR) || defined(MK_POLYBRUSH)
			autoLP4 color : COLOR0;
		#endif
		#ifdef MK_TCM
			float2 texcoord0 : TEXCOORD0;
		#endif
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	/////////////////////////////////////////////////////////////////////////////////////////////
	// OUTPUT
	/////////////////////////////////////////////////////////////////////////////////////////////
	struct VertexOutputShadowCaster
	{	
		#ifdef MK_LEGACY_RP
			V2F_SHADOW_CASTER_NOPOS
		#endif
		#if defined(MK_VERTCLR) || defined(MK_POLYBRUSH)
			autoLP4 color : COLOR0;
		#endif
		#ifdef MK_PARALLAX
			half3 viewTangent : TEXCOORD6;
		#endif
		#ifdef MK_TCM
			float2 uv : TEXCOORD7;
		#endif
		UNITY_VERTEX_INPUT_INSTANCE_ID
		UNITY_VERTEX_OUTPUT_STEREO
	};
#endif