﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameCore.WorldBuilding.ModifyWorld
{
    public interface IModifyWorldAction
    {
    //    World world { get; }

        void ModifyTheWorld();
    }
}
