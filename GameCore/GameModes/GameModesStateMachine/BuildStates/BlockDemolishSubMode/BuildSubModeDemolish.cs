using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
namespace Assets.Scripts.GameCore.GameModes
{
    public class BuildSubModeDemolish
    {
        private readonly SpaceStationModificator modifyWorldActionHandler = new SpaceStationModificator();

        public void RemoveBlockFromWorld(SymetricBlock blockToRemove)
        {
            //if (Settings.isBuildMode)
                this.modifyWorldActionHandler.ModifySpaceStation(new RemoveBlockCommand(blockToRemove));
        }

       
    }
}
