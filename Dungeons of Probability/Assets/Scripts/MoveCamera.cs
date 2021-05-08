using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour
{
    public int speed = 5;
    int currentPoint = 0;
    public Transform enemy;
    public enum PointStatus{
        battle,
        rotateRight,
        rotateLeft,
        end
    };

    [Serializable]
    public struct Points{
        public Transform point;
        public PointStatus pointStatus;
        public float cameraRotation;
    }

    [SerializeField]
    public Points[] points;

    Transform movePoint;
    bool moving = false;
    bool rotating = false;
    float angelsToRotate = 0;
    GameManager gameManager;
    UIManager uiManager;
    Vector3 angles;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        uiManager = GetComponent<UIManager>();
        currentPoint = PlayerPrefs.GetInt("CameraPoint");
        movePoint = points[currentPoint].point;
        MoveToCurrentPoint();
    }

    void MoveToCurrentPoint(){
        transform.position = new Vector3(points[currentPoint].point.position.x, points[currentPoint].point.position.y, points[currentPoint].point.position.z);
        Vector3 p = transform.eulerAngles;
        p.y = points[currentPoint].cameraRotation;
        transform.eulerAngles = p;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving == true){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, step);
        }

        else if (rotating == true){
            if(angelsToRotate < 0){
                angles.y -= 1.5f;
                angelsToRotate += 1.5f;
            }
            else{
                angles.y += 1.5f;
                angelsToRotate -= 1.5f;
            }
            gameObject.transform.rotation = Quaternion.Euler(angles);
            if(angelsToRotate == 0){
                rotating = false;
                Move();
            }
        }

        if(Vector3.Distance(movePoint.position, transform.position) == 0 && moving == true){
            moving = false;
            switch(points[currentPoint].pointStatus){
                case PointStatus.battle:
                    enemy.gameObject.SetActive(true);
                    gameManager.nextBattle = true;
                    gameManager.BeginFight();
                    uiManager.DialogueButton.GetComponent<Button>().interactable = true;
                    break;
                case PointStatus.rotateLeft:
                    Rotate(-90f);
                    break;
                case PointStatus.rotateRight:
                    Rotate(90f);
                    break;
                case PointStatus.end:
                    uiManager.Win();
                    uiManager.DialogueButton.GetComponent<Button>().interactable = true;
                    break;
            }
        }
    }

    public void Move(){
        if(moving == false){
            uiManager.DialogueButton.GetComponent<Button>().interactable = false;
            currentPoint = currentPoint + 1;
            PlayerPrefs.SetInt("CameraPoint", currentPoint);
            enemy.gameObject.SetActive(false);
            movePoint = points[currentPoint].point;
            moving = true;
        }
    }

    void Rotate(float newAngels){
        angles = transform.rotation.eulerAngles;
        rotating = true;
        angelsToRotate = newAngels;
    }
}
