using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap mainTile;
    [SerializeField] private TileBase whiteTile;
    public GameObject[] machines;
    private PlaceableObject objectToPlace;
    private int index = 0;

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            index = 0;
            InitializedWithObject(machines[index]);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            index = 1;
            InitializedWithObject(machines[index]);
        }

        if (!objectToPlace)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (objectToPlace.Placed == false)
            {
                objectToPlace.Rotate();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (CanBePlaced(objectToPlace))
            {
                objectToPlace.Place();
                Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                TakeArea(start);
            }

            else
            {
                Destroy(objectToPlace.gameObject);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(objectToPlace.gameObject);
        }
    }

    public static Vector3 GetMouseWorldPosition() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach(var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    public void InitializedWithObject(GameObject prefab)
    {
        Vector3 pos = SnapCoordinateToGrid(new Vector3(0, prefab.transform.position.y, 0));
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
    }

    private bool CanBePlaced(PlaceableObject placeableObject)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        area.size = placeableObject.Size;

        TileBase[] baseArray = GetTilesBlock(area, mainTile);

        foreach(var boxCollider in baseArray)
        {
            if (boxCollider == whiteTile && placeableObject.Placed == false)
            {
                return false;
            }
        }

        return true;
    }
    
    public void TakeArea(Vector3Int start)
    {
        mainTile.BoxFill(start, whiteTile, start.x, start.y, start.x, start.y);
    }
}
