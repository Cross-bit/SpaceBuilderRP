using UnityEngine;

namespace Assets.Scripts.GameCore.GameControls.Controllers
{ 
    public interface IGeneralInputControls : IControlsInput
    {
        Vector2 AxisInput { get; }
        bool IsPlayerInteracting { get; }
        Vector2 MousePosition { get; }
        bool WasCancleOperationBtnTriggered { get; }
    }
}