using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    protected const float MaxVel = 4; 

    private SpriteRenderer spriteRenderer;
    private string spritePrefix = "manBlue";
    private Weapon weapon;
    private Rigidbody2D rigidbody2D;

    protected Vector3 velocity;

    // Use this for initialization
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 lookDir = Input.mousePosition - playerScreenPos;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg);

        float forward = Input.GetAxis("Vertical") * MaxVel;
        float strafe = Input.GetAxis("Horizontal") * MaxVel;
        velocity = new Vector3(strafe, forward, 0);
        velocity = Vector3.ClampMagnitude(velocity, MaxVel);
        rigidbody2D.velocity = velocity;

        if (Input.GetMouseButtonDown(0)) {
            if (weapon != null)
                weapon.Shoot();
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        GameObject go = collider.gameObject;
        if (go.tag == "Weapon") {
            string path = spritePrefix + "/" + go.name.Split("_".ToCharArray())[1];
            spriteRenderer.sprite = Resources.Load<Sprite>(path);
            Destroy(go);
            weapon = new Pistol(transform);
        }
    }
}
