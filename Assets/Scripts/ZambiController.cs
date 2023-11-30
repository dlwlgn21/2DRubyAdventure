using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZambiController : MonoBehaviour
{

    public GameObject dialogBox;
    private float mDisplayTimer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(dialogBox != null);
        mDisplayTimer = -1.0f;
        dialogBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (mDisplayTimer >= 0.0f)
        {
            mDisplayTimer -= Time.deltaTime;
            if (mDisplayTimer < 0.0f)
            {
                dialogBox.SetActive(false);
            }
        }
    }
    public void DisplayDialog()
    {
        dialogBox.SetActive(true);
        mDisplayTimer = 4.0f;
    }
}
