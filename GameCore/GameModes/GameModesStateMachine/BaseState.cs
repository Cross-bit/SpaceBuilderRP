using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameCore.GameModes.GameModesStateMachine
{
    public abstract class BaseState
    {
        private StateMachine _stateMachine;

        public BaseState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public abstract void Enter();

        public virtual void OnUpdate() { return; }

        public virtual void OnLateUpdate() { return; }

        public abstract void Exit();
    }
}
