using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    protected const float MaxVel = 4; 

    private GameObject blood;
    private SpriteRenderer spriteRenderer;
    private string spritePrefix = "manBlue";
    private Weapon weapon;
    private Rigidbody2D rigidbody2D;

    protected Vector3 velocity;

    [SerializeField]
    private GameObject player;

	// Use this for initialization
	void Start () {
        blood = Resources.Load<GameObject>("Blood");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
	}

    void Update() {
        velocity = Vector3.zero;
        if (CanSeePlayer()) {
            velocity = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);
            velocity *= MaxVel;
        }
        velocity = Vector3.ClampMagnitude(velocity, MaxVel);
        rigidbody2D.velocity = velocity;

        if (weapon != null)
            weapon.Shoot();
    }

    bool CanSeePlayer() {
        Vector3 dir = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, dir);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
        return hit.collider.name == "Player";
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
