using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    private SpriteRenderer renderer;
    private GameMaster gm;
    
    public Sprite[] tileGraphics;
    public float hoverAmount;
    public LayerMask obstacleLayer;
    public Color highlightedColor;
    public bool isWalkable;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        var randomTile = Random.Range(0, tileGraphics.Length);
        renderer.sprite = tileGraphics[randomTile];
        gm = FindObjectOfType<GameMaster>();
    }

    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * hoverAmount;
    }
    
    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * hoverAmount;
    }

    public bool isClear()
    {
        var obstacle = Physics2D.OverlapCircle(transform.position, 0.2f, obstacleLayer);
        return obstacle != null;
    }

    public void Highlight()
    {
        renderer.color = highlightedColor;
        isWalkable = true;
    }

    public void Reset()
    {
        renderer.color = Color.white;
        isWalkable = false;
    }

    public void OnMouseDown()
    {
        if (isWalkable && gm.selectedUnit != null)
        {
            gm.selectedUnit.Move(this.transform.position);
        }
    }
}
