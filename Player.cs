using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool speedBoost = false;
    public bool tripleShot = false;
    public bool shield = false;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    public int lives = 3;

    [SerializeField]
    private GameObject[] _engines;

    private UIManager _UIManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _laserShotSound;
	// Use this for initialization
	private void Start ()
    {
        transform.position = new Vector3(0.0f, -3.57f, 0.0f);

        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _laserShotSound = GetComponent<AudioSource>();

        if (_spawnManager != null)
        {
            _spawnManager.startCoroutine();
        }

        if (_UIManager != null)
        {
            _UIManager.updateLives(lives);
        }
	}
	
	// Update is called once per frame
	private void Update ()
    {
        Movement();
        Shoot();
        shieldAnimation();
    }

    private void Movement()
    {
        float UserInputH = Input.GetAxis("Horizontal");
        float UserInputV = Input.GetAxis("Vertical");

        if (speedBoost)
        {
            transform.Translate(Vector3.right * _speed * 2.5f * UserInputH * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 2.5f * UserInputV * Time.deltaTime);
        }
        else if (speedBoost == false)
        {
            transform.Translate(Vector3.right * _speed * UserInputH * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * UserInputV * Time.deltaTime);
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 11.8f)
        {
            transform.position = new Vector3(11.8f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.8f)
        {
            transform.position = new Vector3(-11.8f, transform.position.y, 0);
        }


    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > _nextFire)
            {
                
                Instantiate(_laserPrefab, transform.position + new Vector3(0.01f, 1.08f, 0), Quaternion.identity);
                
                _nextFire = Time.time + _fireRate;
                _laserShotSound.Play();

                if (tripleShot)
                {
                    Instantiate(_laserPrefab, transform.position + new Vector3(0.802f, -0.22f, 0), Quaternion.identity);
                    Instantiate(_laserPrefab, transform.position + new Vector3(-0.87f, -0.22f, 0), Quaternion.identity);

                }
            }
        }
    }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Enemy" )
            {
                EnemyAI enemy = other.GetComponent<EnemyAI>();

                if (enemy != null && shield == false)
                {
                    lives -= 1;
                    _UIManager.updateLives(lives);
                    if (lives == 2)
                    {
                     _engines[Random.Range(0, 2)].SetActive(true);
                    }
                    else if (lives == 1)
                    {
                    _engines[0].SetActive(true);
                    _engines[1].SetActive(true);
                    }
                    if (lives == 0)
                    {
                        Instantiate(_explosion, transform.position, Quaternion.identity);
                        _UIManager.showTitle();
                        _gameManager.gameOver = true;
                        Destroy(this.gameObject, 0.0f);
                    }
                }
            }
        }

    public void TripleShotOn()
    {
        tripleShot = true;

        StartCoroutine(TripleShotCooldown());
    }

   public IEnumerator TripleShotCooldown()
    {
        yield return new WaitForSeconds(5.0f);

        tripleShot = false;
    }

    public void speedBoostOn()
    {
        speedBoost = true;
      
        StartCoroutine(SpeedBoostCooldown());
    }

    public IEnumerator SpeedBoostCooldown()
    {
        yield return new WaitForSeconds(5.0f);

        speedBoost = false;
    }

    public void shieldOn()
    {
        shield = true;
        StartCoroutine(ShieldCooldown());
    }

    public IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(10.0f);
        shield = false;
    }

    private void shieldAnimation()
    {
        if (shield)
        {
            _shieldGameObject.SetActive(true);
        }
        else
        {
            _shieldGameObject.SetActive(false);
        }
    }
}
