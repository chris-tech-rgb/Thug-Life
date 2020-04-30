using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rbody;
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 0.6f);
    }

    // Update is called once per frame

    public void Move(Vector2 aimDirection)
    {
        rbody.AddForce(aimDirection * speed * 100);
        float rotZ = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<FinalController>().kills++;
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}
