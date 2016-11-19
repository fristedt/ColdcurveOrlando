using UnityEngine;
using System.Collections.Generic;
using System;

public class InspectState : IEnemyState {
    private const float Threshold = 0.1f;

    private readonly EnemyController enemy;
    private Pathfinder pathfinder;
    private List<Vector2> path;
    private int currentTargetIndex;

    public InspectState(EnemyController enemyController) {
        enemy = enemyController;
        pathfinder = enemy.GetComponent<Pathfinder>();
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
        if (currentTargetIndex >= path.Count) {
            ToIdleState();
            return;
        }

        if (enemy.CanSeePlayer())
            ToAttackState();

        Vector2 velocity = (path[currentTargetIndex] - (Vector2)enemy.transform.position).normalized * EnemyController.MaxVel;
        velocity = Vector2.ClampMagnitude(velocity, EnemyController.MaxVel);
        enemy.rigidbody2D.velocity = velocity;
        enemy.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);

        if (Vector2.Distance(enemy.transform.position, path[currentTargetIndex]) < Threshold)
            currentTargetIndex += 1;
    }
    
    public void SetPathTo(Vector2 destination) {
        path = pathfinder.GetPathTo(destination);
        currentTargetIndex = 0;
    }
}
