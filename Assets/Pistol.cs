using UnityEngine;
using System.Collections;
using System;

public class Pistol : Weapon {
    private GameObject bulletPrefab;
    private Transform holderTransform;
    private Vector3 spawnPos = new Vector3(0.225f, -0.1f, 0);

    public Pistol(Transform holderTransform) {
        this.holderTransform = holderTransform;
        bulletPrefab = Resources.Load<GameObject>("Bullet");
    }

    public void Shoot() {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, holderTransform.TransformPoint(spawnPos), Quaternion.identity) as GameObject;
        bullet.GetComponent<BulletController>().direction = holderTransform.right;
    }
}
