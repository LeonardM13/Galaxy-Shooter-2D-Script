using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public bool gameOver = true;
    public GameObject player;

    private UIManager _UIManager;

    private void Start()
    {
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    }

    void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, new Vector3(0.0f, 3.57f, 0.0f), Quaternion.identity);
                gameOver = false;
                _UIManager.hideTitle();
            }
        }
    }

}
