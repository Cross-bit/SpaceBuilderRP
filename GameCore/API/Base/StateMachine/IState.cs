
namespace Assets.Scripts.GameCore.API.Base {
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}