using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class PixelStylizerCamera : MonoBehaviour
{
  public int screenWidthSize = 1280;
  public int pixelSize = 2;
  public bool smoothCamera = false;
  public bool stylizePixels = true;
  //
  //SETTINGS VARIABLES
  //
  public int cameraType;
      //CAMERA STYLE
      public int presetStyle;
      public int customStyleType = 0;
      //COLORS
      private Material colorMaterial;
      public Color Color_1 = new Color32(255, 233, 184, 255);
      public Color Color_2 = new Color32(255, 207, 78, 255);
      public Color Color_3 = new Color32(224, 98, 60, 255);
      public Color Color_4 = new Color32(147, 141, 129, 255);
      public Color Color_5 = new Color32(84, 79, 74, 255);
      public Color Color_6 = new Color32(0, 147, 255, 255);
      public Color Color_7 = new Color32(0, 255, 76, 255);
      public Color Color_8 = new Color32(132, 61, 29, 255);
      public Color shadowColor = new Color32(49, 44, 82, 255);

      public Color customColor_1 = new Color32(240, 240, 240, 255);
      public Color customColor_2 = new Color32(210, 210, 210, 255);
      public Color customColor_3 = new Color32(180, 180, 180, 255);
      public Color customColor_4 = new Color32(150, 150, 150, 255);
      public Color customColor_5 = new Color32(120, 120, 120, 255);
      public Color customColor_6 = new Color32(90, 90, 90, 255);
      public Color customColor_7 = new Color32(60, 60, 60, 255);
      public Color customColor_8 = new Color32(30, 30, 30, 255);
      public Color customShadowColor = new Color32(0, 0, 0, 255);

      public float shadowTolerance = 0.2f;

      //PRIVATE VARIABLES
      private Material pixelMaterial;
    	private float correctedScreenSize;
    	private RenderTexture targetTexture;
    	private float aspectRatio;
    	private int cameraPixelWidth;
    	private int cameraPixelHeight;
    	private int screenWidth;
    	private int screenHeight;

      //Variables used to compare past preset values with the actual ones. These variables
      //are going to be used in the OnValidate() method.
      private int pastPreset;
      private bool firstValidate = false;

    //This function is called when the script is loaded or a value is changed in the Inspector.
    void OnValidate(){

      if(!firstValidate){
        firstValidate = true;
        pastPreset = presetStyle;
      }

      if(screenWidthSize < 320){
        //This prevents the camera to look excessively pixelated
        screenWidthSize = 320;
      }else if(screenWidthSize > 4096){
        //This prevents the camera to render more than 4K
        screenWidthSize = 4096;
      }

      if(pastPreset != presetStyle){
        SetPreset(presetStyle);
        pastPreset = presetStyle;
      }

    }

    void Awake(){

        pixelMaterial = new Material(Shader.Find("Hidden/PSC_Shader"));

    }

    void OnRenderImage(RenderTexture actualTexture, RenderTexture finalTexture){

      //cameraType == 0 is for stylized presets, and CameraType == 1 is for custom.
      if(cameraType == 0){

        //If "stylizePixels" is true, then we are going to use the filter palettes
        //made of 8 colors. If it is false, then we are going to use the shader
        //that uses the true colors of the scene.
        if(stylizePixels){
          pixelMaterial = new Material(Shader.Find("Hidden/PSC_Shader"));
        }else if(!stylizePixels){
          pixelMaterial = new Material(Shader.Find("Hidden/PSC_PixelOnly_Shader"));
        }

        //Gets the real values of the screen size and save them in the "screenWidth"
        //and "screenHeight" variables.
        screenWidth = Screen.width;
    		screenHeight = Screen.height;

        //In this part we determine the values we need to render the scene view with
        //an appropiate aspect ratio.
    		aspectRatio = Camera.main.aspect;
  			correctedScreenSize = pixelSize / ((float)screenWidthSize / (float)screenWidth);
  			cameraPixelWidth = Mathf.RoundToInt(screenWidth / correctedScreenSize);
  			cameraPixelHeight = Mathf.RoundToInt(screenWidth / correctedScreenSize * aspectRatio);

    		targetTexture = RenderTexture.GetTemporary(cameraPixelHeight,cameraPixelWidth);

        //This prevents a smooth filtering in the camera view.
        if(!smoothCamera){
          targetTexture.filterMode = FilterMode.Point;
        }else if(smoothCamera){
          targetTexture.filterMode = FilterMode.Bilinear;
        }
    		targetTexture.anisoLevel=0;

        //In this part we set our camera color palette with the colors given by the user
        pixelMaterial.SetColor("_Col1", Color_1);
    		pixelMaterial.SetColor("_Col2", Color_2);
    		pixelMaterial.SetColor("_Col3", Color_3);
    		pixelMaterial.SetColor("_Col4", Color_4);
    		pixelMaterial.SetColor("_Col5", Color_5);
    		pixelMaterial.SetColor("_Col6", Color_6);
    		pixelMaterial.SetColor("_Col7", Color_7);
        pixelMaterial.SetColor("_Col8", Color_8);
        pixelMaterial.SetColor("_shadowCol", shadowColor);
        pixelMaterial.SetFloat("_shadowTolerance", shadowTolerance);

        //In this part the camera finally renders the image with a pixel style
    		Graphics.Blit(actualTexture, targetTexture, pixelMaterial);
    		Graphics.Blit(targetTexture, finalTexture);
    		RenderTexture.ReleaseTemporary(targetTexture);

      }else if(cameraType == 1){

        if(stylizePixels){
          pixelMaterial = new Material(Shader.Find("Hidden/PSC_Shader"));
        }else if(!stylizePixels){
          pixelMaterial = new Material(Shader.Find("Hidden/PSC_PixelOnly_Shader"));
        }

        //Gets the real values of the screen size and save them in the "screenWidth"
        //and "screenHeight" variables.
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        //In this part we determine the values we need to render the scene view with
        //an appropiate aspect ratio.
        aspectRatio = Camera.main.aspect;
        correctedScreenSize = pixelSize / ((float)screenWidthSize / (float)screenWidth);
        cameraPixelWidth = Mathf.RoundToInt(screenWidth / correctedScreenSize);
        cameraPixelHeight = Mathf.RoundToInt(screenWidth / correctedScreenSize * aspectRatio);

        targetTexture = RenderTexture.GetTemporary(cameraPixelHeight,cameraPixelWidth);

        //This prevents a smooth filtering in the camera view.
        if(!smoothCamera){
          targetTexture.filterMode = FilterMode.Point;
        }else if(smoothCamera){
          targetTexture.filterMode = FilterMode.Bilinear;
        }
        targetTexture.anisoLevel=0;

        //In this part we set our camera color palette with the colors given by the user

        if(customStyleType == 0){

          //2 COLORS
          pixelMaterial.SetColor("_Col1", customColor_1);
          pixelMaterial.SetColor("_Col2", customColor_1);
          pixelMaterial.SetColor("_Col3", customColor_1);
          pixelMaterial.SetColor("_Col4", customColor_1);
          pixelMaterial.SetColor("_Col5", customColor_2);
          pixelMaterial.SetColor("_Col6", customColor_2);
          pixelMaterial.SetColor("_Col7", customColor_2);
          pixelMaterial.SetColor("_Col8", customColor_2);

        }else if(customStyleType == 1){

          //3 COLORS
          pixelMaterial.SetColor("_Col1", customColor_1);
          pixelMaterial.SetColor("_Col2", customColor_1);
          pixelMaterial.SetColor("_Col3", customColor_1);
          pixelMaterial.SetColor("_Col4", customColor_2);
          pixelMaterial.SetColor("_Col5", customColor_2);
          pixelMaterial.SetColor("_Col6", customColor_2);
          pixelMaterial.SetColor("_Col7", customColor_3);
          pixelMaterial.SetColor("_Col8", customColor_3);

        }else if(customStyleType == 2){

          //4 COLORS
          pixelMaterial.SetColor("_Col1", customColor_1);
          pixelMaterial.SetColor("_Col2", customColor_1);
          pixelMaterial.SetColor("_Col3", customColor_2);
          pixelMaterial.SetColor("_Col4", customColor_2);
          pixelMaterial.SetColor("_Col5", customColor_3);
          pixelMaterial.SetColor("_Col6", customColor_3);
          pixelMaterial.SetColor("_Col7", customColor_4);
          pixelMaterial.SetColor("_Col8", customColor_4);

        }else if(customStyleType == 3){

          //8 COLORS
          pixelMaterial.SetColor("_Col1", customColor_1);
          pixelMaterial.SetColor("_Col2", customColor_2);
          pixelMaterial.SetColor("_Col3", customColor_3);
          pixelMaterial.SetColor("_Col4", customColor_4);
          pixelMaterial.SetColor("_Col5", customColor_5);
          pixelMaterial.SetColor("_Col6", customColor_6);
          pixelMaterial.SetColor("_Col7", customColor_7);
          pixelMaterial.SetColor("_Col8", customColor_8);

        }

        pixelMaterial.SetColor("_shadowCol", customShadowColor);
        pixelMaterial.SetFloat("_shadowTolerance", shadowTolerance);

        //In this part the camera finally renders the image with a pixel style
        Graphics.Blit(actualTexture, targetTexture, pixelMaterial);
        Graphics.Blit(targetTexture, finalTexture);
        RenderTexture.ReleaseTemporary(targetTexture);

      }

  	}

    public void SetPreset(int presetIndex){

      switch(presetIndex){

        //STYLE: RETRO PIXEL
        case 0:

        Color_1 = new Color32(255, 233, 184, 255);
        Color_2 = new Color32(255, 207, 78, 255);
        Color_3 = new Color32(224, 98, 60, 255);
        Color_4 = new Color32(147, 141, 129, 255);
        Color_5 = new Color32(84, 79, 74, 255);
        Color_6 = new Color32(0, 147, 255, 255);
        Color_7 = new Color32(0, 255, 76, 255);
        Color_8 = new Color32(132, 61, 29, 255);
        shadowColor = new Color32(49, 44, 82, 255);

        break;

        //STYLE: PROPAGANDA
        case 1:

        Color_1 = new Color32(241, 74, 74, 255);
        Color_2 = new Color32(166, 3, 3, 255);
        Color_3 = new Color32(255, 240, 162, 255);
        Color_4 = new Color32(255, 212, 107, 255);
        Color_5 = new Color32(238, 208, 134, 255);
        Color_6 = new Color32(60, 144, 154, 255);
        Color_7 = new Color32(74, 99, 115, 255);
        Color_8 = new Color32(120, 120, 120, 255);
        shadowColor = new Color32(77, 29, 38, 255);

        break;

        //STYLE: NEO TOKIO
        case 2:

        Color_1 = new Color32(255, 5, 0, 255);
        Color_2 = new Color32(108, 0, 22, 255);
        Color_3 = new Color32(30, 112, 204, 255);
        Color_4 = new Color32(46, 66, 166, 255);
        Color_5 = new Color32(27, 30, 142, 255);
        Color_6 = new Color32(10, 14, 99, 255);
        Color_7 = new Color32(16, 18, 77, 255);
        Color_8 = new Color32(19, 9, 46, 255);
        shadowColor = new Color32(0, 0, 0, 255);

        break;

        //STYLE: GREEN FOREST
        case 3:

        Color_1 = new Color32(252, 214, 141, 255);
        Color_2 = new Color32(252, 214, 141, 255);
        Color_3 = new Color32(174, 183, 110, 255);
        Color_4 = new Color32(174, 183, 110, 255);
        Color_5 = new Color32(132, 147, 87, 255);
        Color_6 = new Color32(132, 147, 87, 255);
        Color_7 = new Color32(80, 96, 50, 255);
        Color_8 = new Color32(80, 96, 50, 255);
        shadowColor = new Color32(19, 43, 20, 255);

        break;

        //STYLE: AUTUMN
        case 4:

        Color_1 = new Color32(255, 255, 255, 255);
        Color_2 = new Color32(244, 224, 224, 255);
        Color_3 = new Color32(166, 166, 166, 255);
        Color_4 = new Color32(250, 60, 78, 255);
        Color_5 = new Color32(96, 74, 61, 255);
        Color_6 = new Color32(235, 210, 126, 255);
        Color_7 = new Color32(231, 107, 60, 255);
        Color_8 = new Color32(154, 121, 95, 255);
        shadowColor = new Color32(60, 8, 60, 255);

        break;

        //STYLE: WILD WEST
        case 5:

        Color_1 = new Color32(255, 242, 182, 255);
        Color_2 = new Color32(255, 203, 83, 255);
        Color_3 = new Color32(231, 148, 69, 255);
        Color_4 = new Color32(147, 100, 55, 255);
        Color_5 = new Color32(106, 219, 114, 255);
        Color_6 = new Color32(65, 166, 73, 255);
        Color_7 = new Color32(255, 255, 255, 255);
        Color_8 = new Color32(123, 123, 123, 255);
        shadowColor = new Color32(89, 40, 21, 255);

        break;

        //STYLE: NOIR
        case 6:

        Color_1 = new Color32(255, 255, 255, 255);
        Color_2 = new Color32(255, 255, 255, 255);
        Color_3 = new Color32(171, 171, 171, 255);
        Color_4 = new Color32(171, 171, 171, 255);
        Color_5 = new Color32(91, 91, 91, 255);
        Color_6 = new Color32(91, 91, 91, 255);
        Color_7 = new Color32(60, 60, 60, 255);
        Color_8 = new Color32(60, 60, 60, 255);
        shadowColor = new Color32(35, 26, 41, 255);

        break;

        //STYLE: OLD PHOTO
        case 7:

        Color_1 = new Color32(255, 208, 143, 255);
        Color_2 = new Color32(192, 144, 72, 255);
        Color_3 = new Color32(154, 107, 36, 255);
        Color_4 = new Color32(99, 51, 29, 255);
        Color_5 = new Color32(99, 51, 29, 255);
        Color_6 = new Color32(99, 51, 29, 255);
        Color_7 = new Color32(0, 0, 0, 255);
        Color_8 = new Color32(0, 0, 0, 255);
        shadowColor = new Color32(43, 27, 19, 255);

        break;

        //STYLE: GALACTIC
        case 8:

        Color_1 = new Color32(255, 255, 255, 255);
        Color_2 = new Color32(255, 220, 0, 255);
        Color_3 = new Color32(0, 100, 255, 255);
        Color_4 = new Color32(255, 0, 84, 255);
        Color_5 = new Color32(126, 255, 0, 255);
        Color_6 = new Color32(205, 0, 255, 255);
        Color_7 = new Color32(255, 200, 102, 255);
        Color_8 = new Color32(16, 20, 38, 255);
        shadowColor = new Color32(12, 11, 24, 255);

        break;

        //STYLE: DEEP OCEAN
        case 9:

        Color_1 = new Color32(112, 179, 236, 255);
        Color_2 = new Color32(112, 179, 236, 255);
        Color_3 = new Color32(60, 121, 197, 255);
        Color_4 = new Color32(60, 121, 197, 255);
        Color_5 = new Color32(41, 65, 130, 255);
        Color_6 = new Color32(41, 65, 130, 255);
        Color_7 = new Color32(29, 41, 82, 255);
        Color_8 = new Color32(29, 41, 82, 255);
        shadowColor = new Color32(22, 33, 36, 255);

        break;

        //STYLE: ARMY
        case 10:

        Color_1 = new Color32(255, 234, 0, 255);
        Color_2 = new Color32(255, 239, 184, 255);
        Color_3 = new Color32(255, 208, 162, 255);
        Color_4 = new Color32(71, 123, 74, 255);
        Color_5 = new Color32(176, 119, 22, 255);
        Color_6 = new Color32(219, 207, 127, 255);
        Color_7 = new Color32(38, 79, 38, 255);
        Color_8 = new Color32(0, 0, 0, 255);
        shadowColor = new Color32(21, 34, 20, 255);

        break;

      }

    }

}
