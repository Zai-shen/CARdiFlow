// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
//#pragma target 3.0
Shader "VATMaterial"
{
	Properties
	{
		_Timeposition("Time position", Range( 0 , 1)) = 0
		_Color("Color", Color) = (1,0.5773492,0,0)
		_Metalness("Metalness", Range( 0 , 1)) = 0
		_Roughness("Roughness", Range( 0 , 1)) = 0
		_Framecount("Frame count", Float) = 240
		_VAT_positions("VAT_positions", 2D) = "white" {}
		_VAT_normals("VAT_normals", 2D) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			half filler;
		};

		uniform sampler2D _VAT_positions;
		uniform float4 _VAT_positions_TexelSize;
		uniform float _Framecount;
		uniform float _Timeposition;
		uniform sampler2D _VAT_normals;
		uniform float4 _Color;
		uniform float _Metalness;
		uniform float _Roughness;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 appendResult9 = (float2(( _VAT_positions_TexelSize.x * ( ( _Framecount - 1.0 ) * _Timeposition ) ) , 0.0));
			float2 temp_output_21_0 = ( appendResult9 + v.texcoord1.xy );
			v.vertex.xyz += tex2Dlod( _VAT_positions, float4( temp_output_21_0, 0, 0.0) ).rgb;
			v.normal = tex2Dlod( _VAT_normals, float4( temp_output_21_0, 0, 0.0) ).rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _Color.rgb;
			o.Metallic = _Metalness;
			o.Smoothness = ( 1.0 - _Roughness );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
}