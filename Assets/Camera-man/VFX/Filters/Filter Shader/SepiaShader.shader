// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Odin Shaders/Sepia"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		
		
		ZTest Always
		Cull Off
		ZWrite Off

		
		Pass
		{ 
			CGPROGRAM 

			

			#pragma vertex vert_img_custom 
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			

			struct appdata_img_custom
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				
			};

			struct v2f_img_custom
			{
				float4 pos : SV_POSITION;
				half2 uv   : TEXCOORD0;
				half2 stereoUV : TEXCOORD2;
		#if UNITY_UV_STARTS_AT_TOP
				half4 uv2 : TEXCOORD1;
				half4 stereoUV2 : TEXCOORD3;
		#endif
				
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			

			v2f_img_custom vert_img_custom ( appdata_img_custom v  )
			{
				v2f_img_custom o;
				
				o.pos = UnityObjectToClipPos( v.vertex );
				o.uv = float4( v.texcoord.xy, 1, 1 );

				#if UNITY_UV_STARTS_AT_TOP
					o.uv2 = float4( v.texcoord.xy, 1, 1 );
					o.stereoUV2 = UnityStereoScreenSpaceUVAdjust ( o.uv2, _MainTex_ST );

					if ( _MainTex_TexelSize.y < 0.0 )
						o.uv.y = 1.0 - o.uv.y;
				#endif
				o.stereoUV = UnityStereoScreenSpaceUVAdjust ( o.uv, _MainTex_ST );
				return o;
			}

			half4 frag ( v2f_img_custom i ) : SV_Target
			{
				#ifdef UNITY_UV_STARTS_AT_TOP
					half2 uv = i.uv2;
					half2 stereoUV = i.stereoUV2;
				#else
					half2 uv = i.uv;
					half2 stereoUV = i.stereoUV;
				#endif	
				
				half4 finalColor;

				// ase common template code
				float2 uv_MainTex = i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode3 = tex2D( _MainTex, uv_MainTex );
				float G10 = tex2DNode3.g;
				float B11 = tex2DNode3.b;
				float clampResult35 = clamp( ( ( (0) * 0.393 ) + ( G10 * 0.769 ) + ( B11 * 0.189 ) ) , 0.0 , 1.0 );
				float targetR14 = clampResult35;
				float clampResult34 = clamp( ( ( (0) * 0.349 ) + ( G10 * 0.686 ) + ( B11 * 0.168 ) ) , 0.0 , 1.0 );
				float targetG22 = clampResult34;
				float clampResult43 = clamp( ( ( (0) * 0.272 ) + ( G10 * 0.534 ) + ( B11 * 0.131 ) ) , 0.0 , 1.0 );
				float targetB28 = clampResult43;
				float4 appendResult4 = (float4(targetR14 , targetG22 , targetB28 , 1.0));
				

				finalColor = appendResult4;

				return finalColor;
			} 
			ENDCG 
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17400
1920;131;1920;763;1430.793;577.3026;1;True;False
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;2;-1255.324,-195.3943;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-1058.042,-176.7326;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;11;-497.6433,-14.60303;Inherit;False;B;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;10;-503.6433,-94.60303;Inherit;False;G;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;17;-972.06,-881.4025;Inherit;False;10;G;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;23;-926.2005,-433.3205;Inherit;False;11;B;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;13;-900.146,-1267.031;Inherit;False;-1;;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-536.6893,-884.8506;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.686;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-550.4252,-1259.788;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.769;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-555.3217,-517.4907;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.534;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-559.4252,-1141.788;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.189;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-564.3217,-399.4909;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.131;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-547.6895,-990.8506;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.349;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-548.6895,-766.851;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.168;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-558.4252,-1365.788;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.393;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-563.3217,-623.4906;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.272;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;27;-331.0424,-549.7336;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-315.4098,-917.0934;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-344.1456,-1288.031;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;43;-147.9071,-573.6877;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;35;-137.5813,-1330.87;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;34;-101.5813,-953.8704;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;22;108.5902,-947.0934;Inherit;False;targetG;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;14;67.85423,-1335.031;Inherit;False;targetR;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;28;114.9579,-571.7336;Inherit;False;targetB;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;29;-93.5813,-117.8704;Inherit;False;22;targetG;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;30;-101.5813,-22.87036;Inherit;False;28;targetB;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;16;-89.74695,-207.3367;Inherit;False;14;targetR;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;9;-501.6433,-168.603;Inherit;False;R;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;4;195.1705,-139.7462;Inherit;True;COLOR;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;498,-113;Float;False;True;-1;2;ASEMaterialInspector;0;2;Odin Shaders/Sepia;c71b220b631b6344493ea3cf87110c93;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;True;2;False;-1;False;False;True;2;False;-1;True;7;False;-1;False;True;0;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;3;0;2;0
WireConnection;11;0;3;3
WireConnection;10;0;3;2
WireConnection;19;0;17;0
WireConnection;7;0;17;0
WireConnection;25;0;17;0
WireConnection;8;0;23;0
WireConnection;26;0;23;0
WireConnection;18;0;13;0
WireConnection;20;0;23;0
WireConnection;6;0;13;0
WireConnection;24;0;13;0
WireConnection;27;0;24;0
WireConnection;27;1;25;0
WireConnection;27;2;26;0
WireConnection;21;0;18;0
WireConnection;21;1;19;0
WireConnection;21;2;20;0
WireConnection;12;0;6;0
WireConnection;12;1;7;0
WireConnection;12;2;8;0
WireConnection;43;0;27;0
WireConnection;35;0;12;0
WireConnection;34;0;21;0
WireConnection;22;0;34;0
WireConnection;14;0;35;0
WireConnection;28;0;43;0
WireConnection;9;0;3;1
WireConnection;4;0;16;0
WireConnection;4;1;29;0
WireConnection;4;2;30;0
WireConnection;1;0;4;0
ASEEND*/
//CHKSM=DAAEFD2EF4405FF9CEC5B98CBC34242D8EB3D6F6