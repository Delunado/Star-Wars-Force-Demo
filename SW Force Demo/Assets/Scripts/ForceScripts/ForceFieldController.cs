using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldController : MonoBehaviour
{

    public float speed = 10f;

    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movement * speed * Time.deltaTime;
    }

    public void SetMovement(Vector3 movement)
    {
        this.movement = movement;
    }
}
