using UnityEngine;
using System.Collections;

public class PlayerController : Human {
	// Update is called once per frame
	void Update () {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 lookDir = Input.mousePosition - playerScreenPos;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg);

        float forward = Input.GetAxis("Vertical") * MaxVel;
        float strafe = Input.GetAxis("Horizontal") * MaxVel;
        velocity = new Vector3(strafe, forward, 0);

        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }

        base.Update();
    }
}
