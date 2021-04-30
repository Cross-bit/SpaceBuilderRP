using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;
using Assets.Scripts.GameCore.WorldBuilding.ModifyWorld;

namespace Assets.Scripts.GameCore.GameModes
{

    public class BuildMode : IGameMode
    {
        public void TurnModeOn()
        {
            if (!Settings.isGameLoaded) return;

            if (Settings.isBuildMode) return;

            Settings.isBuildMode = true;

            // Screen highlight
            UI.ScreenHighlightState(true, Settings.ScreenHighlights.BUILD); // Zapneme highlight

            // Barevný grid podkladu
            BlockLibrary.blocksLib.ForEach(b => b.BlockGrid?.SetGridActive(true));

            UI.BuildModeElementsState(true);

        }

        public void TurnModeOff()
        {
            // Vypneme buildmode
            Settings.isBuildMode = false;

            // Musí se povolit kamera
            Manager.Instance.cameraController.ResetTarget();

            // Screen highlight
            UI.ScreenHighlightState(false);

            // Vypneme ostatní buildmode prvky
            UI.BuildModeElementsState(false);

            // Gizmos
            GizmosInGame.GridState(false);

            BlockLibrary.blocksLib.ForEach(b => b.BlockGrid?.SetGridActive(false));
        }

    }
}
