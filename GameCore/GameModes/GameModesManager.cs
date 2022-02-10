using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.GameCore.GameModes
{
    //[Obsolete("This class should not be used anymore!", true)]
    public class GameModesManager : Singleton<GameModesManager> // Kind of state Machine
    {        
        public IGameMode CurrentGameMode { get; private set; }
        public IGameMode LastGameMode;

        public SubModesHandler subModesHandler = new SubModesHandler(); // Možná v budoucnu udělat generické pro n vnoření (n submodů)

        /*public void SetGameMode(IGameMode gameModeToSet) {
            this.LastGameMode = CurrentGameMode;
            this.CurrentGameMode = gameModeToSet;
        }

        public void TurnModeOn() {
            this.CurrentGameMode?.TurnModeOn();
        }*/

        /*public void StopCurrentGameMode<T>() {

            if (this.CurrentGameMode == null) {
                Debug.LogError("Neexistuje current GameMode");
                return;
            }

            if (!(CurrentGameMode is T)) {
                Debug.LogError($"Snaha vypnout game Mode, který je jiného typu ({typeof(T)}), než current jenž je typu: {CurrentGameMode.GetType()}");
                return;
            }

            if (subModesHandler.CurrentSubMode != null) {
                var subModeType = this.subModesHandler?.CurrentSubMode.GetType(); // TODO Fixnout při buildu přes btn
                this.subModesHandler.StopCurrentSubMode(subModeType);
            }
                this.CurrentGameMode?.TurnModeOff();
        }*/

    }
}
