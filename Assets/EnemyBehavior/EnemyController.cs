using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    protected const float MaxVel = 4; 

    private GameObject player;
    private GameObject blood;
    private SpriteRenderer spriteRenderer;
    private string spritePrefix = "manBlue";
    private Weapon weapon;
    private Rigidbody2D rigidbody2D;

    protected Vector3 velocity;

    [HideInInspector]
    public IEnemyState currentState;
    [HideInInspector]
    public IdleState idleState;
    [HideInInspector]
    public InspectState inspectState;
    [HideInInspector]
    public AttackState attackState;
    [HideInInspector]
    public DeadState deadState;

    private void Awake() {
        idleState = new IdleState(this);
        inspectState = new InspectState(this);
        attackState = new AttackState(this);
        deadState = new DeadState(this);
    }

	// Use this for initialization
	void Start () {
        currentState = idleState;

        blood = Resources.Load<GameObject>("Blood");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
	}

    void Update() {
        if (player == null) {
            rigidbody2D.velocity = Vector3.zero;
            return;
        }

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

        if (go.tag == "Player") {
            player.GetComponent<Shootable>().OnHit();
        }
    }
}
