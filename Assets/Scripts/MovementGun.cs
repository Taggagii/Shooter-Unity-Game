using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGun : MonoBehaviour
{
    public Camera gameCamera;
    GameObject spawn;
    public bool autoFire = false;
    // Update is called once per frame
    GameObject spawnedThings;
    bool cloning;
    bool forcing;
    bool shooting;
    bool gravity;
    bool push = true;
    bool explosive = false;
    float originalMousex;
    float originalMousey;
    RaycastHit currentHit;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            cloning = true;
            forcing = false;
            gravity = false;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            cloning = false;
            forcing = true;
            gravity = false;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            cloning = false;
            forcing = false;
            gravity = true;
        }
        if (cloning)
        {
            if (Input.GetKeyDown(KeyCode.P)) autoFire = !autoFire;
            if (Input.GetButtonDown("Fire2"))
            {
                RaycastHit hit;
                if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit))
                {
                    if (hit.transform.name != "Ground")
                    {
                        spawn = hit.transform.gameObject;
                    }
                }
            }
            if (autoFire) shooting = Input.GetButton("Fire1");
            else shooting = Input.GetButtonDown("Fire1");

            if (shooting)
            {
                RaycastHit hit;
                if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit))
                {
                    spawnedThings = Instantiate(spawn, new Vector3(hit.point.x, hit.point.y + (spawn.transform.localScale.y / 2), hit.point.z), Quaternion.LookRotation(-hit.normal));
                }

            }
            if (Input.GetMouseButton(2))
            {
                RaycastHit hit;
                if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit))
                {
                    if (hit.transform.name != "Ground") Destroy(hit.transform.gameObject);
                }

            }
        }
        if (forcing)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                explosive = true;
                push = false;
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                explosive = false;
                push = true;
            }
            RaycastHit hit;
            if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit))
            {
                if (hit.rigidbody != null)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (push) hit.rigidbody.AddForce(-hit.normal * 8000);
                        if (explosive) hit.rigidbody.AddExplosionForce(20000, hit.transform.position, 1000);
                    }
                }
            }
        }
        if (gravity)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out currentHit);
                originalMousex = Input.GetAxis("Mouse X");
                originalMousey = Input.GetAxis("Mouse Y");
            }
            if (Input.GetButton("Fire1") && currentHit.transform.name != "Ground")
            {
                Debug.Log(Vector3.Distance(transform.position, currentHit.transform.position));
                currentHit.transform.localPosition = new Vector3(currentHit.transform.localPosition.x + (originalMousex + (Input.GetAxis("Mouse X") * (Vector3.Distance(transform.position, currentHit.transform.position) / 2) * Time.deltaTime)), currentHit.transform.localPosition.y + (originalMousey + (Input.GetAxis("Mouse Y") * (Vector3.Distance(transform.position, currentHit.transform.position) / 2) * Time.deltaTime)), currentHit.transform.localPosition.z);
            }
        }
    }
}



