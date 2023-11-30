using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public uint Damage;
    // Start is called before the first frame update
    void Start()
    {
        Damage = 1;
        Debug.Assert(Damage != 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        RubyController ruby = collision.GetComponent<RubyController>();
        if (ruby != null)
        {
            ruby.DecreaseHP(Damage);
        }
    }
}
