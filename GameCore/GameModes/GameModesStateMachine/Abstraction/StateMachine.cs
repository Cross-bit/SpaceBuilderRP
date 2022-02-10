using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameCore.GameModes.GameModesStateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        private BaseState _currentState;

        private void Start() {
            _currentState = GetInitialState();
            _currentState?.Enter();
        }

        private void Update() {
            _currentState?.OnUpdate();
        }

        private void FixedUpdate() {
            _currentState?.OnFixedUpdate();
        }

        private void LateUpdate() {
            _currentState?.OnLateUpdate();

        }

        public void SetNewState(BaseState changeTo) {
            _currentState?.Exit();

            _currentState = changeTo;

            _currentState.Enter();
        }
        
        protected virtual BaseState GetInitialState() {
            return null;
        }

        private void OnGUI() {
            string content = _currentState != null ? _currentState.GetType().ToString() : "(no current state)";
            GUILayout.Label($"<color='white'><size=40>{content}</size></color>");
        }
    }
}
