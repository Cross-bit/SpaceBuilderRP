using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
namespace Assets.Scripts.GameCore.GameModes
{
    public class BuildSubModeDemolish : IGameMode
    {
        private readonly ModifyWorldActionHandler modifyWorldActionHandler = new ModifyWorldActionHandler();

        public void RemoveBlockFromWorld(SymBlock blockToRemove)
        {
            if (Settings.isBuildMode)
                this.modifyWorldActionHandler.ModifyWorld(new RemoveBlockFromTheWorldAction(blockToRemove));
        }
        


        public void TurnModeOff()
        {
            // todo
        }

        public void TurnModeOn()
        {
            // Todo
        }
    }
}
