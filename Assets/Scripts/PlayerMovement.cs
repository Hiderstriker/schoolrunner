using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public Rigidbody rb;

    public float speed =100.0f;

    float keyX, keyY, keyZ;

    // Update is called once per frame
    void Update()
    {
        keyX = Input.GetAxisRaw("Horizontal");
        keyY = Input.GetAxisRaw("Vertical");
        keyZ = Input.GetAxisRaw("Jump");
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(keyX * 50.0f, keyZ * 50.0f, keyY * 50.0f));
    }
}
