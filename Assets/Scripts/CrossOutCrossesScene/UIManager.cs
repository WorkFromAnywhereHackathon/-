using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private FiguresSpawner figuresSpawner;

    [SerializeField] private Text timerText;
    [SerializeField] private Text amountCrossessText;
    [SerializeField] private Image initialTextImage;
    [SerializeField] private Image infoImage;
    [SerializeField] private Image exitMenuImage;
    [SerializeField] private Image endingMenuImage;

    [SerializeField] private GameObject fireworks;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private Button consentToGameButton;
    [SerializeField] private Button refusalToGameButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button infoButton;
    [SerializeField] private Button buttonNoInExitMenu;
    [SerializeField] private Button buttonYesInExitMenu;

    private float minute = 1;
    private float second = 60;

    private bool isStartTimer = false;
    private bool isInfoMenu = false;
    private bool isEndingMenu = false;


    void OnEnable()
    {
        consentToGameButton.onClick.AddListener(consentToGameButton_OnPlayGame);
        refusalToGameButton.onClick.AddListener(ChangeScene);
        exitButton.onClick.AddListener(exitButton_OnExitGame);
        infoButton.onClick.AddListener(infoButton_OnInfoText);
        buttonNoInExitMenu.onClick.AddListener(buttonNoInExitMenu_OnContinue);
        buttonYesInExitMenu.onClick.AddListener(ChangeScene);
    }

    void OnDisable()
    {
        consentToGameButton.onClick.RemoveListener(consentToGameButton_OnPlayGame);
        refusalToGameButton.onClick.RemoveListener(ChangeScene);
        exitButton.onClick.RemoveListener(exitButton_OnExitGame);
        infoButton.onClick.RemoveListener(infoButton_OnInfoText);
        buttonNoInExitMenu.onClick.RemoveListener(buttonNoInExitMenu_OnContinue);
        buttonYesInExitMenu.onClick.RemoveListener(ChangeScene);
    }

    private void consentToGameButton_OnPlayGame()
    {
        initialTextImage.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        infoButton.gameObject.SetActive(true);
        isStartTimer = true;

        figuresSpawner.SpawnObjects();
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void exitButton_OnExitGame()
    {
        exitMenuImage.gameObject.SetActive(true);
        isStartTimer = false;
    }

    private void buttonNoInExitMenu_OnContinue()
    {
        exitMenuImage.gameObject.SetActive(false);
        isStartTimer = true;
    }

    private void infoButton_OnInfoText()
    {
        isStartTimer = false;

        isInfoMenu = true;
        infoImage.gameObject.SetActive(true);
    }

    private void ChangeEndingText()
    {
        endingMenuImage.gameObject.SetActive(true);
        infoButton.gameObject.SetActive(false);


        figuresSpawner.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        mainCamera.backgroundColor = Color.black; 
        fireworks.SetActive(true);

        isEndingMenu = true;

        int amountCrosses = figuresSpawner.AmountCrosses;
        int amountCrossedOutCrosses = figuresSpawner.AmountCrossedOutCrosses;

        amountCrossessText.text = string.Format("Правильно зачёркнуто {0}/{1} крестиков", amountCrossedOutCrosses, amountCrosses);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isInfoMenu)
            {
                infoImage.gameObject.SetActive(false);
                isInfoMenu = false;

                isStartTimer = true;
            }

            if (isEndingMenu)
            {
                ChangeScene();
            }
        }


        if (isStartTimer)
        {
            bool isCompleted = false;
            if (figuresSpawner.AmountCrossedOutCrosses == figuresSpawner.AmountCrosses)
            {
                timerText.color = Color.green;
                isCompleted = true;
            }
            if (second <= 10 && !isCompleted)
            {
                timerText.color = Color.red;
            }

            if (second <= 0)
            {
                isStartTimer = false;
                timerText.gameObject.SetActive(false);
                infoButton.gameObject.SetActive(false);
                ChangeEndingText();
            }

            float timeModifier;
            if (isCompleted)
            {
                timeModifier = 10f;
            }
            else
            {
                timeModifier = 1f;
            }

            if (Convert.ToInt32(minute) > 0)
            {
                timerText.text = string.Format("{0}:00", Convert.ToInt32(minute));
                minute -= Time.deltaTime * timeModifier;
                second -= Time.deltaTime * timeModifier;
            }
            else
            {
                if (Convert.ToInt32(second) >= 10)
                {
                    timerText.text = string.Format("{0}:{1}", Convert.ToInt32(minute), Convert.ToInt32(second));
                }
                else
                {
                    timerText.text = string.Format("{0}:0{1}", Convert.ToInt32(minute), Convert.ToInt32(second));
                }

                second -= Time.deltaTime * timeModifier;
            }
        }        
    }
}
