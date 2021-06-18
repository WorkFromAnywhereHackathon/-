using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class FiguresSpawner : MonoBehaviour
{
    private const int precentScreenWidth = 5;
    private const int precentScreenHeight = 25;
    private const int minCrossAmount = 8;
    private const int maxCrossAmount = 12;

    [SerializeField] private Camera camera;

    private int rows = 7;
    private int columns = 10;

    private int amountCrosses;
    public int AmountCrosses
    {
        get
        {
            return amountCrosses;
        }
    }

    private int amountCrossedOutCrosses;
    public int AmountCrossedOutCrosses
    {
        get
        {
            return amountCrossedOutCrosses;
        }
    }

    [SerializeField] private Figure[] figuresType = new Figure[6];
    private Figure[,] figures = new Figure[7, 10];

    public void SpawnObjects()
    {
        float firstPositionX = Screen.width * precentScreenWidth / 100;
        float firstPositionY = Screen.height * precentScreenHeight / 100;

        float playingAreaWidth = Screen.width - firstPositionX * 2;
        float playingAreaHeight = Screen.height - firstPositionY * 2;

        float distanceBetweenColumn = playingAreaWidth / columns;
        float distanceBetweenRows = playingAreaHeight / rows;

        float secondPositionFigureX = firstPositionX + distanceBetweenColumn;
        float secondPositionFigureY = firstPositionY + distanceBetweenRows;

        Vector2 firstPosition = camera.ScreenToWorldPoint(new Vector2(firstPositionX, firstPositionY));
        Vector2 secondPosition = camera.ScreenToWorldPoint(new Vector2(secondPositionFigureX, secondPositionFigureY));

        Vector2 distanceBetweenFigures = secondPosition - firstPosition;

        float indentX = distanceBetweenFigures.x / 2;
        float indentY = distanceBetweenFigures.y / 2;

        CreateCross(firstPosition, indentX, indentY, distanceBetweenFigures);

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if (figures[row, column] == null)
                {
                    int num = Random.Range(1, figuresType.Length);

                    Figure figure = Instantiate(figuresType[num], transform);
                    SetPositionFigure(figure, firstPosition, indentX, indentY, distanceBetweenFigures, column, row);
                    figures[row, column] = figure;
                }
            }
        }
    }


    private void CreateCross(Vector2 firstPosition, float indentX, float indentY, Vector2 distanceBetweenFigures)
    {
        amountCrosses = Random.Range(minCrossAmount, maxCrossAmount);

        for (int i = 0; i < amountCrosses; i++)
        {
            CalculateIndexFigure(firstPosition, indentX, indentY, distanceBetweenFigures);
        }
    }

    private void CalculateIndexFigure(Vector2 firstPosition, float indentX, float indentY, Vector2 distanceBetweenFigures)
    {
        int firstNumberElement = Random.Range(0, rows - 1);
        int secondNumberElement = Random.Range(0, columns - 1);

        if (figures[firstNumberElement, secondNumberElement] == null)
        {
            figures[firstNumberElement, secondNumberElement] = Instantiate(figuresType[0], transform);
            SetPositionFigure(figures[firstNumberElement, secondNumberElement], firstPosition, indentX, indentY, distanceBetweenFigures, secondNumberElement, firstNumberElement);
        }
        else
        {
            CalculateIndexFigure(firstPosition, indentX, indentY, distanceBetweenFigures);
        }
    }

    private void SetPositionFigure(Figure figure, Vector2 firstPosition, float indentX, float indentY, Vector2 distanceBetweenFigures, int column, int row)
    {
        figure.transform.position = new Vector2(firstPosition.x + indentX + distanceBetweenFigures.x * column, firstPosition.y + indentY + distanceBetweenFigures.y * row);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D raycastHit = Physics2D.Raycast(mousePosition, Vector3.forward, -10.0f);

                if (raycastHit.collider != null)
                {
                    if (raycastHit.collider.GetComponent<Figure>().FigureType == FigureType.Cross)
                    {
                        Figure figure = raycastHit.collider.GetComponent<Figure>();
                        if (!figure.isCrossOut)
                        {
                            Transform childFigure = figure.transform.GetChild(0);
                            childFigure.gameObject.SetActive(true);
                            figure.isCrossOut = true;
                            amountCrossedOutCrosses++;
                        }
                    }
                }
            }
        }
    }
}


