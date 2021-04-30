using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameCore.GameModes
{
    public class SubModesHandler
    {

        public IGameMode CurrentSubMode { get; private set; }
        public IGameMode LastSubMode;

        public void SetSubMode(IGameMode subMode)
        {
            this.LastSubMode = CurrentSubMode;
            this.CurrentSubMode = subMode;
        }

        public void TurnModeOn() {
            CurrentSubMode?.TurnModeOn();

            if (CurrentSubMode == null)
                Debug.Log("CurrentSubMode není nastaven LastSubMode");
        }

        public void StopCurrentSubMode(Type T) {

            if (CurrentSubMode == null)
                return;

            if (!(CurrentSubMode.GetType() == T))
            {
                Debug.LogError($"Snaha vypnout game Mode, který je jiného typu ({T.GetType()}), než current jenž je typu: {CurrentSubMode.GetType()}");
                return;
            }

            this.CurrentSubMode?.TurnModeOff();

        }


    }
}
