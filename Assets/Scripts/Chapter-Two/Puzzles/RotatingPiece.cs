using System.Security.Cryptography;
using UnityEngine;

public class RotatingPiece : MonoBehaviour
{
    [Header("Rotacion")]
    [SerializeField] private float rotationStep = 90f;
    [SerializeField] private float correct = 0f;
    [SerializeField] private float tolerance = 5f;

    private PuzzleManager manager;

    public void Start()
    {
        manager = FindAnyObjectByType<PuzzleManager>();
    }

    public void RotationPiece()
    {
        transform.Rotate(0, rotationStep, 0);

        if (manager != null)
        {
            manager.CheckPuzzle();
        }
    }

    public bool IsCorrect()
    {
        float y = transform.eulerAngles.y;
        return Mathf.Abs(y - correct) <= tolerance;
    }
}
