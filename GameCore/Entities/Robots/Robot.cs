using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameCore.Robots
{

    public abstract class Robot : IRobot
    {
        public string RobotName { get; set; }

        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
        public Transform RobotTransform { get => robotTransform; set => robotTransform = value; }


        [Header("Hledání cíle:")]
        public Transform Target;
        public float DetectionDist = 5f;
        public float RaysOffset = 1f;
        public AIPathGenerator path;

        private Transform robotTransform;
        private float movementSpeed;
        private float rotationSpeed;

        public abstract bool followAIPath { get; set; }

        public void SetDefaultValues()
        {
            // Pokud jsou null Tak nastav
            if (movementSpeed == 0f) movementSpeed = 5f;
            if (rotationSpeed == 0f) rotationSpeed = 5f;
            if (DetectionDist == 0f) DetectionDist = 10f;
            if (RaysOffset == 0f) RaysOffset = 5f;
        }

        public void Turn(Transform target)
        {
            Vector3 pos = target.position - robotTransform.position;
            Quaternion rotation = Quaternion.LookRotation(pos);
            robotTransform.rotation = Quaternion.Slerp(robotTransform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        public void Move()
        {
            if (this.robotTransform == null){
                Debug.LogWarning("Robot nemá přiřazenou transform.");
                return;
            }
                

            robotTransform.position += robotTransform.forward * MovementSpeed * Time.deltaTime;
        }

        public void GeneratePath(Color pathColor) // why....
        {
            path = new AIPathGenerator(pathColor, robotTransform.position, Target, 4f);
            path.GeneratePath();

        }

        public void FollowAIPath()
        {
            if (path != null)
            {
                for (int i = 0; i < path.allMovePoints.Count; i++)
                {
                    if (path.allMovePoints.Count > i + 1)
                        Debug.DrawLine(path.allMovePoints[i].transform.position, path.allMovePoints[i + 1].transform.position, Color.red);

                }

                if (path.isPathReady)
                {
                    if (robotTransform.position != path.currentPoint.position)
                    {
                        // Pohyb směrem k bodu
                        robotTransform.position = Vector3.MoveTowards(robotTransform.position, path.currentPoint.position, movementSpeed * Time.deltaTime);
                        //Move();
                        //Turn(path.currentPoint);

                        // Rotace look AT
                        Vector3 targetDir = path.currentPoint.position - robotTransform.position;
                        Quaternion newRot = Quaternion.LookRotation(targetDir);
                        robotTransform.rotation = Quaternion.Slerp(robotTransform.rotation, newRot, 5f * Time.deltaTime);
                    }
                    else
                    {
                        path.SetNewPoint();
                    }
                }
                else
                {
                    GameObject.Destroy(path.pathHolder.gameObject);
                    path = null;
                }
            }
        }

        public void PathFinding()
        {
            /*RaycastHit hit;
            Vector3 raycastOffset = Vector3.zero;

            Vector3 left_ray = transform.position - transform.right * raysOffset;
            Vector3 right_ray = transform.position + transform.right * raysOffset;
            Vector3 up_ray = transform.position + transform.up * raysOffset;
            Vector3 down_ray = transform.position - transform.up * raysOffset;

            Debug.DrawRay(left_ray, transform.forward * detectionDist, Color.red);
            Debug.DrawRay(right_ray, transform.forward * detectionDist, Color.yellow);
            Debug.DrawRay(up_ray, transform.forward * detectionDist, Color.blue);
            Debug.DrawRay(down_ray, transform.forward * detectionDist, Color.green);

            if (Physics.Raycast(right_ray, transform.forward, out hit, detectionDist))
            {
                raycastOffset -= transform.right;
                Debug.Log(hit.transform.gameObject.name +""+ raycastOffset);
            }
            else if (Physics.Raycast(left_ray, transform.forward, out hit, detectionDist))
            {
                raycastOffset += transform.right;
                Debug.Log(hit.transform.gameObject.name + "" + raycastOffset);
            }

            if (Physics.Raycast(up_ray, transform.forward, out hit, detectionDist))
            {
                raycastOffset -= transform.up;
                Debug.Log(hit.transform.gameObject.name + "" + raycastOffset);
            }
            else if (Physics.Raycast(down_ray, transform.forward, out hit, detectionDist))
            {
                raycastOffset += transform.up;
                Debug.Log(hit.transform.gameObject.name + "" + raycastOffset);
            }


            if (raycastOffset != Vector3.zero)
            {
                //transform.Rotate(raycastOffset * 500f * Time.deltaTime);

                transform.position += raycastOffset * Time.deltaTime;//Vector3.MoveTowards(transform.position,  raycastOffset, Time.deltaTime);

                Debug.Log(raycastOffset);
                // Move();
                //Debug.Log("B" + transform.position);
            }
            else
            {
                //Debug.Log("A" + transform.position);
               // Turn();
                transform.position = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);

                transform.LookAt(target);
            }*/
        }
    }

}
