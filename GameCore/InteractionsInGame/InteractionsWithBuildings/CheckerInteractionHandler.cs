using UnityEngine;

namespace Assets.Scripts.GameCore.InteractionsInGame.InteractionsWithBuildings
{
    public class CheckerInteractionHandler : BaseBuildingInteraction, ICheckerInteractionHandler
    {
        public BlockChecker _lastActiveChecker { get; private set; }

        public CheckerInteractionHandler(RaycastHit hitData) : base (hitData){
            SetLastActiveChecker();
        }

        public override void SetLastBlockPlayerInteractedWith()
        {
            GameObject checker_obj = HitData.transform.gameObject; // objekt(grafika checkeru) na kterou jsme klikli
            Vector3 checkers_pos = checker_obj.transform.parent.gameObject.transform.position; //!!! GLOBÁLNÍ POZICE Bloku (GameObjektu)!!!
            base.LastInteractedBlock = Helpers.GetBlock(checkers_pos);
        }

        public void SetLastActiveChecker() => _lastActiveChecker = Helpers.GetLastActiveChecker(base.LastInteractedBlock, base.HitData.transform.position);


        public override void OnInteract()
        {
            WorldBuilderManager.Instance.TurnBuildModeAndSubModeOffRecursive();

            if (this.LastInteractedBlock == null) return;

            WorldBuilderManager.Instance.TurnBuildModeOn(_lastActiveChecker); // Pošleme na jakém checkeru chceme stavět

            // Zvuk po kliknutí na kontrolér
            AudioManager.Instance.Play(Settings.SoundEffects.mouse_click_1);
        }

    }
}
