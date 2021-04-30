using UnityEngine;

namespace Assets.Scripts.GameCore.Robots
{
    public interface IRobot
    {
        string RobotName { get; set; }
        float MovementSpeed { get; set; }
        Transform RobotTransform { get; set; }
        float RotationSpeed { get; set; }
        bool followAIPath { get; set; }

        void FollowAIPath();
        void GeneratePath(Color pathColor);
        void Move();
        void SetDefaultValues();
        void PathFinding();
        void Turn(Transform target);
    }
}