using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
using Assets.Scripts.GameCore.GameModes;

public class BlockBuildCart
{

    public RectTransform CartGraphics { get; set; }
    public Settings.Blocks_types BlockType { get; set; }

    public BlockSO BlockData {get; set;}
    public Settings.Checkers_types CheckerType { get; set; }

    public BlockBuildCart(RectTransform cart_g)
    {
        this.CartGraphics = cart_g;
    }


    public void LoadBlockBuild(){
        var subMode = GameModesManager.Instance.subModesHandler.CurrentSubMode as BuildSubModePlace;
        subMode.PlaceBlock(this.BlockType);

        Helpers.ReorganiseSuitableBlocks(BlockData);
    }
}
