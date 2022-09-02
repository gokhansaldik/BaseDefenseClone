﻿//////////////////////////////////////////////////////
// MK Toon Forward Base Setup			       		//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2021 All rights reserved.            //
//////////////////////////////////////////////////////

#ifndef MK_TOON_FORWARD_BASE_SETUP
	#define MK_TOON_FORWARD_BASE_SETUP

	#ifndef MK_FORWARD_BASE_PASS
		#define MK_FORWARD_BASE_PASS
	#endif
	
	//Internal keyword for environment reflections
	#if !defined(_MK_ENVIRONMENT_REFLECTIONS_ADVANCED)
		#if defined(MK_URP)
			#ifndef _ENVIRONMENTREFLECTIONS_OFF
				#define _ENVIRONMENTREFLECTIONS_OFF
			#endif
		#else
			//Legay & LWRP
			#ifndef _GLOSSYREFLECTIONS_OFF
				#define _GLOSSYREFLECTIONS_OFF
			#endif
		#endif
	#endif

	//if particles are used disable dynamic lightmap
	#if defined(DYNAMICLIGHTMAP_ON) && defined(MK_PARTICLES)
		//#undef DYNAMICLIGHTMAP_ON
	#endif

	#if defined(MK_URP) || defined(MK_LWRP)
		#ifdef _MK_RECEIVE_SHADOWS
			#ifdef _RECEIVE_SHADOWS_OFF
				#undef _RECEIVE_SHADOWS_OFF
			#endif
		#else
			#ifndef _RECEIVE_SHADOWS_OFF
				#define _RECEIVE_SHADOWS_OFF
			#endif
		#endif
	#endif

	#include "../Core.hlsl"

	#if defined(MK_URP) && defined(MK_PARTICLES) && UNITY_VERSION >= 202020
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ParticlesInstancing.hlsl"
	#endif

	#include "ProgramForward.hlsl"
#endif