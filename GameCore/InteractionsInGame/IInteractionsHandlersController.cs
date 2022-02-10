using Assets.Scripts.GameCore.InteractionsInGame.InteractionsWithBuildings;

namespace Assets.Scripts.GameCore
{
    public interface IInteractionsHandlersController
    {
        bool IsInteractionValid { get; }

        void OnInteractionWithBot();
    }
}