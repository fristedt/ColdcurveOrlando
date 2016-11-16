using UnityEngine;
using System.Collections;
using System;

public class InspectState : IEnemyState {
    private readonly EnemyController enemy;

    public InspectState(EnemyController enemyController) {
        enemy = enemyController;
    }

    public void ToAttackState() {
        throw new NotImplementedException();
    }

    public void ToDeadState() {
        throw new NotImplementedException();
    }

    public void ToIdleState() {
        throw new NotImplementedException();
    }

    public void ToInspectState() {
        throw new NotImplementedException();
    }

    public void UpdateState() {
        throw new NotImplementedException();
    }
}
