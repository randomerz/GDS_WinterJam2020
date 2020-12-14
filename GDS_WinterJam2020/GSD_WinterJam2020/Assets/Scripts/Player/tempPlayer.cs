using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempPlayer : MonoBehaviour
{
    public float moveSpeed;


    public Animator playerAnim;
    public SpriteRenderer playerSprite;

    private float hoMovement;
    private float vertMovement;
    private Vector3 moveDir;

    void Start()
    {
        
    }
    
    void Update()
    {
        hoMovement = Input.GetAxisRaw("Horizontal");
        vertMovement = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(hoMovement, vertMovement).normalized;

        playerAnim.SetBool("isRunning", moveDir.magnitude > 0);

        // mouse
        // Project the mouse point into world space at
        //   at the distance of the player.
        Vector3 v3Pos = Input.mousePosition;
        v3Pos.z = (transform.position.z - Camera.main.transform.position.z);
        v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
        v3Pos = v3Pos - transform.position;
        float fAngle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
        if (fAngle < 0.0f) fAngle += 360.0f;
        //Debug.Log("1) " + fAngle + " " + (90 < fAngle && fAngle < 270));

        playerSprite.flipX = 90 < fAngle && fAngle < 270;
    }

    private void FixedUpdate()
    {
        transform.position += moveSpeed * Time.deltaTime * moveDir;
    }
}
