using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GraphicDictation : MonoBehaviour
{


    [Serializable]
    struct DictElement
    {
       public int repeats;
       public Sprite image;      
    }

    [SerializeField] DictElement[] dictation;

    [SerializeField] int showElements = 4;

    [SerializeField] TMP_InputField inputField;


    [SerializeField] DictationElementPreview[] elementPreviews;

    [SerializeField] GameObject fireworks;


    bool isFinished = false;
    public bool IsFinished { get { return isFinished; }  }


    int linesCount = 0;

    int elementIndex = 0;
    int elementCount = 0;
    int elementsPreviewCount = 1;

    const string ShowPreviewElements = "ShowPreviewElements";


    // Start is called before the first frame update
    void Start()
    {

        showElements = PlayerPrefs.GetInt(ShowPreviewElements, 4);
        //SetPreviewElement(elementIndex, elementPreviews[0]); 
        SetPreviews(elementIndex);

        inputField.text = showElements.ToString();

        inputField.onEndEdit.AddListener(InputFieldUpdated);

        fireworks.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InputFieldUpdated(string text)
    {
        int lastValue = showElements;
        try
        {
            showElements = int.Parse(text);

            PlayerPrefs.SetInt(ShowPreviewElements, showElements);

            SetPreviews(elementIndex);

        }
        catch (FormatException)
        {
            showElements = lastValue;
        }
    }

    public void LineAdded()
    {
        if (elementIndex < dictation.Length)
        {
            linesCount++;
            elementCount++;

            if (elementCount >= dictation[elementIndex].repeats)
            {
                elementIndex++;
                elementCount = 0;

                //SetPreviewElement(elementIndex, elementPreviews[0]);

                elementsPreviewCount++;
                if (elementsPreviewCount > showElements || elementsPreviewCount > elementPreviews.Length)
                {
                    elementsPreviewCount = 1;
                    SetPreviews(elementIndex);
                }
            }
        }

        if (elementIndex == dictation.Length)
        {
            isFinished = true;
            fireworks.SetActive(true);
        }

    }


    void SetPreviewElement(int index, DictationElementPreview elementPreview)
    {
        if (index < dictation.Length)
        {
            elementPreview.SetPreview(dictation[index].image, dictation[index].repeats);
        }
    }


    void SetPreviews(int startIndex)
    {
        for (int i = 0; i < elementPreviews.Length; i++)
        { 

            if (startIndex + i < dictation.Length && i < showElements)
            {
                elementPreviews[i].gameObject.SetActive(true);
                SetPreviewElement(startIndex + i, elementPreviews[i]);
            }
            else elementPreviews[i].gameObject.SetActive(false);
        }
    }



}
