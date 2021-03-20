using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(PixelStylizerCamera))]
[System.Serializable]
public class PixelStylizerCamera_Editor : Editor{

  enum displayFieldType {DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields}
  displayFieldType DisplayFieldType;

  private PixelStylizerCamera PSC;
  private SerializedObject SO;

  int tabPresets;
  int tabCustom;

  private SerializedProperty screenWidthSize;
  private SerializedProperty pixelSize;
  private SerializedProperty smoothCamera;
  private SerializedProperty stylizePixels;

  //
  //
  //SETTINGS VARIABLES
  //
  //
  private SerializedProperty cameraType;
      //CAMERA STYLE
      private SerializedProperty presetStyle;
      private SerializedProperty customStyleType;
      //COLORS
      private SerializedProperty Color_1;
      private SerializedProperty Color_2;
      private SerializedProperty Color_3;
      private SerializedProperty Color_4;
      private SerializedProperty Color_5;
      private SerializedProperty Color_6;
      private SerializedProperty Color_7;
      private SerializedProperty Color_8;

      private SerializedProperty customColor_1;
      private SerializedProperty customColor_2;
      private SerializedProperty customColor_3;
      private SerializedProperty customColor_4;
      private SerializedProperty customColor_5;
      private SerializedProperty customColor_6;
      private SerializedProperty customColor_7;
      private SerializedProperty customColor_8;

      private SerializedProperty shadowColor;
      private SerializedProperty customShadowColor;
      private SerializedProperty shadowTolerance;

  private void OnEnable(){

    PSC = (PixelStylizerCamera)target;
    SO = new SerializedObject(target);

    screenWidthSize = SO.FindProperty("screenWidthSize");
    pixelSize = SO.FindProperty("pixelSize");
    smoothCamera = SO.FindProperty("smoothCamera");
    stylizePixels = SO.FindProperty("stylizePixels");

    cameraType = SO.FindProperty("cameraType");
        //CAMERA STYLE
        presetStyle = SO.FindProperty("presetStyle");
        customStyleType = SO.FindProperty("customStyleType");
        //COLORS
        Color_1 = SO.FindProperty("Color_1");
        Color_2 = SO.FindProperty("Color_2");
        Color_3 = SO.FindProperty("Color_3");
        Color_4 = SO.FindProperty("Color_4");
        Color_5 = SO.FindProperty("Color_5");
        Color_6 = SO.FindProperty("Color_6");
        Color_7 = SO.FindProperty("Color_7");
        Color_8 = SO.FindProperty("Color_8");

        customColor_1 = SO.FindProperty("customColor_1");
        customColor_2 = SO.FindProperty("customColor_2");
        customColor_3 = SO.FindProperty("customColor_3");
        customColor_4 = SO.FindProperty("customColor_4");
        customColor_5 = SO.FindProperty("customColor_5");
        customColor_6 = SO.FindProperty("customColor_6");
        customColor_7 = SO.FindProperty("customColor_7");
        customColor_8 = SO.FindProperty("customColor_8");

        shadowColor = SO.FindProperty("shadowColor");
        customShadowColor = SO.FindProperty("customShadowColor");
        shadowTolerance = SO.FindProperty("shadowTolerance");

  }

  public override void OnInspectorGUI(){

    SO.Update();

    GUILayout.Space(10);

      GUILayout.Label("SCREEN", EditorStyles.boldLabel);

    GUILayout.Space(10);

      EditorGUILayout.PropertyField(screenWidthSize, new GUIContent("Screen Width Size: "));
      GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Pixel Size: ");
        pixelSize.intValue = EditorGUILayout.IntSlider(pixelSize.intValue, 1, 16);
      GUILayout.EndHorizontal();

      //The SmoothCamera option will be available only if the pixel size is 1.
      if(pixelSize.intValue == 1){
        EditorGUILayout.PropertyField(smoothCamera, new GUIContent("Smooth Camera? "));
      }else if(pixelSize.intValue > 1){
        smoothCamera.boolValue = false;
      }

    //This bool variable makes the stylizer presets to be interactable if its value is true.
    stylizePixels.boolValue = EditorGUILayout.BeginToggleGroup("Use Stylizer? ", stylizePixels.boolValue);

      GUILayout.Space(10);

        GUILayout.Label("CAMERA SETTINGS", EditorStyles.boldLabel);

      GUILayout.Space(10);

      //
      //PRESETS / CUSTOM
      //

            cameraType.intValue = GUILayout.Toolbar(cameraType.intValue, new string[]{"Presets", "Custom"});
            GUILayout.Space(10);

            switch (cameraType.intValue) {
                case 0:
                    tabPresets = 1;
                    tabCustom = 0;
                    break;
                case 1:
                    tabPresets = 0;
                    tabCustom = 1;
                    break;
            }
            if(EditorGUILayout.BeginFadeGroup(tabPresets)){
                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Style: ");
                presetStyle.intValue = EditorGUILayout.Popup(presetStyle.intValue, new string[]{"Retro Pixel", "Propaganda",
                "Neo Tokio", "Green Forest", "Autumn", "Wild West", "Noir", "Old Photo", "Galactic", "Deep Ocean", "Army"});
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                EditorGUILayout.PropertyField(Color_1, new GUIContent("Color 1: "));
                EditorGUILayout.PropertyField(Color_2, new GUIContent("Color 2: "));
                EditorGUILayout.PropertyField(Color_3, new GUIContent("Color 3: "));
                EditorGUILayout.PropertyField(Color_4, new GUIContent("Color 4: "));
                EditorGUILayout.PropertyField(Color_5, new GUIContent("Color 5: "));
                EditorGUILayout.PropertyField(Color_6, new GUIContent("Color 6: "));
                EditorGUILayout.PropertyField(Color_7, new GUIContent("Color 7: "));
                EditorGUILayout.PropertyField(Color_8, new GUIContent("Color 8: "));

                GUILayout.Space(10);

                EditorGUILayout.PropertyField(shadowColor, new GUIContent("Shadow Color: "));
                GUILayout.BeginHorizontal();
                  EditorGUILayout.LabelField("Shadow Tolerance: ");
                  shadowTolerance.floatValue = EditorGUILayout.Slider(shadowTolerance.floatValue, 0.05f, 0.6f);
                GUILayout.EndHorizontal();

            }
            EditorGUILayout.EndFadeGroup();

            if(EditorGUILayout.BeginFadeGroup(tabCustom)){
                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Number of Colors: ");
                customStyleType.intValue = GUILayout.Toolbar(customStyleType.intValue, new string[]{"2", "3", "4", "8"});
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                EditorGUILayout.PropertyField(customColor_1, new GUIContent("Color 1: "));
                EditorGUILayout.PropertyField(customColor_2, new GUIContent("Color 2: "));
                if(customStyleType.intValue == 1){
                  EditorGUILayout.PropertyField(customColor_3, new GUIContent("Color 3: "));
                }else if(customStyleType.intValue == 2){
                  EditorGUILayout.PropertyField(customColor_3, new GUIContent("Color 3: "));
                  EditorGUILayout.PropertyField(customColor_4, new GUIContent("Color 4: "));
                }else if(customStyleType.intValue == 3){
                  EditorGUILayout.PropertyField(customColor_3, new GUIContent("Color 3: "));
                  EditorGUILayout.PropertyField(customColor_4, new GUIContent("Color 4: "));
                  EditorGUILayout.PropertyField(customColor_5, new GUIContent("Color 5: "));
                  EditorGUILayout.PropertyField(customColor_6, new GUIContent("Color 6: "));
                  EditorGUILayout.PropertyField(customColor_7, new GUIContent("Color 7: "));
                  EditorGUILayout.PropertyField(customColor_8, new GUIContent("Color 8: "));
                }

                GUILayout.Space(10);

                EditorGUILayout.PropertyField(customShadowColor, new GUIContent("Shadow Color: "));
                GUILayout.BeginHorizontal();
                  EditorGUILayout.LabelField("Shadow Tolerance: ");
                  shadowTolerance.floatValue = EditorGUILayout.Slider(shadowTolerance.floatValue, 0.05f, 0.6f);
                GUILayout.EndHorizontal();

            }
            EditorGUILayout.EndFadeGroup();

            EditorGUI.indentLevel = 0;

    EditorGUILayout.EndToggleGroup();

    SO.ApplyModifiedProperties();
  }

}
