using System;
using UnityEngine;

namespace Assets.Scripts.GameCore.WorldBuilding.ModifyWorld
{
    public class AddBlockToWorldAction : IModifyWorldAction 
    {
        readonly SpaceStation _world;
        readonly Settings.Blocks_types _blockTypeToAdd;
        readonly BlockChecker _checkerBuildingOn; // Last active checker
        public SymetricBlock LastPlacedBlock { get; private set; }

        public AddBlockToWorldAction(SpaceStation world, BlockChecker checkerBuildingOn, Settings.Blocks_types blockTypeToAdd) {
            _world = world;
            _blockTypeToAdd = blockTypeToAdd;
            _checkerBuildingOn = checkerBuildingOn;
        }

        public void ModifyTheWorld() {
            this.LastPlacedBlock = _world.CreateNewSymBlock(new Vector3(0, 0, 0),
               new Vector3(0, 0, 0), Settings.blocksTypeLibrary[_blockTypeToAdd], _checkerBuildingOn); // 
        }
    }
}
