using System;
using UnityEngine;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Assets.Scripts.GameCore.GameControls.Controllers
{
    [Serializable]
    public class CameraInputControls : ICameraInputControls
    {
        public Vector2 CameraHorizontalMove { get; private set; } // Default WASD/Arrows

        public bool CameraMoveFasterButton { get; private set; }  // Def. LShift

        public bool CameraRotationButton { get; private set; } // Def. RMouseButton


        //de/registrace Inputů

        public void RegisterInputs(InputMaster inputSystemAssetScript){
            inputSystemAssetScript.Player.MoveHorizontaly.performed += SetCameraHorizontalMove;
            inputSystemAssetScript.Player.SpeedUpCamera.performed += SetCameraMoveFasterButton;
            inputSystemAssetScript.Player.CameraRotationKey.performed += SetCameraRotationButton;
            inputSystemAssetScript.Player.MoveVerticaly.performed += CameraMoveVerticaly;
            inputSystemAssetScript.Player.Scroll.performed += CameraMoveVerticalyOnScroll;
        }


        public void UnRegisterInputs(InputMaster inputSystemAssetScript){
            inputSystemAssetScript.Player.MoveHorizontaly.performed -= SetCameraHorizontalMove;
            inputSystemAssetScript.Player.SpeedUpCamera.performed -= SetCameraMoveFasterButton;
            inputSystemAssetScript.Player.CameraRotationKey.performed -= SetCameraRotationButton;
        }

        // Naslouchání inputům kamery

        private void SetCameraHorizontalMove(Context ctx) => this.CameraHorizontalMove = ctx.ReadValue<Vector2>();

        private void SetCameraMoveFasterButton(Context ctx) => this.CameraMoveFasterButton = ctx.ReadValue<float>() > .5f;

        private void SetCameraRotationButton(Context ctx) => this.CameraRotationButton = ctx.ReadValue<float>() > .5f;

        private void CameraMoveVerticaly(Context ctx) => Manager.Instance.cameraController.zoomDirection = -(int) ctx.ReadValue<float>();

        private void CameraMoveVerticalyOnScroll(Context ctx) => 
            Manager.Instance.cameraController.zoomDirection = (int)Mathf.Clamp(ctx.ReadValue<Vector2>().y, -1 , 1) * 7;

    }
}
