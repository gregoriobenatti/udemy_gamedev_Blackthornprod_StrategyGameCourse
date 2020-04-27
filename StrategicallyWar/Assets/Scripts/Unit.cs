using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool selected;
    public int tileSpeed;
    public bool hasMoved;
    public float moveSpeed;

    GameMaster gm;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    private void OnMouseDown()
    {
        if (selected)
        {
            selected = false;
            gm.selectedUnit = null;
            gm.ResetTiles();
        }
        else
        {
            if (gm.selectedUnit != null)
            {
                gm.selectedUnit.selected = false;    
            }

            selected = true;
            gm.selectedUnit = this;
            
            gm.ResetTiles();
            GetWalkableTiles();
        }
    }

    void GetWalkableTiles()
    {
        if (hasMoved) 
        {
             return;
        }
        
        var tiles = FindObjectsOfType<Tile>();
        foreach (var tile in tiles) 
        { 
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileSpeed)
            {
               if (!tile.isClear())
                {
                   tile.Highlight();
                }
            }          
        }
    }

    public void Move(Vector2 tilePosition)
    {
        gm.ResetTiles();
        StartCoroutine(StartMovement(tilePosition));
    }

    IEnumerator StartMovement(Vector2 tilePosition)
    {
        while (transform.position.x != tilePosition.x)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(tilePosition.x, transform.position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }
        
        while (transform.position.y != tilePosition.y)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(transform.position.x, tilePosition.y), moveSpeed * Time.deltaTime);
            yield return null;
        }

        hasMoved = true;
    }
}
