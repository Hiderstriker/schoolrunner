using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public Rigidbody rb;

    public Camera cam;

    public Transform headLoc;

    public Transform orientation;

    public float moveSpeed = 30.0f;
    public float sprintSpeed = 60.0f;
    private float speed;

    public float sensX = 2.7f;
    public float sensY = 2.7f;

    float keyX, keyY;

    float mouseX, mouseY;

    float xRotation, yRotation;

    Vector3 moveDirection;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        keyX = Input.GetAxisRaw("Horizontal");
        keyY = Input.GetAxisRaw("Vertical");

        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        cam.transform.localPosition = headLoc.localPosition;

        yRotation += mouseX * sensX;
        xRotation -= mouseY * sensY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.localRotation = Quaternion.Euler(0, yRotation, 0);

        moveDirection = orientation.forward * keyY + orientation.right * keyX;

        CheckSprint();
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection.normalized * speed);
    }

    private void CheckSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = moveSpeed;
        }
    }

}
