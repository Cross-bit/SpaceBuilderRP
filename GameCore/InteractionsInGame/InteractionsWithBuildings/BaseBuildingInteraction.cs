using UnityEngine;

namespace Assets.Scripts.GameCore.InteractionsInGame.InteractionsWithBuildings
{
    public abstract class BaseBuildingInteraction : IInteractionHandler
    {
        public RaycastHit HitData { get; private set; }

        public SymBlock LastInteractedBlock { get; protected set; } // Blok(budova) se kterým interagujeme

        public BaseBuildingInteraction(RaycastHit hitData)
        {
            this.HitData = hitData;

            SetLastBlockPlayerInteractedWith();
        }

        public abstract void SetLastBlockPlayerInteractedWith();

        public abstract void OnInteract();
    }
}
