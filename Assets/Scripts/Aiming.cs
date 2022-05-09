using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Animator animator;
    public GameObject scopeOverlay;
    public GameObject gunCam;

    // Update is called once per frame
    void Update()
    {
        if (!Shooting.sighted)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                animator.SetBool("scoped", true);
                StartCoroutine(OnScoped());
            }
            if (Input.GetButtonUp("Fire2"))
            {
                animator.SetBool("scoped", false);
                OnUnscoped();
            }
        }
    }
    void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        gunCam.SetActive(true);
    }
    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(0.15f);
        scopeOverlay.SetActive(true);
        gunCam.SetActive(false);
    }
}
