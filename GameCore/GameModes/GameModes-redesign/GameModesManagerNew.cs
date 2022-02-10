using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.GameCore.GameModes {

    public class GameModesManagerNew : Singleton<GameModesManagerNew> {

        public SubModesHandler subModesHandler = new SubModesHandler();

        public IGameState CurrentGameState;

        public BuildModeState BuildModeState { get; }

        public IdleModeState IdleModeState { get; }

        public GameModesManagerNew() {
            this.BuildModeState = new BuildModeState();
            this.IdleModeState = new IdleModeState();

            CurrentGameState = IdleModeState;
        }
    }


}
