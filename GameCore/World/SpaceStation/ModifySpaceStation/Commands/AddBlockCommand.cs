using System;
using UnityEngine;

namespace Assets.Scripts.GameCore.WorldBuilding.ModifyWorld
{
    public class AddBlockCommand : IModifySpaceStationCommand 
    {
        readonly SpaceStation _world;
        readonly Settings.Blocks_types _blockTypeToAdd;
        readonly BlockChecker _checkerBuildingOn;
        public SymetricBlock LastPlacedBlock { get; private set; }

        public AddBlockCommand(SpaceStation spaceStation, BlockChecker checkerBuildingOn, Settings.Blocks_types blockTypeToAdd) {
            _world = spaceStation;
            _blockTypeToAdd = blockTypeToAdd;
            _checkerBuildingOn = checkerBuildingOn;
        }

        public void ModifySpaceStation() {
            this.LastPlacedBlock = _world.CreateNewSymBlock(new Vector3(0, 0, 0),
               new Vector3(0, 0, 0), Settings.blocksTypeLibrary[_blockTypeToAdd], _checkerBuildingOn); // 
        }
    }
}
