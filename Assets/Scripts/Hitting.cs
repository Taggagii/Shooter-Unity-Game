using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitting : MonoBehaviour
{
    public Animator axeSwing;
    public Camera gameCamera;
    public AudioSource axeSound;
    public float impactForce = 80;
    public float range = 1;
    public float damage = 20;
    float stopAnimationTime;
    bool attacked;
    float attackTime;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            axeSwing.SetTrigger("Swung");
            stopAnimationTime = Time.time + 1;
            attackTime = Time.time + 0.5f;
            attacked = true;
        }
        if (attacked && Time.time >= attackTime)
        {
            Attack();
            attacked = false;
        }
        if (Time.time >= stopAnimationTime)
        {
            transform.localPosition = new Vector3(-0.2f, 0.1f, 0);
            transform.localRotation = new Quaternion(-0.9f, -0.4f, 0, 0);
        }

    }
    void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                axeSound.Play();
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
    
}
