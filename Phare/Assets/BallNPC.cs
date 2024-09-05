using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNPC : MonoBehaviour
{
    public float velocity;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        velocity = rb.velocity.y;
    }
}
