using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviourSingleton<Map>
{
    [SerializeField] private Tilemap _tilemap;

    public Vector3 GetCellCenterTile(Vector3 pos)
    {
        Vector3Int cell = _tilemap.WorldToCell(pos);
        Vector3 center = _tilemap.GetCellCenterWorld(cell);
        return center;
    }
}
