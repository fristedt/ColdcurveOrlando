using System;
using UnityEngine;

public class IdleState : IEnemyState {
    private readonly EnemyController enemy;

    public IdleState(EnemyController enemyController) {
        enemy = enemyController;
    }

    public void ToAttackState() {
        throw new NotImplementedException();
    }

    public void ToDeadState() {
        throw new NotImplementedException();
    }

    public void ToIdleState() {
        Debug.LogError("Can't transition to same state.");
    }

    public void ToInspectState() {
        throw new NotImplementedException();
    }

    public void UpdateState() {
        throw new NotImplementedException();
    }
}
