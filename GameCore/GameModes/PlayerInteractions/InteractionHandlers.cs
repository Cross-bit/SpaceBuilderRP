using Assets.Scripts.GameCore.InteractionsInGame.InteractionsWithBuildings;
using Assets.Scripts.GameCore.InteractionsInGame;
using UnityEngine;

namespace Assets.Scripts.GameCore
{


    public class PlayerInteractionsHandler : IInteractionHandler
    {

        public RaycastHit HitData { get; private set; }

        public bool IsInteractionValid { get; private set; }

        public PlayerInteractionsHandler()
        {
            this.IsInteractionValid = Helpers.CastInteractRay(out var hitData);
            this.HitData = hitData;
        }

        public void SpaceStationInteract() {

            BaseBuildingInteraction blockInteractionHandler = null;

            // -- Kliknutí na tělo bloku (resp. budovy)
            if (HitData.transform.gameObject.layer == LayerMask.NameToLayer(Settings.BLOCK_LAYER))
                blockInteractionHandler = new BlockInteractionHandler(HitData);

            // -- Kliknutí na kontrolér
            if (HitData.transform.gameObject.layer == LayerMask.NameToLayer(Settings.CHECKER_LAYER))
                blockInteractionHandler = new CheckerInteractionHandler(HitData);

            blockInteractionHandler?.SpaceStationInteract();
        }

        public void OnInteractionWithBot()
        {
           // throw new System.NotImplementedException();
        }
    }
}
