using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] float speed = 10;
    [SerializeField] float boundary = 10;
    int width;
    int height;

    bool isMoving = false;
    public bool IsMoving { get { return isMoving; } }

    // Start is called before the first frame update
    void Start()
    {

        width = Screen.width;
        height = Screen.height;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > width - boundary)
            {
                transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0));
                isMoving = true;
            }

            if (Input.mousePosition.x < 0 + boundary)
            {
                transform.Translate(new Vector3(-Time.deltaTime * speed, 0, 0));
                isMoving = true;
            }

            if (Input.mousePosition.y > height - boundary)
            {
                transform.Translate(new Vector3(0, Time.deltaTime * speed, 0));
                isMoving = true;
            }

            if (Input.mousePosition.y < 0 + boundary)
            {
                transform.Translate(new Vector3(0, -Time.deltaTime * speed, 0));
                isMoving = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(ReleaseLock());
        }
    }


    IEnumerator ReleaseLock()
    {
        yield return new WaitForSeconds(0.1f);
        isMoving = false;
    }
}
