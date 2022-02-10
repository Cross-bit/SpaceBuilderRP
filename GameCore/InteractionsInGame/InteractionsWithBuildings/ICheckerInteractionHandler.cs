namespace Assets.Scripts.GameCore.InteractionsInGame.InteractionsWithBuildings
{
    public interface ICheckerInteractionHandler {
        BlockChecker _lastActiveChecker { get; }
        void SetLastActiveChecker();
    }
}
