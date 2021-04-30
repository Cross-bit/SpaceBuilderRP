using Assets.Scripts.GameCore.InteractionsInGame.InteractionsWithBuildings;
using Assets.Scripts.GameCore.InteractionsInGame;
using UnityEngine;

namespace Assets.Scripts.GameCore
{


    public class InteractionsHandlersController : IInteractionHandler, IInteractionsHandlersController
    {

        public RaycastHit HitData { get; private set; }

        public bool IsInteractionValid { get; private set; }

        public InteractionsHandlersController()
        {
            this.IsInteractionValid = Helpers.CastInteractRay(out var hitData);
            this.HitData = hitData;
        }

        public void OnInteractionWithBuildings()
        {

            BaseBuildingInteraction blockInteractionHandler = null;

            // -- Kliknutí na tělo bloku (resp. budovy)
            if (HitData.transform.gameObject.layer == LayerMask.NameToLayer(Settings.BLOCK_LAYER))
                blockInteractionHandler = new BlockInteractionHandler(HitData);

            // -- Kliknutí na kontrolér
            if (HitData.transform.gameObject.layer == LayerMask.NameToLayer(Settings.CHECKER_LAYER))
                blockInteractionHandler = new CheckerInteractionHandler(HitData);

            blockInteractionHandler?.OnInteract();
        }


        public void OnInteractionWithBot()
        {
           // throw new System.NotImplementedException();
        }
    }
}
