using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactForce : MonoBehaviour
{
    public float thing;
    private void OnCollisionEnter(Collision collision)
    {
        thing = collision.relativeVelocity.magnitude;
    }
}
