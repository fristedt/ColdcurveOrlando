using UnityEngine;
using System.Collections;

public class EnemyController : Human {

    [SerializeField]
    private GameObject player;

    new void Update() {
        velocity = Vector3.zero;
        if (CanSeePlayer()) {
            velocity = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);
            velocity *= MaxVel;
        }
        Shoot();
        base.Update();
    }

    bool CanSeePlayer() {
        Vector3 dir = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, dir);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
        return hit.collider.name == "Player";
    }
}
