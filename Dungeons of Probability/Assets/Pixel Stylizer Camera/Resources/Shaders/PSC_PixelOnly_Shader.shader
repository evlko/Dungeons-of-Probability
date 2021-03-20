Shader "Hidden/PSC_PixelOnly_Shader"
{

    //In this shader, instead of setting 8 colors for the camera, it takes the
    //true colors of the scene and display them with a pixelated style.

    Properties
    {
      _MainTex ("Texture", 2D) = "white" {}
    }

      SubShader {

        Pass {

          CGPROGRAM
          #pragma vertex vert_img
          #pragma fragment frag

          #include "UnityCG.cginc"
          uniform sampler2D _MainTex;

          float4  frag(v2f_img foundColor): COLOR
          {
            float4 col = tex2D(_MainTex, foundColor.uv);
            return col;
          }

          ENDCG

        }

      }

}
