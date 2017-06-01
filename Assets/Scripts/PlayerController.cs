using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 8.0f;
    public float maxVelocity = 4.0f;

    private Rigidbody2D playerRB;

    [SerializeField]
    private Animator mAnimator;

    // Use this for initialization
    void Start() {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {


       
    }

    private void FixedUpdate() {
        playerWalkKeyboard();
    }




    void playerWalkKeyboard() {
        float force = 0.0f;
        float velocity = Mathf.Abs(playerRB.velocity.x);

        float h = Input.GetAxisRaw("Horizontal");

        if(h > 0) {
            if(velocity < maxVelocity) {
                force = speed;
            }
            Vector3 scale = transform.localScale;
            scale.x = 1.0f;

            transform.localScale = scale;

            mAnimator.SetBool("Walk", true);

        } else if(h < 0) {

            if(velocity < maxVelocity) {
                force = -speed;
            }
            Vector3 scale = transform.localScale;
            scale.x = -1.0f;

            transform.localScale = scale;
            mAnimator.SetBool("Walk", true);
        } else {
            mAnimator.SetBool("Walk", false);
        }

        playerRB.AddForce(new Vector2(force, 0));


    }
}
