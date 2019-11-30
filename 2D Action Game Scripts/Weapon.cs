using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject Projectile;
    public Transform shotPoint;
    public float tbs;

    private float shotTime;

    Animator CameraAnim;

    private void Start()
    {
        CameraAnim = Camera.main.GetComponent<Animator>();
    }

	private void Update ()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if(Input.GetMouseButton(0))
        {
            if(Time.time >= shotTime)
            {
                Instantiate(Projectile, shotPoint.position, transform.rotation);
                CameraAnim.SetTrigger("Shake");
                shotTime = Time.time + tbs;
            }
        }
	}
}
