using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public Rigidbody rb;
    public Camera cam;
    public Transform headLoc;
    public Transform orientation;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpHeight = 10.0f;

    public float moveSpeed = 30.0f;
    public float sprintSpeed = 60.0f;
    private float speed;

    public float sensX = 2.7f;
    public float sensY = 2.7f;

    float keyX, keyY;

    float mouseX, mouseY;

    float xRotation, yRotation;

    Vector3 moveDirection;


    //Cursor verschindet und wird in der Mitte gelockt wenn man auf den Screen clickt (automatisch eingestellt, dass er wieder erscheint, wenn man Esc drückt)
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //laufen
        keyX = Input.GetAxisRaw("Horizontal");
        keyY = Input.GetAxisRaw("Vertical");

        //Mausbewegung erkennen lassen
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        //Kamera auf den "head" (child von Player) gebindet
        cam.transform.localPosition = headLoc.localPosition;

        //Kamerabewegung and Sensetivity angepasst und Drehblockade eingebaut.
        yRotation += mouseX * sensX;
        xRotation -= mouseY * sensY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        //Eingestellt, dass sich anderen Achsen mitdrehen, wenn man sich auf einer anderen Achse dreht
        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.localRotation = Quaternion.Euler(0, yRotation, 0);

        //Eingestellt, dass man sich immer in die Richtung bewegt in die man schaut
        moveDirection = orientation.forward * keyY + orientation.right * keyX;

        //ermoeglicht das Sprinten
        CheckSprint();

        Jump();
    }
    
    //Bewegung
    private void FixedUpdate()
    {
        rb.AddForce(moveDirection.normalized * speed);
    }

    //Sprinten auf LeftShift gelegt und die Schnelligkeit angepasst.
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

    private void Jump()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.VelocityChange);
        }
    }
}
