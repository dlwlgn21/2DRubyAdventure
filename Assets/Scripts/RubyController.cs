using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RubyController : MonoBehaviour
{
    public float Speed;
    public int MaxHP;
    public int CurrrentHP { get; private set; }
    public float InvincibleTime;
    public GameObject mProjectilePrefab;


    private bool mbIsInvincible;
    private float mInvincibleTimer;
    private Rigidbody2D mRigidBody2d;
    private Animator mAnimator;
    private Vector2 mLookDir;
    // Start is called before the first frame update
    void Start()
    {
        MaxHP = 5;
        CurrrentHP = MaxHP;
        InvincibleTime = 2.0f;
        mInvincibleTimer = InvincibleTime;
        mbIsInvincible = false;
        mLookDir = new Vector2(1, 0);
        mRigidBody2d = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        Debug.Assert(mRigidBody2d != null && mAnimator != null && mProjectilePrefab != null);
    }

    // Update is called once per frame
    void Update()
    {
        processInvincible();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 moveVector = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(moveVector.x, 0.0f) || !Mathf.Approximately(0.0f, moveVector.y))
        {
            // x 또는 y가 0이 아니면, 즉, 움직이면.
            mLookDir.Set(moveVector.x, moveVector.y);
            mLookDir.Normalize();
        }
        mAnimator.SetFloat("Look X", mLookDir.x);
        mAnimator.SetFloat("Look Y", mLookDir.y);
        mAnimator.SetFloat("Speed", moveVector.magnitude);

        //Vector2 movingVector = transform.position;
        //movingVector.x += Speed * horizontal * Time.deltaTime;
        //movingVector.y += Speed * vertical * Time.deltaTime;
        //transform.position = movingVector;
        Vector2 position = mRigidBody2d.position;
        position += moveVector * Speed * Time.deltaTime;
        mRigidBody2d.MovePosition(position);
 

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                mRigidBody2d.position + Vector2.up * 0.2f,
                mLookDir,
                1.5f,
                LayerMask.GetMask("NPC")
            );
            if (hit.collider != null)
            {
                Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
                ZambiController zambi = hit.collider.GetComponent<ZambiController>();
                if (zambi != null)
                {
                    zambi.DisplayDialog();
                }
            }
        }
    }

    public void IncreaseHP(uint amount)
    {
        CurrrentHP = (int)Mathf.Clamp(CurrrentHP + amount, 0, MaxHP);
        UIHealthBar.Instance.SetValue(CurrrentHP / (float)MaxHP);
        Debug.Log($"{CurrrentHP} / {MaxHP}");
    }

    public void DecreaseHP(uint amount)
    {
        if (mbIsInvincible)
        {
            return;
        }
        mbIsInvincible = true;
        mInvincibleTimer = InvincibleTime;
        CurrrentHP = (int)Mathf.Clamp(CurrrentHP - amount, 0, MaxHP);
        UIHealthBar.Instance.SetValue(CurrrentHP / (float)MaxHP);
        mAnimator.SetTrigger("Hit");
        Debug.Log($"{CurrrentHP} / {MaxHP}");
    }


    private void processInvincible()
    {
        if (mbIsInvincible)
        {
            mInvincibleTimer -= Time.deltaTime;
            if (mInvincibleTimer < 0.0f)
            {
                mbIsInvincible = false;
            }
        }
    }

    private void Launch()
    {
        GameObject gameObject = Instantiate(mProjectilePrefab, mRigidBody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Launch(mLookDir, 300.0f);
            mAnimator.SetTrigger("Launch");
        }
    }

}
