using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public uint Amount;

    void Start()
    {
        Amount = 5;
        Debug.Assert(Amount != 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Enter this Collider2D {collision.gameObject.name}");
        RubyController ruby = collision.GetComponent<RubyController>();
        if (ruby != null)
        {
            if (ruby.CurrrentHP < ruby.MaxHP)
            {
                ruby.IncreaseHP(Amount);
                Destroy(gameObject);
            }
        }

    }
}
