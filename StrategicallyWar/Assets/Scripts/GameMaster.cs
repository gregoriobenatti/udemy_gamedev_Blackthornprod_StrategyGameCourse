using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Unit selectedUnit;

    public void ResetTiles()
    {
        var tiles = FindObjectsOfType<Tile>();
        foreach (var tile in tiles)
        {
            tile.Reset();
        }
    }
}
