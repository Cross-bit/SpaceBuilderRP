using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameCore.GameModes
{
    public class IdleModeState : IGameState
    {
        public void TurnOnBuildMode() {

            if (!Settings.isGameLoaded) return;

            // Screen highlight
            UI.ScreenHighlightState(true, Settings.ScreenHighlights.BUILD); // Zapneme highlight

            // Barevný grid podkladu
            BlockLibrary.blocksLib.ForEach(b => b.BlockGrid?.SetGridActive(true));

            UI.BuildModeElementsState(true);

            GameModesManagerNew.Instance.CurrentGameState = GameModesManagerNew.Instance.BuildModeState;
        }

        public void TurnOnIndleMode() {
            return;
        }
    }
}
