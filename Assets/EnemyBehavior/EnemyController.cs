using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public const float MaxVel = 4; 
    new public Rigidbody2D rigidbody2D;

    public GameObject player;
    private GameObject blood;
    private SpriteRenderer spriteRenderer;
    private string spritePrefix = "manBlue";
    private Weapon weapon;

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

	// Use this for initialization
	void Start () {
        idleState = new IdleState(this);
        inspectState = new InspectState(this);
        attackState = new AttackState(this);
        deadState = new DeadState(this);

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

        currentState.UpdateState();
    }

    public bool CanSeePlayer() {
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

    public void GunshotHeard(Vector2 gunshotLocation) {
        if (currentState == idleState || currentState == inspectState) {
            inspectState.SetInspectPosition(gunshotLocation);
            currentState = inspectState;
        }
    }
}
