using System;
using System.Collections.Generic;
using UnityEngine;
public class BlockBuildCard
{
    // so this is block cards model

    public RectTransform CartGraphics { get; set; }
    public Settings.Blocks_types BlockType { get; set; }

    public BlockSO BlockData {get; set;}
    public Settings.Checkers_types CheckerType { get; set; }

    public event EventHandler ClickedCard;

    public BlockBuildCard(RectTransform card_g) {
        this.CartGraphics = card_g;
    }

    public void CardClicked() {
        OnBlockSelected();
    }

    protected virtual void OnBlockSelected() {
        ClickedCard?.Invoke(this, new BlockCardEventArgs(this.BlockType));
    }
}

    public class BlockCardEventArgs : EventArgs {

        public readonly Settings.Blocks_types  BlockType;
        public BlockCardEventArgs(Settings.Blocks_types blockType) {
            BlockType = blockType;
        }
    }
