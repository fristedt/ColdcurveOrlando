using UnityEngine;
using System.Collections;
using System;

public class InspectState : IEnemyState {
    private readonly EnemyController enemy;

    public InspectState(EnemyController enemyController) {
        enemy = enemyController;
    }

    public void ToAttackState() {
        enemy.currentState = enemy.attackState;
    }

    public void ToDeadState() {
        enemy.currentState = enemy.deadState;
    }

    public void ToIdleState() {
        enemy.currentState = enemy.idleState;
    }

    public void ToInspectState() {
        Debug.LogError("Can't transition to same state.");
    }

    public void UpdateState() {
        throw new NotImplementedException();
    }
}
