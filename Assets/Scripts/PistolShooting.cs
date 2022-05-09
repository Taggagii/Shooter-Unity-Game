using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PistolShooting : MonoBehaviour
{
    public Animator shootingAnimation;
    public ParticleSystem gunFlash;
    public AudioSource gunSound;
    public GameObject gunLight;
    public GameObject impactFlash;
    public Text bulletCounter;
    public Camera gameCamera;
    public float range = 10;
    public float damage = 10;
    public float fireRate = 10;
    public float impactForce = 80;
    public float zoomFOV = 20;
    public float magazineSize = 10;
    float shootingEndTime;
    bool isAiming = false;
    float originalFOV;
    float originalSensitivity = Mathf.NegativeInfinity;
    float bulletCount;

    private void Start()
    {
        originalFOV = gameCamera.fieldOfView;
    }
    // Update is called once per frame
    void Update()
    {
        bulletCounter.text = (magazineSize - bulletCount).ToString();
        if (originalSensitivity == Mathf.NegativeInfinity) originalSensitivity = CameraController.sensitivity;
        if (Input.GetButtonDown("Fire1") && shootingEndTime + 0.10 <= Time.time && bulletCount < magazineSize)
        {
            bulletCount++;
            shootingAnimation.SetBool("Shot", true);
            gunFlash.Play();
            gunSound.Play();
            shootingEndTime = Time.time + (1/(fireRate*2));
            Shoot();
            gunLight.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.R) && bulletCount != 0)
        {
            bulletCount = 0;
        }
        if (shootingEndTime <= Time.time)
        {
            shootingAnimation.SetBool("Shot", false);
            gunLight.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            shootingAnimation.SetBool("Aiming", true);
            isAiming = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            shootingAnimation.SetBool("Aiming", false);
            isAiming = false;
        }
        if (isAiming)
        {
            gameCamera.fieldOfView = zoomFOV;
            CameraController.sensitivity = zoomFOV /2 ;
        }
        else
        {
            gameCamera.fieldOfView = originalFOV;
            CameraController.sensitivity = originalSensitivity;
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null) target.TakeDamage(damage);
            if (hit.rigidbody != null) hit.rigidbody.AddForce(-hit.normal * impactForce);
            GameObject hitPoints = Instantiate(impactFlash, hit.point, Quaternion.LookRotation(hit.normal));
            hitPoints.SetActive(true);
            Destroy(hitPoints, 0.05f);
               
        }
    }
}
