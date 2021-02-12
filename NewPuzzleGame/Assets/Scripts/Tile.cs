using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 TargetPos { get; set; }

    private Vector2 _originalPos;

    private float _lerpSpeed = 5.0f;

    private SpriteRenderer _spriteRenderer;

    private bool IsInRightSpot = false;

    private GamePlay _gamePlay;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        TargetPos = transform.position;
        _originalPos = transform.position;
    }

    private void Start()
    {
        _gamePlay = GamePlay.Instance;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * _lerpSpeed);
        CheckTile();
    }

    private void CheckTile()
    {
        if (_originalPos == TargetPos && !IsInRightSpot)
        {
            _spriteRenderer.color = Color.green;
            IsInRightSpot = true;
            _gamePlay.IncCorrectTile(1);
        }
        else if (_originalPos != TargetPos && IsInRightSpot)
        {
            _spriteRenderer.color = Color.white;
            IsInRightSpot = false;
            _gamePlay.IncCorrectTile(-1);
        }
    }
}