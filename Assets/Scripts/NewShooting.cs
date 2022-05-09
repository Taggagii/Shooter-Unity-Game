using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShooting : MonoBehaviour
{

    public Camera gameCam;
    public Animator shooting;
    public float damage;
    public float range;
    public float fireRate;
    public bool autoFire;
    public bool autoReload;
    bool shot;

    void Update()
    {
        if (autoFire) shot = Input.GetButton("Fire1");
        else shot = Input.GetButtonDown("Fire1");

        if (shot)
        {
            shooting.SetBool("shot", true);
            Shoot();
              
        }
        if (Input.GetButtonUp("Fire1"))
        {
            shooting.SetBool("shot", false);
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameCam.transform.position, gameCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
