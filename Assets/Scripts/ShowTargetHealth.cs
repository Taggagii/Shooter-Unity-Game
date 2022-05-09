using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTargetHealth : MonoBehaviour
{
    public Text targetsHealthShower;
    public Camera gameCamera;
    public float range;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
                targetsHealthShower.text = target.returnHealth().ToString();
            else
                targetsHealthShower.text = string.Empty;
        }
        else
            targetsHealthShower.text = string.Empty;
    }
}
