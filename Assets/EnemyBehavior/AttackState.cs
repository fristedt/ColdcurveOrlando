using UnityEngine;
using System.Collections;
using System;

public class AttackState : IEnemyState {
    private readonly EnemyController enemy;

    public AttackState(EnemyController enemyController) {
        enemy = enemyController;
    }

    public void ToAttackState() {
        throw new NotImplementedException();
    }

    public void ToDeadState() {
        throw new NotImplementedException();
    }

    public void ToIdleState() {
        enemy.currentState = enemy.idleState;
    }

    public void ToInspectState() {
        throw new NotImplementedException();
    }

    public void UpdateState() {
        if (!enemy.CanSeePlayer())
            ToIdleState();

        Vector2 velocity = (enemy.player.transform.position - enemy.transform.position).normalized * EnemyController.MaxVel;
        velocity = Vector2.ClampMagnitude(velocity, EnemyController.MaxVel);
        enemy.rigidbody2D.velocity = velocity;
        enemy.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);
    }
}
