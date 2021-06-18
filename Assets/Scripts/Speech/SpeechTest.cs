using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextSpeech;
using UnityEngine.Android;

public class SpeechTest : MonoBehaviour
{

    private Image image;

    const string LANG_CODE = "ru-RU";



    [SerializeField] TMPro.TextMeshProUGUI detectedText;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        SpeechToText.instance.Setting(LANG_CODE);

        detectedText.text = "";

        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;

        CheckPermission();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckPermission()
    {
        #if UNITY_ANDROID

        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }

        #endif
    }

    public void StartListening()
    {

        SpeechToText.instance.StartRecording();
        detectedText.text = "Запись...";

        //effect.SetActive(true);
        // scale = 1;
        // sample.StartRecording();

        image.color = Color.green;
    }

    public void StopListening()
    {

        SpeechToText.instance.StopRecording();
        detectedText.text = "Запись завершена.";

        //  effect.SetActive(false);
        // sample.StopRecording();

        image.color = Color.white;
    }


    void OnFinalSpeechResult(string result)
    {
        detectedText.text = result;
    }
}
