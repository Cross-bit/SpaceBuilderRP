using UnityEngine;

namespace Assets.Scripts.GameCore.InteractionsInGame.InteractionsWithBuildings
{
    public class BlockInteractionHandler : BaseBuildingInteraction
    {
        public BlockInteractionHandler(RaycastHit hitData) : base(hitData) { }

        public override void SetLastBlockPlayerInteractedWith()
        {
            base.LastInteractedBlock = Helpers.GetBlock(HitData.transform.position);
        }

        public override void OnInteract()
        {
            if (base.LastInteractedBlock == null) return;

            if (base.LastInteractedBlock.isBlockBuilded)
            {
                // Zapneme okno bloku
                UI.BlockDetailWindowState(true, this.LastInteractedBlock);
                AudioManager.Instance.Play(Settings.SoundEffects.mouse_click_1);
            }
        }

    }
}
