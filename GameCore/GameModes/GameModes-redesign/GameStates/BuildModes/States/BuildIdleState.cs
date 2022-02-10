using Assets.Scripts.GameCore.UISystems.AskDialogueWindow;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameCore.GameModes
{
    public class BuildIdleState : IBuildState {
        public void TurnOnBuildIdleState() {
            return;
        }

        public void TurnOnBlockDeleteState()
        {
            throw new NotImplementedException();
        }

        public void TurnOnBlockEditState()
        {
            throw new NotImplementedException();
        }

        public void TurnOnBlockPlaceingState() {

        }

        public void TurnOnBlockPlacingState(BlockChecker checkerToBuildOn = null)
        {

            // vypneme knihovnu bloků
            UI.BlockLibraryWindowState(false); // Proč?? Nechceme vidět load knihovny (v případěm že by karet bylo víc tak by to mohlo bugovat a to by bylo zlé... )

            if (checkerToBuildOn != null) {
                // Nastavíme kameru
                Manager.Instance.cameraController.SetTarget(checkerToBuildOn.CheckerTransform);

                // Změna scény při kliknutí na checker - zvuk/zvýraznění
                checkerToBuildOn.checkers_graphics.sharedMaterial = Helpers.GameHighlights(Settings.GameHighlights.CHECKER, checkerToBuildOn.checkers_graphics);

                // Otevřeme UI Knihovnu bloků (v hře)
                UI.BlockLibraryWindowState(true, checkerToBuildOn.checkerType); // Pošleme pouze typ posledního bloku

                // Barevný grid podkladu
                BlockLibrary.blocksLib.ForEach(b => b.BlockGrid?.SetGridActive(true));
            }

           // _dialogWindow = new ADWPlaceModeModul(ModifyWorldActionHandler); todo: ?
        }

        public void TurnOnBuildIdleState(BlockChecker checkerToBuildOn)
        {
            throw new NotImplementedException();
        }
    }
}
