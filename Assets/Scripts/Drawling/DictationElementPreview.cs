using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DictationElementPreview : MonoBehaviour
{

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPreview(Sprite picture, int repeats)
    {
        image.sprite = picture;
        text.text = repeats.ToString();
    }
}
