using UnityEngine;
using System.Collections;

public class EnemyController : Human {

    [SerializeField]
    private GameObject player;

    new void Update() {
        velocity = (player.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);
        velocity *= MaxVel;
        Shoot();
        base.Update();
    }
}
