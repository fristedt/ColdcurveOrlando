using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {

    protected const float MaxVel = 4; 

    private GameObject blood;
    private SpriteRenderer spriteRenderer;
    private string spritePrefix = "manBlue";
    private Weapon weapon;
    private Rigidbody2D rigidbody2D;

    protected Vector3 velocity;

	// Use this for initialization
	void Start () {
        blood = Resources.Load<GameObject>("Blood");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	protected void Update () {
        velocity = Vector3.ClampMagnitude(velocity, MaxVel);
        rigidbody2D.velocity = velocity;
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

    protected void Shoot() {
        if (weapon == null)
            return;
        weapon.Shoot();
    }

    public void Hit() {
        Instantiate(blood, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
