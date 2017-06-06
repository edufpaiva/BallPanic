using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {
    [SerializeField]
    private float arrowSpeed = 4.0f;
    private bool canShootStickyArrow;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        ShootArrow();
    }

    void ShootArrow() {
        Vector3 temp = transform.position;
        temp.y += arrowSpeed * Time.unscaledDeltaTime;
        transform.position = temp;

    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "TopBrick") {
            gameObject.SetActive(false);
        }
    }


}
