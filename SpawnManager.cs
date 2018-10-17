using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyShipPrefab;

    [SerializeField]
    private GameObject[] _powerups;

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartCoroutine(enemyCooldown());
        StartCoroutine(powerUpCooldown());
    }
 

    public void startCoroutine()
    {
        StartCoroutine(enemyCooldown());
        StartCoroutine(powerUpCooldown());
    }

    private IEnumerator enemyCooldown()
    {
        while (_gameManager.gameOver == false)
        {
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7.0f, 7.0f), 7.0f, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }

    private IEnumerator powerUpCooldown()
    {
        while(_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-7.0f, 7.0f), 7.0f, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(10.0f);
        }
    }

}
