using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject toWhat;
    public GameObject groundChecker;
    public GameObject headChecker;
    public LayerMask checkLayer;
    public float hitSpeed = 10;
    public float damage = 10;
    public float speed = 2;
    bool hitting;
    bool stop;
    float nextHitTime;
    // Update is called once per frame
    void Update()
    {
        stop = false;
        if (Vector3.Distance(transform.position, toWhat.transform.position) <= 1.21f)
            stop = true;
        if (!stop) stop = Physics.CheckSphere(groundChecker.transform.position, 0.4f, checkLayer);
        if (!stop) stop = Physics.CheckSphere(headChecker.transform.position, 0.4f, checkLayer);
        if (!stop) transform.position = Vector3.MoveTowards(transform.GetComponent<Collider>().bounds.center, toWhat.GetComponent<Collider>().bounds.center, 2f * Time.deltaTime);
        hitting = stop;
        if (!hitting)
        {
            if (Vector3.Distance(transform.position, toWhat.transform.position) <= 2f)
                hitting = true;
            if (!hitting) hitting = Physics.CheckSphere(groundChecker.transform.position, 1f, checkLayer);
            if (!hitting) hitting = Physics.CheckSphere(headChecker.transform.position, 1f, checkLayer);
            if (!hitting) transform.position = Vector3.MoveTowards(transform.GetComponent<Collider>().bounds.center, toWhat.GetComponent<Collider>().bounds.center, speed * Time.deltaTime);
        }
        if (hitting && Time.time >= nextHitTime)
        {
            Target target = toWhat.transform.GetComponent<Target>();
            target.TakeDamage(damage);
            nextHitTime = Time.time + 1 / hitSpeed;
        }
    }
}
