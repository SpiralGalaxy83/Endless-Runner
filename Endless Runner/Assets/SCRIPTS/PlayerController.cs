using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private GameObject player;
    private bool lane1;
    private bool lane2;
    private bool lane3;

    public float laneTransitionSpeed;
    private bool isMoving;


    // Input
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch;

    public float swipeRange;
    public float tapRange;






    void Start()
    {


        Init();

    }


    private void Init()
    {
        player = this.gameObject;
        lane1 = false;
        lane2 = true;
        lane3 = false;
    }





    void Update()
    {

        GetInput();
    }



    public void GetInput()
    {

#if UNITY_STANDALONE || UNITY_EDITOR


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GoRight();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GoLeft();
        }


#endif

#if UNITY_ANDROID

        // Phase Began
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }


        // Phase Moved
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentTouchPosition - startTouchPosition;

            if (!stopTouch) 
            { 
                if (Distance.x < -swipeRange)
                {
                    GoLeft();
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange)
                {
                    GoRight();
                    stopTouch = true;
                }

            }

        }


        // Phase Ended
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange)
            {
                // Tap here
            }

        }


#endif


    }



    // Go Right
    public void GoRight()
    {
        

        if (lane2 == true && lane3 == false && !isMoving)
        {
            //player.transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
            LeanTween.moveX(player, transform.position.x + 3, laneTransitionSpeed).setOnComplete(() => ResetMotion());

            lane2 = false;
            lane3 = true;
            isMoving = true;

        }
        else if (lane1 == true && lane2 == false && !isMoving)
        {
            //player.transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
            LeanTween.moveX(player, transform.position.x + 3, laneTransitionSpeed).setOnComplete(() => ResetMotion());
            lane1 = false;
            lane2 = true;
            isMoving = true;
        }

        //StartCoroutine(ResetMotion());

    }


    // Go Left
    public void GoLeft()
    {
        if (lane2 == true && lane1 == false && !isMoving)
        {
            //player.transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
            LeanTween.moveX(player, transform.position.x - 3, laneTransitionSpeed).setOnComplete(() => ResetMotion());
            lane2 = false;
            lane1 = true;
            isMoving = true;
            
        
        }
        else if (lane3 == true && lane2 == false && !isMoving)
        {
            //player.transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
            LeanTween.moveX(player, transform.position.x - 3, laneTransitionSpeed).setOnComplete(() => ResetMotion());
            lane3 = false;
            lane2 = true;
            isMoving = true;
        }

        //StartCoroutine(ResetMotion());

    }


    private void ResetMotion()
    {
        isMoving = false;
    }

}
