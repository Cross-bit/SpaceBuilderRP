using UnityEngine;
using UnityEngine.InputSystem;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Assets.Scripts.GameCore.GameControls.Controllers
{
    public class GeneralInputControls : IGeneralInputControls
    {
        public Vector2 MousePosition { get; private set; }

        public Vector2 AxisInput { get; private set; }

        public bool IsPlayerInteracting { get; private set; }

        public bool WasCancleOperationBtnTriggered { get; private set; } 

        //de/registrace Inputů

        public void RegisterInputs(InputMaster inputSystemAssetScript){
            inputSystemAssetScript.General.Interact.performed += SetPlayerInteraction;
            inputSystemAssetScript.General.MousePosition.performed += SetMousePosition;
            inputSystemAssetScript.General.AxisInput.performed += SetAxisInput;
            inputSystemAssetScript.General.CancelOperationOrPauseGame.performed += SetCancleOperation;
        }

        public void UnregisterInputs(InputMaster inputSystemAssetScript){
            inputSystemAssetScript.General.Interact.performed -= SetPlayerInteraction;
            inputSystemAssetScript.General.MousePosition.performed -= SetMousePosition;
            inputSystemAssetScript.General.AxisInput.performed -= SetAxisInput;
            inputSystemAssetScript.General.CancelOperationOrPauseGame.performed -= SetCancleOperation;
        }

        // Čtení inputů

        private void SetPlayerInteraction(Context ctx) => this.IsPlayerInteracting = ctx.ReadValue<float>() > .25f;

        private void SetMousePosition(Context ctx) => this.MousePosition = ctx.ReadValue<Vector2>();

        private void SetAxisInput(Context ctx) => this.AxisInput = ctx.ReadValue<Vector2>() / 10f;

        private void SetCancleOperation(Context ctx) => this.WasCancleOperationBtnTriggered = ctx.ReadValue<float>() > .25f;

    }
}
