using UnityEngine;

namespace Assets.Scripts.BlockScripts.BlockAdditional
{
    public interface IBlockGrid
    {
        void SetGridActive(bool setTo = false);
        void SetGridColor(Color g_col);
        void SetGridOrientation();
        void SetGridPosition();
        void SetGridRotation();
        void SetGridScale();
    }
}