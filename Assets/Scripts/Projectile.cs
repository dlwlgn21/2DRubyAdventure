using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D mRb2d;

    void Awake()
    {
        mRb2d = GetComponent<Rigidbody2D>();
        Debug.Assert(mRb2d != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 400.0f)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Fix();
        }
        Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force)
    {
        mRb2d.AddForce(direction * force);
    }
}
