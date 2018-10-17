using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int powerupID; // 0 = triple shot, 1 = speed, 2 = shield

    [SerializeField]
    private AudioClip _powerupSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y > 7.0f)
        {
            Destroy(this.gameObject);
        }

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_powerupSound, Camera.main.transform.position, 1.0f);

                // enable triple shot
                if (powerupID == 0)
                {
                    player.TripleShotOn();
                }
           
                // enable speed
                else if (powerupID == 1)
                {
                    player.speedBoostOn();
                }

               // enable shields
               else if (powerupID == 2)
                {
                    player.shieldOn();
                }

            }

            Destroy(this.gameObject, 0.0f);
        }
    }
}
