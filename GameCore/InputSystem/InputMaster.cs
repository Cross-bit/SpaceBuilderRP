// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputSystem/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""91544acf-cfc7-4e13-8111-c8cd0311ff73"",
            ""actions"": [
                {
                    ""name"": ""MoveHorizontaly"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4247a524-b75b-4337-8758-b9bddbc0b971"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpeedUpCamera"",
                    ""type"": ""Button"",
                    ""id"": ""cd538b92-afb4-47e2-8a4d-c8fa6e2236c6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraRotationKey"",
                    ""type"": ""Button"",
                    ""id"": ""98930943-cdfe-4f0e-a1c8-f484a5e26aff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""MoveVerticaly"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d8bea0f2-6fd0-4113-a5a8-c7a537354632"",
                    ""expectedControlType"": ""Digital"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ba4cb955-0be5-4e10-90ab-19bfaee04c75"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""3b4de836-10ea-4a23-adc1-d035ba6e868b"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0e8fca37-44bf-4c8f-b3a0-bd6f370ae985"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""330bc26c-3b8c-4304-9e8f-d01c4920e89a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""211231bb-352a-40c4-8524-bb98218eb1f7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7f17b6ec-20c8-4586-b7ad-16046f8507a6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""176f7156-1721-44fb-bd05-f8d645435afc"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c82f79b5-2c10-437e-af35-776287570935"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b6e40300-9469-442b-ba02-0ad6627b8cb3"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""785a9de6-e6cd-4f8e-b84d-67487fa24055"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""40215972-7d0c-4aed-ad95-1e654af5f19a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveHorizontaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ca9d5480-97d3-4aab-b789-051340d92f8f"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""SpeedUpCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d87ecaf-d190-4168-bf0c-d095f216a63c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraRotationKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Move"",
                    ""id"": ""c444a975-f8d1-4127-a04f-8a2b273221fc"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVerticaly"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f0e2d977-70d9-4712-b81c-090e9578f060"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveVerticaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""df614d1f-9a62-4e12-b474-1df3da4ab922"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveVerticaly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""08e2fc19-58c9-426b-b651-20eb03fbe3ef"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""General"",
            ""id"": ""d7621c5e-88e8-4b4a-870a-e3cc9d9d4ecb"",
            ""actions"": [
                {
                    ""name"": ""CancelOperationOrPauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""903fdc52-b40b-4462-a6c9-1199371c755f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a22051f1-11b9-4083-abb9-403c00b7251a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AxisInput"",
                    ""type"": ""PassThrough"",
                    ""id"": ""34925a6e-0f0a-45d5-9272-dc43a537a9db"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d148215e-b87b-4992-a56f-1b814b066dd7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""43d195ca-d320-4621-bf75-77e2411b4272"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CancelOperationOrPauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4de24326-befb-4e09-b71f-c5ee0f588da7"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f15a78b7-9006-403f-963d-eaf331485fe5"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""AxisInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b3e3b7b-cb07-45c4-a764-cefeebbfba01"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI_window"",
            ""id"": ""5ff15b26-0043-4f18-9af7-ef04b5aefe15"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""aa02731c-f675-4514-a89e-b91c31ecf808"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CloseWindow"",
                    ""type"": ""Button"",
                    ""id"": ""378b1112-ee4a-4dcb-8675-e7e9046b90d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""dcf108a2-f93b-4016-8ac3-1349222eff1a"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5331dc8-9a9e-4e21-a0bc-1ea00899ccf1"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CloseWindow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""arrowKeys"",
                    ""id"": ""525399b9-a741-448c-9d64-8879464561a8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseWindow"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""601dfc13-0760-4f1f-ae8d-b0e18a52ca0a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CloseWindow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""70f3c3e7-ec89-4358-9714-64018906510d"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CloseWindow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7c33b9d5-4e50-408c-84d5-95e62cf1e1ae"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CloseWindow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9f7c3c21-fd48-4673-9b11-6064186d4929"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CloseWindow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""wasdKeys"",
                    ""id"": ""5d75a73c-b7be-4a4f-b668-181c15ef3ffd"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CloseWindow"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a6c3fe55-119d-4450-af7f-302008a200c7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CloseWindow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e1893c2d-7e4c-4da1-8dbf-da89c56178ba"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CloseWindow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4b44d2f1-8bcd-4ddb-8a98-74a18816c838"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CloseWindow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""594afea6-19f1-42dd-a230-3b85425b478b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CloseWindow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_MoveHorizontaly = m_Player.FindAction("MoveHorizontaly", throwIfNotFound: true);
        m_Player_SpeedUpCamera = m_Player.FindAction("SpeedUpCamera", throwIfNotFound: true);
        m_Player_CameraRotationKey = m_Player.FindAction("CameraRotationKey", throwIfNotFound: true);
        m_Player_MoveVerticaly = m_Player.FindAction("MoveVerticaly", throwIfNotFound: true);
        m_Player_Scroll = m_Player.FindAction("Scroll", throwIfNotFound: true);
        // General
        m_General = asset.FindActionMap("General", throwIfNotFound: true);
        m_General_CancelOperationOrPauseGame = m_General.FindAction("CancelOperationOrPauseGame", throwIfNotFound: true);
        m_General_MousePosition = m_General.FindAction("MousePosition", throwIfNotFound: true);
        m_General_AxisInput = m_General.FindAction("AxisInput", throwIfNotFound: true);
        m_General_Interact = m_General.FindAction("Interact", throwIfNotFound: true);
        // UI_window
        m_UI_window = asset.FindActionMap("UI_window", throwIfNotFound: true);
        m_UI_window_Newaction = m_UI_window.FindAction("New action", throwIfNotFound: true);
        m_UI_window_CloseWindow = m_UI_window.FindAction("CloseWindow", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_MoveHorizontaly;
    private readonly InputAction m_Player_SpeedUpCamera;
    private readonly InputAction m_Player_CameraRotationKey;
    private readonly InputAction m_Player_MoveVerticaly;
    private readonly InputAction m_Player_Scroll;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveHorizontaly => m_Wrapper.m_Player_MoveHorizontaly;
        public InputAction @SpeedUpCamera => m_Wrapper.m_Player_SpeedUpCamera;
        public InputAction @CameraRotationKey => m_Wrapper.m_Player_CameraRotationKey;
        public InputAction @MoveVerticaly => m_Wrapper.m_Player_MoveVerticaly;
        public InputAction @Scroll => m_Wrapper.m_Player_Scroll;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @MoveHorizontaly.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveHorizontaly;
                @MoveHorizontaly.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveHorizontaly;
                @MoveHorizontaly.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveHorizontaly;
                @SpeedUpCamera.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpeedUpCamera;
                @SpeedUpCamera.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpeedUpCamera;
                @SpeedUpCamera.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpeedUpCamera;
                @CameraRotationKey.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotationKey;
                @CameraRotationKey.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotationKey;
                @CameraRotationKey.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotationKey;
                @MoveVerticaly.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveVerticaly;
                @MoveVerticaly.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveVerticaly;
                @MoveVerticaly.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveVerticaly;
                @Scroll.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScroll;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveHorizontaly.started += instance.OnMoveHorizontaly;
                @MoveHorizontaly.performed += instance.OnMoveHorizontaly;
                @MoveHorizontaly.canceled += instance.OnMoveHorizontaly;
                @SpeedUpCamera.started += instance.OnSpeedUpCamera;
                @SpeedUpCamera.performed += instance.OnSpeedUpCamera;
                @SpeedUpCamera.canceled += instance.OnSpeedUpCamera;
                @CameraRotationKey.started += instance.OnCameraRotationKey;
                @CameraRotationKey.performed += instance.OnCameraRotationKey;
                @CameraRotationKey.canceled += instance.OnCameraRotationKey;
                @MoveVerticaly.started += instance.OnMoveVerticaly;
                @MoveVerticaly.performed += instance.OnMoveVerticaly;
                @MoveVerticaly.canceled += instance.OnMoveVerticaly;
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // General
    private readonly InputActionMap m_General;
    private IGeneralActions m_GeneralActionsCallbackInterface;
    private readonly InputAction m_General_CancelOperationOrPauseGame;
    private readonly InputAction m_General_MousePosition;
    private readonly InputAction m_General_AxisInput;
    private readonly InputAction m_General_Interact;
    public struct GeneralActions
    {
        private @InputMaster m_Wrapper;
        public GeneralActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @CancelOperationOrPauseGame => m_Wrapper.m_General_CancelOperationOrPauseGame;
        public InputAction @MousePosition => m_Wrapper.m_General_MousePosition;
        public InputAction @AxisInput => m_Wrapper.m_General_AxisInput;
        public InputAction @Interact => m_Wrapper.m_General_Interact;
        public InputActionMap Get() { return m_Wrapper.m_General; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneralActions set) { return set.Get(); }
        public void SetCallbacks(IGeneralActions instance)
        {
            if (m_Wrapper.m_GeneralActionsCallbackInterface != null)
            {
                @CancelOperationOrPauseGame.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCancelOperationOrPauseGame;
                @CancelOperationOrPauseGame.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCancelOperationOrPauseGame;
                @CancelOperationOrPauseGame.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCancelOperationOrPauseGame;
                @MousePosition.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMousePosition;
                @AxisInput.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnAxisInput;
                @AxisInput.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnAxisInput;
                @AxisInput.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnAxisInput;
                @Interact.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_GeneralActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CancelOperationOrPauseGame.started += instance.OnCancelOperationOrPauseGame;
                @CancelOperationOrPauseGame.performed += instance.OnCancelOperationOrPauseGame;
                @CancelOperationOrPauseGame.canceled += instance.OnCancelOperationOrPauseGame;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @AxisInput.started += instance.OnAxisInput;
                @AxisInput.performed += instance.OnAxisInput;
                @AxisInput.canceled += instance.OnAxisInput;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public GeneralActions @General => new GeneralActions(this);

    // UI_window
    private readonly InputActionMap m_UI_window;
    private IUI_windowActions m_UI_windowActionsCallbackInterface;
    private readonly InputAction m_UI_window_Newaction;
    private readonly InputAction m_UI_window_CloseWindow;
    public struct UI_windowActions
    {
        private @InputMaster m_Wrapper;
        public UI_windowActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_UI_window_Newaction;
        public InputAction @CloseWindow => m_Wrapper.m_UI_window_CloseWindow;
        public InputActionMap Get() { return m_Wrapper.m_UI_window; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UI_windowActions set) { return set.Get(); }
        public void SetCallbacks(IUI_windowActions instance)
        {
            if (m_Wrapper.m_UI_windowActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_UI_windowActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_UI_windowActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_UI_windowActionsCallbackInterface.OnNewaction;
                @CloseWindow.started -= m_Wrapper.m_UI_windowActionsCallbackInterface.OnCloseWindow;
                @CloseWindow.performed -= m_Wrapper.m_UI_windowActionsCallbackInterface.OnCloseWindow;
                @CloseWindow.canceled -= m_Wrapper.m_UI_windowActionsCallbackInterface.OnCloseWindow;
            }
            m_Wrapper.m_UI_windowActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
                @CloseWindow.started += instance.OnCloseWindow;
                @CloseWindow.performed += instance.OnCloseWindow;
                @CloseWindow.canceled += instance.OnCloseWindow;
            }
        }
    }
    public UI_windowActions @UI_window => new UI_windowActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMoveHorizontaly(InputAction.CallbackContext context);
        void OnSpeedUpCamera(InputAction.CallbackContext context);
        void OnCameraRotationKey(InputAction.CallbackContext context);
        void OnMoveVerticaly(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
    }
    public interface IGeneralActions
    {
        void OnCancelOperationOrPauseGame(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnAxisInput(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IUI_windowActions
    {
        void OnNewaction(InputAction.CallbackContext context);
        void OnCloseWindow(InputAction.CallbackContext context);
    }
}
