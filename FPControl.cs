#region System Usage 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
#endregion

public class FPControl : MonoBehaviour {

    public event Action addxxx;
    public float mouseSensitivity = 25f;
    public float walkspeed = 4f;
    public float jumpForce = 220f;
    public LayerMask groundMask; 
    float verticalLookRotation ;
    bool grounded;

    public float score = 0;
    public Text ch;

    Vector3 moveAmount;
    Vector3 smoothMove;

    Rigidbody rb;
    Transform cameraT;
    void Start ()
	{
        rb = GetComponent<Rigidbody>();
        cameraT = Camera.main.transform; 
	}
	
	
	void Update ()
	{
        //score = Time.time;
        ch.text = " " + score.ToString("0");



        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation,-75,75);
        cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized;
        Vector3 targetMoveAmount = moveDir * walkspeed;
        moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMove,.15f);

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce);
            }
            
        }

        grounded = false;
        Ray ray = new Ray(transform.position,-transform.up);
        RaycastHit _hit;
        if (Physics.Raycast(ray,out _hit , 1 + .1f,groundMask))
        {
            grounded = true;
        }
    }
    


    private void FixedUpdate()
    {


        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime) ;


    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag=="phinx")
        {
            Destroy(col.gameObject);
            score += 10;
            if (addxxx !=null)
            {
                
                addxxx();
            }
        }
    }

}
