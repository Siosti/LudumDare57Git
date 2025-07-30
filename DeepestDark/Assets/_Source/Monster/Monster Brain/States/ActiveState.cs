using FillDisplayerSystem;
using MonsterMovementSystem;
using MonsterPerseptionSystem;
using WeaponSystem;
using StateMachineSystem;
using System;
using UnityEngine;
using EffectsPlayerSystem;

namespace MonsterBrainSystem
{
    internal class ActiveState : IState
    {
        private readonly MonsterPerception _perception;
        private readonly IFillDisplayer _awarenessDisplayer;

        private readonly StateMachine _stateMachine;

        public ActiveState(MonsterMovement movement, MonsterPerception perception, MonsterObjectives objectives, Weapon weapon, IFillDisplayer awarenessDisplayer, IEffectPlayer alertEffectPlayer)
        {
            _perception = perception ?? throw new ArgumentNullException(nameof(perception));
            _awarenessDisplayer = awarenessDisplayer ?? throw new ArgumentNullException(nameof(awarenessDisplayer));

            StillState still = new(movement);
            PatrolState patrol = new(movement, objectives);
            ChaseState chase = new(movement, perception, weapon, alertEffectPlayer);
            
            _stateMachine = new(still);

            _stateMachine.AddTransition(still, patrol, () => objectives.PatrolRoute.Length > 0);
            _stateMachine.AddTransition(patrol, still, () => objectives.PatrolRoute.Length <= 0);

            _stateMachine.AddTransition(still, chase, () => AwarenessLevel >= 1.0f);
            _stateMachine.AddTransition(patrol, chase, () => AwarenessLevel >= 1.0f);

            _stateMachine.AddTransition(chase, still, () => AwarenessLevel <= 0.0f && objectives.PatrolRoute.Length <= 0);
            _stateMachine.AddTransition(chase, patrol, () => AwarenessLevel <= 0.0f && objectives.PatrolRoute.Length > 0);

            _stateMachine.AddTransition(chase, still, chase.ChaseFinished);
            _stateMachine.AddTransition(chase, patrol, chase.ChaseFinished);
        }

        public float AwarenessLevel { get; private set; } = 0.0f;

        public void Enter()
        {
            Debug.Log("Active");

            _stateMachine.Enter();
        }

        public void Exit()
        {
            _awarenessDisplayer.DisplayFill(AwarenessLevel);

            _stateMachine.Exit();
        }

        public void Tick()
        {
            if (_perception.PercievedObjects.Count > 0)
            {
                AwarenessLevel = Mathf.Clamp(AwarenessLevel + Time.deltaTime, 0.0f, 1.0f);
            }
            else
            {
                AwarenessLevel = Mathf.Clamp(AwarenessLevel - Time.deltaTime, 0.0f, 1.0f);
            }

            _awarenessDisplayer.DisplayFill(AwarenessLevel);

            _stateMachine.Tick();
        }
    }
}
