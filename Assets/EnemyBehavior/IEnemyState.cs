public interface IEnemyState {
    void UpdateState();

    void ToIdleState();
    void ToInspectState();
    void ToAttackState();
    void ToDeadState();
}
