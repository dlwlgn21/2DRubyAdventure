using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    enum eBotType
    {
        VERTICAL = 0,
        HORIZONTAL = 1,
        COUNT
    }

    enum eDirection
    { 
        LEFT,
        RIGHT,
        COUNT
    }

    public float Speed;
    public uint Damage;
    public float DirectonSwitchTime;
    public uint BotType;
    public ParticleSystem SmokeEffect;

    private eDirection meDirection;
    private float mDirectonSwitchTimer;

    private Rigidbody2D mRigidBody2D;
    private Animator mAnimator;
    private bool mbIsBroken;
    // Start is called before the first frame update
    void Start()
    {
        Speed = 3.0f;
        Damage = 1;
        DirectonSwitchTime = 2.0f;
        mDirectonSwitchTimer = DirectonSwitchTime;
        mRigidBody2D = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        meDirection = eDirection.RIGHT;
        mbIsBroken = true;
        Debug.Assert(mRigidBody2D != null && mAnimator != null && SmokeEffect != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (!mbIsBroken)
        {
            return;
        }
        Vector2 moveVector = mRigidBody2D.position;
        mDirectonSwitchTimer -= Time.deltaTime;
        if (mDirectonSwitchTimer < 0.0f)
        {
            mDirectonSwitchTimer = DirectonSwitchTime;
            if (meDirection == eDirection.LEFT)
            {
                meDirection = eDirection.RIGHT;
            }
            else
            {
                meDirection = eDirection.LEFT;
            }
        }

        switch (BotType)
        { 
            case 0:
            {
                if (meDirection == eDirection.LEFT)
                {
                    moveVector.y -= Speed * Time.deltaTime;
                    SetAnimatorFloat(0.0f, -1.0f);
                }
                else
                {
                    moveVector.y += Speed * Time.deltaTime;
                    SetAnimatorFloat(0.0f, 1.0f);
                }
                break;
            }
            case 1:
            {
                if (meDirection == eDirection.LEFT)
                {
                    moveVector.x -= Speed * Time.deltaTime;
                    SetAnimatorFloat(-1.0f, 0.0f);
                }
                else
                {
                    moveVector.x += Speed * Time.deltaTime;
                    SetAnimatorFloat(1.0f, 0.0f);
                }
                break;
            }
            default:
                Debug.Assert(false);
                break;
        }

        mRigidBody2D.MovePosition(moveVector);
    }

    public void Fix()
    {
        mbIsBroken = false;
        mRigidBody2D.simulated = false;
        mAnimator.SetTrigger("IsFixed");
        SmokeEffect.Stop();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController ruby = collision.gameObject.GetComponent<RubyController>();
        if (ruby != null)
        {
            ruby.DecreaseHP(Damage);
        }
    }

    private void SetAnimatorFloat(float xAmount, float yAmount)
    {
        mAnimator.SetFloat("MoveX", xAmount);
        mAnimator.SetFloat("MoveY", yAmount);
    }
}
