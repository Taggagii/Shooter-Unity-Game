using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public Camera gameCam;
    public Camera gunCam;
    public ParticleSystem muzzleFlash;
    public GameObject flash;
    public GameObject impactFlash;
    public AudioSource gunSound;
    public AudioSource reloadSound;
    public Text bulletCount;
    public float damage = 10;
    public float range = 100;
    public int magazineSize = 15;
    public float impactForce = 100;
    public float fireRate = 15;
    public float zoom = 6;
    public bool autoReload = true;
    public bool autoFire;
    public bool hasSight;
    public static bool sighted;
    float timeOfNextShot;
    float timeStart = 0;
    float reloadingTime = 0;
    int bulletCounter = 0;
    bool shooting = false;
    float endAnimationTime;
    void Start()
    {
        bulletCount.text = (magazineSize - bulletCounter).ToString();
    }
    // Update is called once per frame
    void Update()
    {
        sighted = hasSight;
        if (autoFire) shooting = Input.GetButton("Fire1");
        else shooting = Input.GetButtonDown("Fire1");
        bulletCount.text = (magazineSize - bulletCounter).ToString();
        if (shooting && Time.time >= timeOfNextShot && bulletCounter < magazineSize && reloadingTime <= Time.time)
        {
            bulletCounter++;
            bulletCount.text = (magazineSize - bulletCounter).ToString();
            gunSound.Play();
            flash.SetActive(true);
            timeStart = Time.time + muzzleFlash.main.duration;
            timeOfNextShot = Time.time + 1 / fireRate;
            muzzleFlash.Play();
            endAnimationTime = Time.time + 0.1f;
            Shoot();
        }

        if (Time.time >= timeStart)
        {
            flash.SetActive(false);
            timeStart = 0;
        }
        if (Input.GetKeyDown(KeyCode.R) && bulletCounter > 0 || autoReload && bulletCounter == magazineSize)
        {
            reloadSound.Play();
            bulletCounter = 0;
            bulletCount.text = (magazineSize - bulletCounter).ToString();
            reloadingTime = Time.time + reloadSound.clip.length;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            gameCam.fieldOfView /= zoom;
            gunCam.fieldOfView /= zoom / 4;
            CameraController.sensitivity /= zoom;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            gameCam.fieldOfView *= zoom;
            gunCam.fieldOfView *= zoom / 4;
            CameraController.sensitivity *= zoom;
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameCam.transform.position, gameCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null) target.TakeDamage(damage);
            if (hit.rigidbody != null) hit.rigidbody.AddForce(-hit.normal * impactForce);
            GameObject hitPoints = Instantiate(impactFlash, hit.point, Quaternion.LookRotation(hit.normal));
            hitPoints.SetActive(true);
            Destroy(hitPoints, 0.2f);
        }
    }

    

    
   
}


