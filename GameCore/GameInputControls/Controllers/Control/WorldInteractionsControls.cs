using Context = UnityEngine.InputSystem.InputAction.CallbackContext;
using Assets.Scripts.GameCore.InteractionsInGame.InteractionsWithBuildings;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Assets.Scripts.GameCore.GameControls.Controllers
{
    public class WorldInteractionsControls : IControlsInput
    {
        public void RegisterInputs(InputMaster inputSystemAssetScript)
        {
            inputSystemAssetScript.General.Interact.performed += CallInteractionWithWorld;
            inputSystemAssetScript.General.CancelOperationOrPauseGame.performed += CallTerminateBuild;
        }

        public void UnRegisterInputs(InputMaster inputSystemAssetScript)
        {
            //throw new NotImplementedException();
        }

        private void CallInteractionWithWorld(Context ctx)
        {

            if (ctx.ReadValue<float>() > .25f) {

                var worldInteractionHandler = new InteractionsHandlersController();
                if(worldInteractionHandler.IsInteractionValid)
                    worldInteractionHandler.OnInteractionWithBuildings();
            }
        }

        private void CallTerminateBuild(Context ctx)
        {
            if (ctx.ReadValue<float>() > .25f)
                WorldBuilderManager.Instance.TurnBuildModeAndSubModeOffRecursive();
        }
    }
}
