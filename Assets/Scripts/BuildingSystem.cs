using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEditor;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap mainTile;
    [SerializeField] private TileBase whiteTile;
    public GameObject[] machines;
    public TMPro.TMP_Text selectionText;
    private PlaceableObject objectToPlace;
    private int index = 0;

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (objectToPlace != null && objectToPlace.Placed == false)
            {
                Destroy(objectToPlace.gameObject);
            }
            index = 0;
            InitializedWithObject(machines[index]);
            selectionText.text = "Blocks Machine";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (objectToPlace != null && objectToPlace.Placed == false)
            {
                Destroy(objectToPlace.gameObject);
            }
            index = 1;
            InitializedWithObject(machines[index]);
            selectionText.text = "Conveyor";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (objectToPlace != null && objectToPlace.Placed == false)
            {
                Destroy(objectToPlace.gameObject);
            }
            index = 2;
            InitializedWithObject(machines[index]);
            selectionText.text = "Intersection";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (objectToPlace != null && objectToPlace.Placed == false)
            {
                Destroy(objectToPlace.gameObject);
            }
            index = 3;
            InitializedWithObject(machines[index]);
            selectionText.text = "Piston";
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
                selectionText.text = null;
                Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                TakeArea(start);
            }

            else
            {
                selectionText.text = null;
                Destroy(objectToPlace.gameObject);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            selectionText.text = null;
            Destroy(objectToPlace.gameObject);
        }

        if (Input.GetMouseButtonDown(1))
        {
            DeleteObjectUnderMouse();
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

    private void DeleteObjectUnderMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            PlaceableObject placeableObject = raycastHit.collider.GetComponent<PlaceableObject>();
            if (placeableObject != null && placeableObject.Placed)
            {
                Destroy(placeableObject.gameObject);
                Vector3Int cellPosition = gridLayout.WorldToCell(placeableObject.transform.position);
                mainTile.SetTile(cellPosition, null);
            }
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
