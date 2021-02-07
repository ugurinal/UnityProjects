using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] _puzzlePiece;

    public GameObject[] PuzzlePiece { get => _puzzlePiece; }
}