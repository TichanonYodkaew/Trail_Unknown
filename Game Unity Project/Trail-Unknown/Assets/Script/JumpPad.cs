using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField]private float bounce = 5f;
    [SerializeField] private AudioSource jumppadSFX;
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        Rigidbody2D rigid2D = coll.gameObject.GetComponent<Rigidbody2D>();

        if (rigid2D != null)
        {
            jumppadSFX.Play();
            Vector2 force = new Vector2(0f, Mathf.Sqrt(2f * bounce * Mathf.Abs(Physics2D.gravity.y)));

            float angle = transform.rotation.eulerAngles.z;

            force = Quaternion.Euler(0, 0, angle) * force;

            rigid2D.velocity = force;
        }
    }

}
