using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Rigidbody rb;
    bool isForced;
    Transform oldParent;

    Camera fpsCamera;

    public float ForceFieldLaunch = 6f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        oldParent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (isForced)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void SetCamera(Camera camera)
    {
        fpsCamera = camera;
    }

    public void IsForced(bool value)
    {
        if (value)
        {
            rb.useGravity = false;
            isForced = true;
            rb.velocity = Vector3.zero;

            //Adds a random rotation to the object while you are "forcing" it
            rb.AddTorque(new Vector3(Random.Range(70, 100), Random.Range(70, 100), Random.Range(70, 100)));
        } else
        {
            rb.useGravity = true;
            isForced = false;
            transform.parent = oldParent;
        }
    }

    public void Launch(float launch_force)
    {
        if (fpsCamera)
        {
            rb.AddForce((fpsCamera.transform.forward + (Vector3.up / 4)) * launch_force, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Force Field"))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce((other.transform.forward + (Vector3.up / 2)) * Random.Range(1.1f, 1.8f) * ForceFieldLaunch, ForceMode.Impulse);
        }
    }

}
