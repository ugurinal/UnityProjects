using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 TargetPos { get; set; }

    private Vector2 originalPos;

    private float _lerpSpeed = 5.0f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        TargetPos = transform.position;
        originalPos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * _lerpSpeed);
        CheckTile();
    }

    private void CheckTile()
    {
        if (originalPos == TargetPos)
            spriteRenderer.color = Color.green;
        else
            spriteRenderer.color = Color.white;
    }
}