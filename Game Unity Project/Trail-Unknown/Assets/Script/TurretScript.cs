using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    [SerializeField] private float Range;

    [SerializeField] private Transform Target;

    bool Detected = false;

    Vector2 Direction;

    [SerializeField] private GameObject AlarmLight;

    [SerializeField] private GameObject Gun;

    [SerializeField] private GameObject Bullet;

    [SerializeField] private float FireRate;

    float nextTimeToFire = 0;

    [SerializeField] private Transform ShootPoint;

    [SerializeField] private float Force;

    [SerializeField] private AudioSource shootSFX;

    public PlayerLife playerlife;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = Target.position;

        Direction = targetPos - (Vector2)transform.position;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range);

        if (rayInfo)
        {
            if(rayInfo.collider.gameObject.tag == "Player") {

                if (Detected == false)
                {
                    Detected= true;
                    AlarmLight.GetComponent<SpriteRenderer>().color= Color.red;
                }

            }

            else
            {
                if (Detected == true)
                {
                    Detected = false;
                    AlarmLight.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
        }
        if (Detected)
        {
            Gun.transform.up = - Direction;
            if(Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time+1/FireRate;
                Shoot();
            }
            else if (playerlife.isDie == true)
            {
                Detected = false;
                AlarmLight.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }

    void Shoot()
    {
       shootSFX.Play();
       GameObject BulletIns = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
       BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
