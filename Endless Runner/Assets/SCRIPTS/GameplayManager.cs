using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameplayManager : MonoBehaviour
{
    static public GameplayManager instance;

    [Header("GENERAL")]
    [SerializeField] private GameObject player;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Collider playerCollider;
    
    public int distance;
    public int coins;




    private void SubscribeEvents()
    {
        GameEvents.instance.OnStartGame += GetReady;
        //GameEvents.instance.OnStartRound += StartRunning
        GameEvents.instance.OnCoinPickUp += PickupCoin;
        GameEvents.instance.OnGameOver += EndRound;
    }

    private void UnSubscribeEvents()
    {
        GameEvents.instance.OnStartGame -= GetReady;
        //GameEvents.instance.OnStartRound -= StartRunning;
        GameEvents.instance.OnCoinPickUp -= PickupCoin;
        GameEvents.instance.OnGameOver -= EndRound;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }






    private void Awake()
    {
        instance = this;
        playerAnimator = player.GetComponent<Animator>();
    }

    private void Start()
    {

        Init();
    }


    private void Init()
    {
        distance = 0;
        coins = 0;
    }



    private void GetReady()
    {

        playerAnimator.Play("Happy Idle" , 0);
        Invoke("StartRunning", 3);
        
    }


    private  void StartRunning()
    {
        GameEvents.instance.StartRound();
        playerAnimator.Play("Running", 0);
        InvokeRepeating("CountDistance" , 0 ,0.1f);
    }

    private void CountDistance()
    {
        distance += 1;
    }
    

    
    public void PickupCoin()
    {
        coins += 1;
    }


    private void EndRound()
    {

        CancelInvoke();
        playerAnimator.Play("Fall Flat" , 0 , 0.2f);
        playerCollider.enabled = false;
        playerController.enabled = false;

        StartCoroutine(EndRoundCoroutine());
    }


    private IEnumerator EndRoundCoroutine()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }


}
