using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Vector3 hip;
    public Vector3 aim;
    public float aimSpeed = 15;

    private void Start()
    {
        hip = transform.localPosition;
        Debug.Log(hip);
    }
    // Update is called once per frame
    void Update()
    {
        if (Shooting.sighted)
        {
            if (Input.GetMouseButton(1))
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, aim, aimSpeed * Time.deltaTime);
            }
            else
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, hip, aimSpeed * Time.deltaTime);
            }
        }
    }
}
