using System;
using UnityEngine;

public class IdleState : IEnemyState {
    private readonly EnemyController enemy;

    public IdleState(EnemyController enemyController) {
        enemy = enemyController;
    }

    public void ToAttackState() {
        Debug.LogError("Only inspect can transition to attack state.");
    }

    public void ToDeadState() {
        enemy.currentState = enemy.deadState;
    }

    public void ToIdleState() {
        Debug.LogError("Can't transition to same state.");
    }

    public void ToInspectState() {
        enemy.currentState = enemy.inspectState;
    }

    public void UpdateState() {
        // Do nothing.
    }
}
