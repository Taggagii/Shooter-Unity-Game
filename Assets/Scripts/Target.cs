using UnityEngine;
using UnityEngine.UI;
public class Target : MonoBehaviour
{
    public GameObject destroyedVersion;
    public Text showHealth;
    public Canvas canvasStuff;
    public float health = 50;
    public float despawnTime = 10f;
    public float regenRate;
    public float regenAmount;
    public bool takeFallDamage;
    public bool regenHealth;
    public bool showAsHealth;
    bool dead = false;
    GameObject destroyedBits;
    float moreHealthTime;
    float originalHealth;
    private void Start()
    {
        if (showAsHealth) showHealth.text = $"Health: {health.ToString()}";
        originalHealth = health;
    }
    private void Update()
    {
        if (regenHealth && Time.time >= moreHealthTime && health < originalHealth)
        {
            health += regenAmount;
            moreHealthTime = Time.time + 1 / regenRate;
            if (health > originalHealth) health = originalHealth;
            showHealth.text = $"Health: {health.ToString()}";
        }
    }
    public void TakeDamage(float amountOfDamage)
    {
        if (!dead)
        {
            health -= amountOfDamage;
            moreHealthTime = Time.time + 2;
            if (showAsHealth) showHealth.text = $"Health: {health.ToString()}";
            if (health <= 0)
            {
                Die();
            }
        }
    }
    public float returnHealth()
    {
        return health;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (takeFallDamage && !dead)
        {
            float damage = collision.relativeVelocity.magnitude;
            if (damage > 5)
            {
                TakeDamage(damage / 2);
                if (showAsHealth) showHealth.text = $"Health: {health.ToString()}";
            }
        }
    }
    void Die()
    {
        if (!dead)
        {
            if (destroyedVersion != null)
                destroyedBits = Instantiate(destroyedVersion, transform.position, transform.rotation);
                Destroy(destroyedBits, despawnTime);
            Destroy(gameObject);
            Destroy(showHealth);
            Destroy(canvasStuff);
            dead = true;
            
        }
    }
}
