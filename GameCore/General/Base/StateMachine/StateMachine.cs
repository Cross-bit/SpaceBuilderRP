﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;

namespace Assets.Scripts.GameCore.API.Base
{
    public class StateMachine
    {
        private IState _currentState; // Momentální stav

        // Seznam všech přechodů, dle Typu stavu(State)
        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>(); // Momentálně dostupné přechody ze stavu
        private List<Transition> _anyTransitions = new List<Transition>(); // Přechody dostupné pro všechny stavy(uzly)

        private static List<Transition> EmptyTransitions = new List<Transition>(0);

        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null)
                SetState(transition.To);

            _currentState?.Tick();
        }

        public void SetState(IState state)
        {
            if (state == _currentState)
                return;

            // Ukovčení current state a switch za nový
            _currentState?.OnExit();  // Konec animace atd...
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = EmptyTransitions;

            _currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, predicate)); // Do nově vytvořeného seznamu
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(state, predicate));
        }

        private class Transition
        {
            public Func<bool> Condition { get; } // Za jaké podmínky změnit stav
            public IState To { get; } // Do jakého stavu

            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }

        private Transition GetTransition()
        {
            foreach (var transition in _anyTransitions) // Jakýkoliv
                if (transition.Condition())
                    return transition;

            foreach (var transition in _currentTransitions) // Dostupné pro daný uzel
                if (transition.Condition())
                    return transition;

            return null;
        }
    }
}