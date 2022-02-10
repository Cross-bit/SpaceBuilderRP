#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace UnityToolbarExtender.Examples
{
    static class ToolbarStyles
    {
        public static readonly GUIStyle commandButtonStyle = new GUIStyle("Command")
        {
            //fontSize = 16,
            alignment = TextAnchor.MiddleCenter,
            imagePosition = ImagePosition.ImageAbove,
            fontStyle = FontStyle.Bold,
            fixedWidth = 50f
            //normal = Color.blue;


        };

        public static readonly GUIStyle pressedButtonStyle = new GUIStyle("Command")
        {
            //fontSize = 16,
            alignment = TextAnchor.MiddleCenter,
            imagePosition = ImagePosition.ImageAbove,
            fontStyle = FontStyle.BoldAndItalic,
            fixedWidth =  50f

        };

    }

    [InitializeOnLoad]
    public class SceneSwitchLeftButton
    {
        static SceneSwitchLeftButton()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }
        public static bool pressed = true;

        static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            // S tímhle sc opatrně, nezkoušet přidat do classy!!! otrocky vypsat

            if (EditorSceneManager.GetActiveScene().name != Settings.SCREEN_UI_SCENE_NAME && EditorSceneManager.GetActiveScene().name == Settings.MAIN_SCENE_NAME)
            {
                if (GUILayout.Button(new GUIContent("SC-UI", "Hlavní UI hráče."), ToolbarStyles.commandButtonStyle))
                {
                    SceneHelper.StartScene(Settings.SCENES_PATH + (Settings.SCREEN_UI_SCENE_NAME + ".unity"));
                }

            }
            else if (EditorSceneManager.GetActiveScene().name == Settings.SCREEN_UI_SCENE_NAME) {
                EditorGUI.BeginDisabledGroup(true);
                GUI.backgroundColor = Color.blue;
                if (GUILayout.Button(new GUIContent("SC-UI", "Hlavní UI hráče.(current)"), ToolbarStyles.pressedButtonStyle))
                {
                    Debug.Log("CurrentScene!");
                }
                    EditorGUI.EndDisabledGroup();
            }

            GUI.backgroundColor = Color.white;




            if (EditorSceneManager.GetActiveScene().name != Settings.MAIN_SCENE_NAME)
            {
                if (GUILayout.Button(new GUIContent("MAIN", "Hlavní scéna hry."), ToolbarStyles.commandButtonStyle))
                {
                    SceneHelper.StartScene(Settings.SCENES_PATH + (Settings.MAIN_SCENE_NAME + ".unity"));
                }

            }
            else
            {
                EditorGUI.BeginDisabledGroup(true);
                GUI.backgroundColor = Color.blue;
                if (GUILayout.Button(new GUIContent("MAIN", "Hlavní scéna hry.(current)"), ToolbarStyles.pressedButtonStyle))
                {
                    Debug.Log("CurrentScene!");
                }
                EditorGUI.EndDisabledGroup();
            }

            GUI.backgroundColor = Color.white;

            //   AddButtonToLayoutClass addBtn = new AddButtonToLayoutClass();

            // INICIALIZAČNÍ SCÉNA --
            // AddButtonToLayout(Settings.INI_SCENE_NAME, new GUIContent("INI", "Inicializační scéna."));

            /*if (EditorSceneManager.GetActiveScene().name != Settings.INI_SCENE_NAME)
            {
                GUI.backgroundColor = Color.white;
                if (GUILayout.Button(new GUIContent("INI", "Inicializační scéna."), ToolbarStyles.commandButtonStyle))
                {
                    SceneHelper.StartScene(Settings.SCENES_PATH + (Settings.INI_SCENE_NAME + ".unity"));
                }
            }
            else
            {
                EditorGUI.BeginDisabledGroup(true);
                GUI.backgroundColor = Color.blue;
                if (GUILayout.Button(new GUIContent("INI", "Inicializační scéna."), ToolbarStyles.commandButtonStyle))
                    Debug.Log("Current Layer");
                EditorGUI.EndDisabledGroup();
            }

            // MAINMENUBTN --
            /* AddButtonToLayout(Settings.MAIN_MENU_SCENE_NAME, new GUIContent("MENU", "Main Menu scéna.."));

             // MAINMENUBTN --
             AddButtonToLayout(Settings.MAIN_SCENE_NAME, new GUIContent("MAIN", "Hlavní scéna hry."));


             // SCREEN UI --
             if (EditorSceneManager.GetActiveScene().name == Settings.MAIN_SCENE_NAME){
                 AddButtonToLayout(Settings.SCREEN_UI_SCENE_NAME, new GUIContent("SC-UI", "Hlavní UI hráče."));
             }*/


            GUI.backgroundColor = Color.white;
        }

        /* public static void AddButtonToLayout(string sceneName, GUIContent content)
         {

             if (EditorSceneManager.GetActiveScene().name != sceneName)
             {
                 GUI.backgroundColor = Color.white;
                 if (GUILayout.Button(content, ToolbarStyles.commandButtonStyle))
                 {
                     SceneHelper.StartScene(Settings.SCENES_PATH + (sceneName == Settings.MAIN_MENU_SCENE_NAME ? "MainMenu/" : "") + (sceneName + ".unity"));
                 }
             }
             else
             {
                 EditorGUI.BeginDisabledGroup(true);
                 GUI.backgroundColor = Color.blue;
                 if (GUILayout.Button(content, ToolbarStyles.commandButtonStyle))
                     Debug.Log("Current Layer");
                 EditorGUI.EndDisabledGroup();
             }
         }*/

    }

    /* class AddButtonToLayoutClass {
         public void AddButtonToLayout(string sceneName, GUIContent content)
         {

             if (EditorSceneManager.GetActiveScene().name != sceneName)
             {
                 GUI.backgroundColor = Color.white;
                 if (GUILayout.Button(content, ToolbarStyles.commandButtonStyle))
                 {
                     SceneHelper.StartScene(Settings.SCENES_PATH + (sceneName == Settings.MAIN_MENU_SCENE_NAME ? "MainMenu/" : "") + (sceneName + ".unity"));
                 }
             }
             else
             {
                 EditorGUI.BeginDisabledGroup(true);
                 GUI.backgroundColor = Color.blue;
                 if (GUILayout.Button(content, ToolbarStyles.commandButtonStyle))
                     Debug.Log("Current Layer");
                 EditorGUI.EndDisabledGroup();
             }
         }
     }*/



    static class SceneHelper
    {
        static string sceneToOpen;

        public static void StartScene(string scene)
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }

            sceneToOpen = scene;
            EditorApplication.update += OnUpdate;
        }

        static void OnUpdate()
        {
            if (sceneToOpen == null ||
                EditorApplication.isPlaying || EditorApplication.isPaused ||
                EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

            EditorApplication.update -= OnUpdate;

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(sceneToOpen);
                //EditorApplication.isPlaying = true;
            }
            sceneToOpen = null;
        }
    }
}
#endif