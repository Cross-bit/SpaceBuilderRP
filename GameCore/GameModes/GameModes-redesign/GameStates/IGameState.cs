﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameCore.GameModes
{
    public interface IGameState {
        public void TurnOnIndleMode();
        public void TurnOnBuildMode();
    }
}
