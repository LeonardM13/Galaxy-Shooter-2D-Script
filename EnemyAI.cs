using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private float _speed = 4.0f;

    [SerializeField]
    private GameObject _explosion;

    private UIManager _UIManager;

    [SerializeField]
    private AudioClip _explosionSound;

	// Use this for initialization
	void Start ()
    {
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.35f)
        {
            transform.position = new Vector3(Random.Range(-11.49f, 11.49f), 5.61f, 0.0f); 
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if ( other.tag == "Player" || other.tag == "Laser")
        {
            Player player = other.GetComponent<Player>();
            _speed = 0.0f;
            Instantiate(_explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_explosionSound, Camera.main.transform.position, 1.0f);
            Destroy(this.gameObject);
          
            _UIManager.updateScore();  
        }
    }
}
