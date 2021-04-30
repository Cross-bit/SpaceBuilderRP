using UnityEngine;

namespace Assets.Scripts.GameCore.GameControls.Controllers
{
    public interface ICameraInputControls : IControlsInput
    {
        Vector2 CameraHorizontalMove { get; }
        bool CameraMoveFasterButton { get; }
        bool CameraRotationButton { get; }

    }
}