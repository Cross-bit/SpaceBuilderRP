using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
namespace Assets.Scripts.GameCore.GameModes
{
    public class BuildSubModeDemolish
    {
        private readonly ModifyWorldActionHandler modifyWorldActionHandler = new ModifyWorldActionHandler();

        public void RemoveBlockFromWorld(SymetricBlock blockToRemove)
        {
            //if (Settings.isBuildMode)
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
