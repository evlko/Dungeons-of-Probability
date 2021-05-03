using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMode : MonoBehaviour
{
    MoveCamera moveCamera;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        moveCamera = GetComponent<MoveCamera>();
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            moveCamera.Move();
        }

        if(Input.GetKeyDown(KeyCode.E)){
            if(gameManager.Enemy.active == true){
                gameManager.Enemy.gameObject.SetActive(false);
            }
            else{
                gameManager.Enemy.gameObject.SetActive(true);
            }
        }
    }
}
