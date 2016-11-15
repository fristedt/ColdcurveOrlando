using UnityEngine;
using System.Collections;

public class Shootable : MonoBehaviour {
    private GameObject blood;

    void Start() {
        blood = Resources.Load<GameObject>("Blood");
    }

    public void OnHit() {
        Instantiate(blood, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
