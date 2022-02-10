using Context = UnityEngine.InputSystem.InputAction.CallbackContext;
using Assets.Scripts.GameCore.InteractionsInGame.InteractionsWithBuildings;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace Assets.Scripts.GameCore.GameControls.Controllers
{
    public class PlayerInteractionsControls : IControlsInput
    {
        public event EventHandler PlayerInteracted;

        public void RegisterInputs(InputMaster inputSystemAssetScript) {
            inputSystemAssetScript.General.Interact.performed += CallInteractionWithWorld;
            inputSystemAssetScript.General.CancelOperationOrPauseGame.performed += CallTerminateBuild;
        }

        public void UnregisterInputs(InputMaster inputSystemAssetScript) {
            
        }

        private void CallInteractionWithWorld(Context ctx) {


            if (ctx.ReadValue<float>() > .25f) {
                PlayerInteracted?.Invoke(this, new InteractionData(InteractionData.ActionType.MOUSE_L_ACTION));

                /*var worldInteractionHandler = new InteractionsHandlersController();
                if (worldInteractionHandler.IsInteractionValid)
                    worldInteractionHandler.OnInteract();*/
            }
        }

        private void CallTerminateBuild(Context ctx) {
            if (ctx.ReadValue<float>() > .25f)
                PlayerInteracted?.Invoke(this, new InteractionData(InteractionData.ActionType.CANCLE_ACTION));
                //BuildController.Instance.TurnBuildModeOff();
        }
    }

    public class InteractionData : EventArgs {
        public enum ActionType { CANCLE_ACTION, MOUSE_L_ACTION }

        public readonly ActionType ActionPerformed;

        public InteractionData(ActionType interactionType) {
            ActionPerformed = interactionType;
        }
    }
}
