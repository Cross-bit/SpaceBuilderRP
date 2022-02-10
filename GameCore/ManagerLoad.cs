using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RTS_Cam;

namespace Assets.Scripts.ManagerLoad
{
    public static class ManagerLoad
    {
        public static bool LoadScreenUIScene() {
          
            bool loaded = Settings.LoadSceneAdditive(Settings.SCREEN_UI_SCENE_NAME);

            if (!loaded)
                Debug.Log("Nemohla být vytvořena scéna Screen UI!!!(Manager.cs) X-|");
            
            return loaded;
        }

        public static bool LoadWorldBuilder()
        {
            bool loaded = false;

            World worldBuiler = MonoBehaviour.FindObjectOfType<World>();

            if (worldBuiler == null)
            {
                GameObject worldBuilder_obj = new GameObject("WorldBuilder");
                worldBuiler = worldBuilder_obj.AddComponent<World>();
                Debug.LogError("WorlBuilder musel být vytvořen kódem!(Manager.cs) :-|");
            }

            if (worldBuiler != null)
                loaded = true;

            return loaded;
        }


        public static bool LoadCamera()
        {
            // --- Kamera --- 

           // Camera mainCamera = Manager.Instance.mainCamera;

            if (Manager.Instance.mainCamera != null) {
                if(Manager.Instance.mainCamera.GetComponent<CameraController>() != null){
                    return true; // Tohle by se nemělo nikdy stát, ale je to ideální případ (téměř ještě by mělo dojít k ověření/přepsání settings (rychlost pohybu ...))
                }
            }

            if (Camera.main != null)
            {
                Manager.Instance.mainCamera = Camera.main;
            }
            else {
                GameObject camObject = new GameObject("MainCamera");
                Manager.Instance.mainCamera = camObject.AddComponent<Camera>();
            }

            if (Manager.Instance.mainCamera == null) {
                return false;
            }

            // --- CameraController ---

            /*CameraController cameraController =*/

            if (Manager.Instance.cameraController == null)
            {
                // Pokud není přiřazena
                Manager.Instance.cameraController = Manager.Instance.mainCamera.gameObject.GetComponent<CameraController>();

                // Pokud je stále null
                if (Manager.Instance.cameraController == null) // Vytvoříme
                    Manager.Instance.cameraController = Manager.Instance.mainCamera.gameObject.AddComponent<CameraController>();
            }

            if (Manager.Instance.cameraController == null){
                return false;
            }

            // Nastavíme enabled
            Manager.Instance.cameraController.enabled = Settings.useCameraController;

            return true;
        }

        public static bool RemoveOtherCameras() {

            Camera[] allSceneCameras = MonoBehaviour.FindObjectsOfType<Camera>();
            // Pokud se nejedná o hlavní (main) kameru, tak se jí zbavíme
            foreach (Camera cam in allSceneCameras)
            {
                if (cam != Manager.Instance.mainCamera){
                    MonoBehaviour.Destroy(cam.gameObject);
                }
            }

            return true;
        }

        internal static bool LoadGizmos()
        {

            #region GRID
            Manager.Instance.grid = Resources.Load<GameObject>(Settings.PREFABS_CODE_LOADED + "/grid");

            if (Manager.Instance.grid == null)
                return false;

            // - GIZMOS
            //Grid systém
            if (Manager.Instance.grid != null){
                GizmosInGame.grid = (GameObject) MonoBehaviour.Instantiate(Manager.Instance.grid);
                GizmosInGame.grid.transform.position = Manager.Instance.worldPosition;
                GizmosInGame.grid.SetActive(false); // TODO:WTF Možná load z uložiště ??? - co to zanamená uložiště moje minulé já??
            }
            #endregion

            return true;

        }
    }
}
