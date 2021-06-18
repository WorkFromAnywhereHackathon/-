using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerInMainMenuScene : MonoBehaviour
{
    [SerializeField] private Button crossOutCrossesButton;
    [SerializeField] private Button drawingButton;
    [SerializeField] private Button speechButton;

    void OnEnable()
    {
        crossOutCrossesButton.onClick.AddListener(CrossOutCrossesButton_OnSceneCrossOutCrosses);
        drawingButton.onClick.AddListener(DrawingButton_OnSceneDrawing);
        speechButton.onClick.AddListener(SpeechButton_OnSceneSpeech);
    }

    void OnDisable()
    {
        crossOutCrossesButton.onClick.RemoveListener(CrossOutCrossesButton_OnSceneCrossOutCrosses);
        drawingButton.onClick.RemoveListener(DrawingButton_OnSceneDrawing);
        speechButton.onClick.RemoveListener(SpeechButton_OnSceneSpeech);
    }

    private void CrossOutCrossesButton_OnSceneCrossOutCrosses()
    {
        SceneManager.LoadScene("CrossOutCrosses");
    }

    private void DrawingButton_OnSceneDrawing()
    {
        SceneManager.LoadScene("Drawing");
    }


    private void SpeechButton_OnSceneSpeech()
    {
        SceneManager.LoadScene("Speech");
    }

    void Update()
    {
        
    }
}
