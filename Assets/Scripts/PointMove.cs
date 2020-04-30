using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D rbody;
    private Vector2 moveVector;
    private float distance;
    // Update is called once per frame

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        distance = (transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude;
        moveVector = (transform.position - GameObject.FindGameObjectWithTag("Player").transform.position)/(distance * distance * distance * distance);
        Vector2 position = rbody.position;
        position += moveVector * speed * Time.deltaTime;
        rbody.MovePosition(position);
    }
}
