using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController character;
    public Transform groundChecker;
    public LayerMask checkLayer;
    public float checkDistance;
    bool grounded;
    public float speed;
    public float gravity;
    public float jumpSpeed;
    Vector3 velocity;
    Vector3 forward;
    // Update is called once per frame
    void Update()
    {
        Vector3 right = (transform.right * Input.GetAxis("Horizontal"));
        if (grounded && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.W)) forward = (2 * transform.forward * Input.GetAxis("Vertical"));
        else forward = (transform.forward * Input.GetAxis("Vertical"));
        Vector3 move = (forward + right);
        if (!grounded) move /= 1.5f;
        if (grounded && Input.GetKey(KeyCode.LeftShift)) move /= 2;

        character.Move(move * speed * Time.deltaTime);

        grounded = Physics.CheckSphere(groundChecker.position, checkDistance, checkLayer);

        if (grounded && velocity.y < 0) velocity.y = -2f;
        if (grounded && Input.GetButton("Jump")) velocity.y = Mathf.Sqrt(-2 * gravity * jumpSpeed);
        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);

    }
}
