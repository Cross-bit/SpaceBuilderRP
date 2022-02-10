using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;
using Assets.Scripts.GameCore.GameModes;

public class BlockBuildCart
{
    // (comment 2022)ok chápejme tohle tedy jako model té kartičky ok
    public RectTransform CartGraphics { get; set; }
    public Settings.Blocks_types BlockType { get; set; }

    public BlockSO BlockData {get; set;}
    public Settings.Checkers_types CheckerType { get; set; }

    public BlockBuildCart(RectTransform cart_g) {
        this.CartGraphics = cart_g;
    }
}
