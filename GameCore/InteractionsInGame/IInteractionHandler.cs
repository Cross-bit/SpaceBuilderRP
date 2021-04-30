using UnityEngine;

namespace Assets.Scripts.GameCore.InteractionsInGame
{
    public interface IInteractionHandler
    {
        RaycastHit HitData { get; }
    }
}
