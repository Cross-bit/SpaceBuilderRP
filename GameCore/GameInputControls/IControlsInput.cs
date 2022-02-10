namespace Assets.Scripts.GameCore.GameControls
{
    public interface IControlsInput // Všechno v controlls musí implementovat(krom InputManager...)!
    {
        void RegisterInputs(InputMaster inputSystemAssetScript);
        void UnregisterInputs(InputMaster inputSystemAssetScript);
    }
}
