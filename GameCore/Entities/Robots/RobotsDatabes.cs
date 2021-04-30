using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.GameCore.Robots;

public abstract class RobotsDatabase
{
    // Hlavní databázový List
    public static List<IRobot> RobotsDatabaseList = new List<IRobot>();


    public static void AddRobotToDatabase(IRobot robotToAdd) => RobotsDatabaseList.Add(robotToAdd);

    public static void RemoveRobotFromDatabase(IRobot robotToRemove) => RobotsDatabaseList.Add(robotToRemove);

    public static IRobot GetRobotByName(string nameToSearchFor) {
        foreach (IRobot robot in RobotsDatabaseList) {
            if (robot.RobotName == nameToSearchFor)
                return robot;
        }

        Debug.Log("Nebyl nalezen robot s daným jménem");
        return null;
    }

}

