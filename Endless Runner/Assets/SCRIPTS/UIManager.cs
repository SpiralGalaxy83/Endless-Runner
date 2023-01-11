using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    static public UIManager instance;

    [SerializeField] private RectTransform fader;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject coinsIcon;
    [SerializeField] private Text messageText;
    [SerializeField] private Text distanceText;
    [SerializeField] private Text coinsText;


    private void SubscribeEvents()
    {
        GameEvents.instance.OnStartGame += StartGame;
        GameEvents.instance.OnStartRound += StartCountingDistance;
        GameEvents.instance.OnCoinPickUp += GetCoins;
        GameEvents.instance.OnGameOver += EndRound;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.instance.OnStartGame -= StartGame;
        GameEvents.instance.OnStartRound -= StartCountingDistance;
        GameEvents.instance.OnCoinPickUp -= GetCoins;
        GameEvents.instance.OnGameOver -= EndRound;
    }


    private void OnEnable()
    {
        SubscribeEvents();
    }


    private void OnDisable()
    {
        UnsubscribeEvents();
    }







    private void Awake()
    {
        instance = this;
       
    }

    private void Start()
    {
        fadeIn(2);
    }


    #region FIXED

    private void StartGame()
    {
  
        messageText.text = "GET READY";
        distanceText.text = 0.ToString();
        FadeOutRectTransform(messageText.GetComponent<RectTransform>(), 0);
        messageText.gameObject.SetActive(true);

        FadeOutRectTransform(playButton.GetComponent<RectTransform>(), 0.4f);
        FadeInRectTransform(messageText.GetComponent<RectTransform>() , 0.6f);
        ScaleEffect(messageText.gameObject , new Vector2 (2 , 2) , 0.6f);

        StartCoroutine(SetTextCoroutine(messageText , "GO" , 2.8f));
        StartCoroutine(DisableObjectCoroutine(messageText.gameObject , 3.6f));
        StartCoroutine(CancelTweenCoroutine(messageText.gameObject, 3.6f));

        ActivateGameplayUI();

    }


    public void StartCountingDistance()
    {
        InvokeRepeating("CountDistance", 0, 0.1f);
    }

    public void CountDistance()
    {
        distanceText.text = GameplayManager.instance.distance.ToString();
    }

    public void GetCoins()
    {

        coinsText.text = GameplayManager.instance.coins.ToString();
    }

    private void EndRound()
    {
       
        StartCoroutine(EndRoundCoroutine());
    }

    private IEnumerator EndRoundCoroutine()
    {

        yield return new WaitForSeconds(1);

        messageText.text = "GAME OVER";
        messageText.gameObject.SetActive(true);
        ScaleEffect(messageText.gameObject, new Vector2(2f , 2f), 0.8f);

        yield return new WaitForSeconds(2);
        fadeOut(2);
    }


    #endregion








    #region REUSABLE

    // Fade in a RectTransform in seconds
    private void FadeInRectTransform(RectTransform rectTransform , float seconds)
    {
        LeanTween.alpha(rectTransform , 1 , seconds);

    }


    
    // Fade out a RectTransform in seconds
    private void FadeOutRectTransform(RectTransform rectTransform, float seconds)
    {
        LeanTween.alpha(rectTransform, 0 , seconds);
    }




    // Disable an Object after Seconds Coroutine
    private IEnumerator DisableObjectCoroutine(GameObject objectToDisable, float seconds)
    {

        yield return new WaitForSeconds(seconds);
        objectToDisable.SetActive(false);
    }

    


    // Sets the text of a textObject after a delay (Coroutine)
    private IEnumerator SetTextCoroutine(Text textObject ,string text , float delay)
    {
        yield return new WaitForSeconds(delay);
        textObject.text = text;

    }



    // Cancels a Tween GameObject after seconds Coroutine
    private IEnumerator CancelTweenCoroutine(GameObject objectToCancel, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        LeanTween.cancel(objectToCancel);
    }



    // Scale an object from his vector2 to and ping pong loops
    private void ScaleEffect(GameObject scaleObject, Vector2 to , float timestep)
    {
        LeanTween.scale(scaleObject, to, timestep).setLoopPingPong();
    }


  



    //// Scales a GameObject , to(Vector3) in seconds
    //private void ScaleObject(GameObject scalingObject , Vector3 to, float seconds)
    //{
    //    LeanTween.scale(scalingObject, to, seconds);
    //}


    #endregion










    private void ActivateGameplayUI()
    {
        distanceText.gameObject.SetActive(true);
        coinsText.gameObject.SetActive(true);
        coinsIcon.SetActive(true);
    }






    public void fadeIn(float seconds)
    {
        LeanTween.alpha(fader, 0, seconds);
        StartCoroutine(DisableObjectCoroutine(fader.gameObject, seconds));
    }


    public void fadeOut(float seconds)
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 1, seconds);

    }


   






}
