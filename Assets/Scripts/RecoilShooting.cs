using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilShooting : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
            animator.SetBool("shot", true);
        else
            animator.SetBool("shot", false);
    }
}
