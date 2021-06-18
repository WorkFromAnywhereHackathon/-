using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawling : MonoBehaviour
{
    private const float lineLength = 0.5f;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] GraphicDictation dictation;

    [SerializeField] CameraMovement cameraMovement;




    private List<Vector3> points = new List<Vector3>();

    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 distanceBetweenPoint;

    private int index = 0;

    private bool drawling = false;

    void Awake()
    {
        points.Add(Vector3.zero);
        //points[0] = Vector3.zero;
    }

    private void DrawlingLine()
    {
        bool lineAdded = false;
        if (drawling)
        {
            Vector3 pointFirst = points[index];
            Vector3 pointTwo = new Vector2();

            if (distanceBetweenPoint.x > lineLength)
            {
                pointTwo = new Vector3(pointFirst.x + lineLength, pointFirst.y);
                lineAdded = true;
            }
            else if (distanceBetweenPoint.y > lineLength)
            {
                pointTwo = new Vector3(pointFirst.x, pointFirst.y + lineLength);
                lineAdded = true;
            }  
            else if (distanceBetweenPoint.y < -lineLength)
            {
                pointTwo = new Vector3(pointFirst.x, pointFirst.y - lineLength);
                lineAdded = true;
            }
            else if (distanceBetweenPoint.x < -lineLength)
            {
                pointTwo = new Vector3(pointFirst.x - lineLength, pointFirst.y);
                lineAdded = true;
            }

            //Debug.Log("Index = " + index +  " Points length: " + points.Count);

            if (lineAdded && !dictation.IsFinished)
            {
                points.Add(pointTwo);
                dictation.LineAdded();

                lineRenderer.positionCount = points.Count;

                lineRenderer.SetPositions(points.ToArray());

                lineRenderer.loop = false;

                index++;
            }

            drawling = false;
        }
    }

    void Update()
    {

        if (!cameraMovement.IsMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                endPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                distanceBetweenPoint = endPoint - startPoint;

                drawling = true;

                DrawlingLine();
            }
        }

    }
}
