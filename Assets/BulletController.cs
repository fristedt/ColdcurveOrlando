using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    private const float speed = 37f;

    private Vector3 oldPos;
    private Vector3 newPos;
    private Vector3 velocity;

    public Vector3 direction;

    void Start() {
        oldPos = transform.position;
        newPos = transform.position;

        Invoke("Destroy", 5);
    }

    void Update() {
        velocity = speed * direction;
        newPos += velocity * Time.deltaTime;

        Vector3 deltaPos = newPos - oldPos;
        float distance = deltaPos.magnitude;

        if (distance > 0) {
            RaycastHit2D hit = Physics2D.Raycast(oldPos, deltaPos, distance);
            if (hit.collider != null) {
                Destroy(gameObject);
                if (hit.collider.tag == "Human") {
                    hit.collider.gameObject.GetComponent<Human>().Hit();
                }
            }
        }

        oldPos = transform.position;
        transform.position = newPos;
    }

    private void Destroy() {
        Destroy(gameObject);
    }
}
