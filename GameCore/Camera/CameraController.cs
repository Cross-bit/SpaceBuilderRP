using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RTS_Cam
{
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("RTS Camera")]
    public class CameraController : MonoBehaviour
    {

        #region Foldouts

#if UNITY_EDITOR

        public int lastTab = 0;

        public bool movementSettingsFoldout;
        public bool zoomingSettingsFoldout;
        public bool rotationSettingsFoldout;
        public bool heightSettingsFoldout;
        public bool mapLimitSettingsFoldout;
        public bool targetingSettingsFoldout;
        public bool inputSettingsFoldout;

#endif

        #endregion

        private Transform m_Transform; //camera tranform
        public bool useFixedUpdate = true; //use FixedUpdate() or Update()

        #region Movement

        public float keyboardMovementSpeed = 5f; //speed with keyboard movement
        public float speedUP = 5f;
        public float screenEdgeMovementSpeed = 3f; //spee with screen edge movement
        public float followingSpeed = 5f; //speed when following a target
        public float rotationSped = 3f;
        public float panningSpeed = 10f;
        [Range (1, 2)]
        public float mouseMovementSensitivity = 10f;

        #endregion

        #region Rotace

        public float yaw;
        public float pitch;
        public float roll;
        public float x;
        public float y;
        public float z;

        public bool invertY;

        [Header("Rotation Settings")]
        [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
        public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

        #endregion

        #region Height

        public bool autoHeight = true;
        public LayerMask groundMask = -1; //layermask of ground or other objects that affect height

        public float maxHeight = 10f; //maximal height
        public float minHeight = 15f; //minimnal height
        public float heightDampening = 5f;
        public float keyboardZoomingSensitivity = 2f;
        public float scrollWheelZoomingSensitivity = 25f;

        private float zoomPos = 0; //value in range (0, 1) used as t in Matf.Lerp

        #endregion

        #region MapLimits

        public bool limitMap = true;
        public float limitX = 50f; //x limit of map
        public float limitY = 50f; //z limit of map

        #endregion

        #region Targeting


        public bool followTarget = true;
        public float targetZoomSpeed;
        public Transform targetFollow; //target to follow
        public float distanceFromTarget;
        public Vector3 targetOffset;

        public Vector3 positionGizmos;

        /// <summary>
        /// are we following target
        /// </summary>
        public bool FollowingTarget
        {
            get
            {
                return targetFollow != null;
            }
        }

        #endregion

        #region Input


        public bool useScreenEdgeInput = true;
        public float screenEdgeBorder = 25f;

        public bool useKeyboardInput = true;
        public string horizontalAxis = "Horizontal";
        public string verticalAxis = "Vertical";


        public bool usePanning = true;
        public KeyCode panningKey = KeyCode.Mouse2;

        public bool useKeyboardZooming = true;
        public KeyCode zoomInKey = KeyCode.E;
        public KeyCode zoomOutKey = KeyCode.Q;

        public bool useScrollwheelZooming = true;
        public string zoomingAxis = "Mouse ScrollWheel";

        public bool useKeyboardRotation = true;
        public KeyCode rotateRightKey = KeyCode.X;
        public KeyCode rotateLeftKey = KeyCode.Z;

        public bool useMouseRotation = true;

        private Vector2 KeyboardInput
        {
            get { return useKeyboardInput ? /*new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis))*/ InputManager.Instance.CamInputs.CameraHorizontalMove : Vector2.zero; }
        }

        private Vector2 MouseInput
        {
            get { return /*Input.mousePosition;*/ InputManager.Instance.GeneralInputs.MousePosition; }
        }

        private float ScrollWheel
        {
            get { return 0f;/* Input.GetAxis(zoomingAxis);*/ }
           // TODO:!!!
        }

        //   private Vector2 MouseAxis
        //   {
        //       get { return InputManager.Instance.MousePosition;/* new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));*/ }
        //   }

        public int zoomDirection;

      /*  public int SetZoomDirection(int direction)
        {
            return direction;
            /*get
            {*/
             /*   bool zoomIn = Input.GetKey(zoomInKey); //TODO:!!! - přes input system
                bool zoomOut = Input.GetKey(zoomOutKey);
                if (zoomIn && zoomOut)
                    return 0;
                else if (!zoomIn && zoomOut)
                    return 1;
                else if (zoomIn && !zoomOut)
                    return -1;
                else
                    return 0;*/
          //  }
      //  }

        /*private int RotationDirection
        {
            get
            {
                bool rotateRight = Input.GetKey(rotateRightKey); - přes input system !!!
                bool rotateLeft = Input.GetKey(rotateLeftKey);
                if(rotateLeft && rotateRight)
                    return 0;
                else if(rotateLeft && !rotateRight)
                    return -1;
                else if(!rotateLeft && rotateRight)
                    return 1;
                else 
                    return 0;
            }
        }*/

        #endregion

        #region Unity_Methods

        private void Start()
        {
            m_Transform = transform;
        }

        private void Update()
        {
            if (!useFixedUpdate)
                CameraUpdate();
        }

        private void FixedUpdate()
        {
            if (useFixedUpdate)
                CameraUpdate();
        }

        #endregion



        /// <summary> update camera movement and rotation </summary>
        private void CameraUpdate()
        {
            // Pokud je kamera
            if (Settings.isCamera)
            {
                if (FollowingTarget && followTarget)
                {
                    if(targetFollow != null)
                        FollowTarget();
                }
                else
                {
                    Move();
                    Rotation();
                }

                HeightCalculation();
                LimitPosition();
            }

        }


        /// <summary>move camera with keyboard or with screen edge</summary>
        private void Move()
        {
            if (useKeyboardInput)
            {
                Vector3 desiredMove = new Vector3(KeyboardInput.x, 0, KeyboardInput.y);
                
                if (InputManager.Instance.CamInputs.CameraMoveFasterButton) // Defaulně LShift
                {
                    desiredMove *= speedUP;
                }

                desiredMove *= keyboardMovementSpeed;
                desiredMove *= Time.deltaTime;
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
                desiredMove = m_Transform.InverseTransformDirection(desiredMove);
                m_Transform.Translate(desiredMove, Space.Self);
            }

            if (useScreenEdgeInput)
            {
                Vector3 desiredMove = new Vector3();

                Rect leftRect = new Rect(0, 0, screenEdgeBorder, Screen.height);
                Rect rightRect = new Rect(Screen.width - screenEdgeBorder, 0, screenEdgeBorder, Screen.height);
                Rect upRect = new Rect(0, Screen.height - screenEdgeBorder, Screen.width, screenEdgeBorder);
                Rect downRect = new Rect(0, 0, Screen.width, screenEdgeBorder);

                desiredMove.x = leftRect.Contains(MouseInput) ? -1 : rightRect.Contains(MouseInput) ? 1 : 0;
                desiredMove.z = upRect.Contains(MouseInput) ? 1 : downRect.Contains(MouseInput) ? -1 : 0;

                desiredMove *= screenEdgeMovementSpeed;
                desiredMove *= Time.deltaTime;
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
                desiredMove = m_Transform.InverseTransformDirection(desiredMove);

                m_Transform.Translate(desiredMove, Space.Self);
            }

          /*  if (usePanning && Input.GetKey(panningKey) && MouseAxis != Vector2.zero) // TODO: panning key...
            {
                Vector3 desiredMove = new Vector3(-MouseAxis.x, 0, -MouseAxis.y);

                desiredMove *= panningSpeed;
                desiredMove *= Time.deltaTime;
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
                desiredMove = m_Transform.InverseTransformDirection(desiredMove);
                m_Transform.Translate(desiredMove, Space.Self);
            }*/
        }



        /// <summary>calcualte height</summary>
        private void HeightCalculation()
        {
            float distanceToGround = DistanceToGround();
            if (useScrollwheelZooming)
                zoomPos += ScrollWheel * Time.deltaTime * scrollWheelZoomingSensitivity;
            if (useKeyboardZooming)
                zoomPos += this.zoomDirection * Time.deltaTime * keyboardZoomingSensitivity;
            //this.zoomDirection = 0;


            zoomPos = Mathf.Clamp01(zoomPos);

            float targetHeight = Mathf.Lerp(minHeight, maxHeight, zoomPos);

            float difference = 0;

            if (distanceToGround != targetHeight)
                difference = targetHeight - distanceToGround;


            //  Debug.Log(targetFollow);
            // Pokud nemáme cíl
            /*  if (targetFollow == null)
              {*/
            // Lerp pozice při scrollu
            m_Transform.position = Vector3.Lerp(m_Transform.position,
                new Vector3(m_Transform.position.x, targetHeight + difference, m_Transform.position.z), Time.deltaTime * heightDampening);
            /*   }
               else
               {

                   //  m_Transform.LookAt(targetFollow);
                   Vector3 targetPos = new Vector3(targetFollow.position.x, m_Transform.position.y, targetFollow.position.z) + targetOffset/*+ Vector3.Scale(targetOffset, Settings.GetVector3Population_(m_Transform.position));*/
            

            /*       m_Transform.position = Vector3.Lerp(m_Transform.position,
                      targetPos, zoomPos * targetZoomSpeed);
                  Vector3 targetPos = new Vector3(targetFollow.position.x, m_Transform.position.y, targetFollow.position.z) + Vector3.Scale(targetOffset, Settings.GetVector3Population_(m_Transform.position));
                     m_Transform.position = Vector3.MoveTowards(m_Transform.position, targetPos, Time.deltaTime * followingSpeed);

             }¨*/
        }


        ///<summary> rotate camera </summary>
        private void Rotation()
        {

            if (InputManager.Instance.CamInputs.CameraRotationButton)
            {
                var axisPlayerInput = InputManager.Instance.GeneralInputs.AxisInput * mouseMovementSensitivity;
                var mouseMovement = new Vector2(axisPlayerInput.x, axisPlayerInput.y * (invertY ? 1 : -1));

                var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

                yaw += mouseMovement.x * mouseSensitivityFactor;
                pitch += mouseMovement.y * mouseSensitivityFactor;
                pitch = Mathf.Clamp(pitch, -90f, 90f);
            }

            m_Transform.eulerAngles = new Vector3(pitch, yaw, roll);
        }


        /// <summary>follow targetif target != null</summary>
        private void FollowTarget()
        {

             // Vektor směru k cíli
             Vector3 targetDir = targetFollow.position - m_Transform.position;
            /*
             // Normalizujeme na 1 a získáme vektor opačný, od cíle
             Vector3 distanceVectorNormalized = targetDir.normalized * -1;
             // Posuneme na pozici cíle
             Vector3 targetPosition = (distanceVectorNormalized * distanceFromTarget) + targetFollow.position;*/

            Vector3 targetPosition = targetFollow.position * 1.5f;

            // Lerpneme na vypočítanou pozici
            m_Transform.position = Vector3.Lerp(m_Transform.position, new Vector3(targetPosition.x, m_Transform.position.y, targetPosition.z), followingSpeed);

            // Necháme, aby se kamera na objekt otočila
            Quaternion newRot = Quaternion.LookRotation(targetDir);
            m_Transform.rotation = Quaternion.Slerp(m_Transform.rotation, newRot, followingSpeed);

            // Musíme ukládat, aby nevznikl skok po přepnutí.
            pitch = m_Transform.rotation.eulerAngles.x;
            yaw = m_Transform.rotation.eulerAngles.y;
            roll = m_Transform.rotation.eulerAngles.z;
        }

        /// <summary>limit camera position</summary>
        private void LimitPosition()
        {
            if (!limitMap)
                return;

            m_Transform.position = new Vector3(Mathf.Clamp(m_Transform.position.x, -limitX, limitX),
                m_Transform.position.y,
                Mathf.Clamp(m_Transform.position.z, -limitY, limitY));
        }

        #region TARGET
        /// <summary>
        /// set the target
        /// </summary>
        /// <param name="target"></param>
        public void SetTarget(Transform target)
        {
            targetFollow = target;
        }

        /// <summary>
        /// reset the target (target is set to null)
        /// </summary>
        public void ResetTarget()
        {
            targetFollow = null;
        }

        #endregion TARGET



        /// <summary> calculate distance to ground </summary>
        private float DistanceToGround()
        {
            Ray ray = new Ray(m_Transform.position, Vector3.down * 50f);

            //Debug.DrawRay(ray.origin,ray.direction * 50f, Color.blue);

            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData, 2f)) //groundMask.value
            {
             //   Debug.Log("ashks");
                return (hitData.point - m_Transform.position).magnitude;
            }

            return 0f;
        }

    }
}