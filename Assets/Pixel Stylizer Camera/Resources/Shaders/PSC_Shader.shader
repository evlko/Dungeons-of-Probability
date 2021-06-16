Shader "Hidden/PSC_Shader"
{
	Properties
	{

    //Variables needed to create a pixelated rendering with 8 colors
    //given by the user.

		_MainTex ("Texture", 2D) = "white" {}
		_Color1 ("Color 1",Color)=(1,1,1,1)
		_Color2 ("Color 2",Color)=(1,1,1,1)
		_Color3 ("Color 3",Color)=(1,1,1,1)
		_Color4 ("Color 4",Color)=(1,1,1,1)
		_Color5 ("Color 5",Color)=(1,1,1,1)
		_Color6 ("Color 6",Color)=(1,1,1,1)
		_Color7 ("Color 7",Color)=(1,1,1,1)
    _Color8 ("Color 8",Color)=(1,1,1,1)
		_Color9 ("Color 9",Color)=(1,1,1,1)

	}

	SubShader {

		Pass {

			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"
			uniform sampler2D _MainTex;
			uniform float4 _Col1;
			uniform float4 _Col2;
			uniform float4 _Col3;
			uniform float4 _Col4;
			uniform float4 _Col5;
			uniform float4 _Col6;
			uniform float4 _Col7;
      uniform float4 _Col8;
			uniform float4 _shadowCol;
			uniform float _shadowTolerance;


			float4  frag(v2f_img i): COLOR
			{

        //Array that contains the 8 colors given by the user.
				float4 colors[8] = {_Col1, _Col2, _Col3, _Col4, _Col5, _Col6, _Col7, _Col8};
				float4 actualColor = tex2D(_MainTex,i.uv);

        //If the actual color divided by 3 (the RGB average) is less than the
        //shadowTolerance, then this color is either a shadow or a very dark color.
				//In this case, we are going to replace that color with the "ShadowColor"
				//chosen by the user.

				if ( (actualColor.r + actualColor.g + actualColor.b) / 3 < _shadowTolerance)
					return _shadowCol;

				//Otherwise, we are going to replace the actual color with the nearest
				//color chosen by the user.

				int currentMinIndex = 1;
				float minDistance = 9999;

				float space;

				for (int rep = 0; rep < 8; rep++)
				{

					space = distance(actualColor, colors[rep]);
					if (space < minDistance)
					{
						currentMinIndex = rep;
						minDistance = space;
					}

				}

				return colors[currentMinIndex];

			}

			ENDCG

		}

	}

}
