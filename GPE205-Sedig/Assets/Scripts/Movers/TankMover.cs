using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    private Rigidbody rb;
    public override void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

  
    public override void Move(Vector3 direction, float speed)
    {
        Vector3 movementVector = direction.normalized * speed * Time.deltaTime;
        //rb.MovePosition(rb.position + movementVector);
        rb.AddForce(movementVector,ForceMode.Impulse);
    }
    public override void Rotate(float speed)
    {
        transform.Rotate(new Vector3(0,speed * Time.deltaTime,0));
    }
    public override void MouseRotate(float speed)
    {
        transform.Rotate(new Vector3(0,speed * Time.deltaTime,0));
    }
}
