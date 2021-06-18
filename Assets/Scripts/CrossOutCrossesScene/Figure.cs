using UnityEngine;

public class Figure : MonoBehaviour
{
    [SerializeField] private FigureType figureType;
    public FigureType FigureType => figureType;

    public bool isCrossOut = false;

}
