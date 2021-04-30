using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameCore.Robots
{

    public class BuildRobot : Robot, IBuildRobot
    {
        public override bool followAIPath { get; set; }

        public void Build()
        {
            throw new System.NotImplementedException();
        }
    }
}
