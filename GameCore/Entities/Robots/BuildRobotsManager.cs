using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameCore.Robots;

public class BuildRobotsManager : Singleton<BuildRobotsManager>
{
    void Start(){
       // RobotsDatabase.AddRobotToDatabase(new BuildRobot() { MovementSpeed = 5, RotationSpeed = 5 });
    }

    public void CreateRobot(IRobot robotToCreate, SymetricBlock baseToCreateIn) {
        // Logika jak vytvořit robota a kde apod. TODO:
    }

    // Update is called once per frame
    void Update()
    {
        
        //for (int i = 0; i < RobotsDatabase.RobotsDatabaseList.Count; i++)
        //{
        //    if (RobotsDatabase.RobotsDatabaseList[i].followAIPath)
        //    {
        //        RobotsDatabase.RobotsDatabaseList[i].GeneratePath(Color.yellow);
        //        RobotsDatabase.RobotsDatabaseList[i].followAIPath = false;
        //    }
        //
        //    RobotsDatabase.RobotsDatabaseList[i].FollowAIPath();
        //    RobotsDatabase.RobotsDatabaseList[i].Move();
        //}

        

    }
}
