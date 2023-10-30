using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool Placed { get; private set; }
    public Vector3Int Size { get; private set; }
    private Vector3[] vertices;

    private void GetColliderVertexPositionLocal()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        vertices = new Vector3[4];
        vertices[0] = boxCollider.center + new Vector3(-boxCollider.size.x, -boxCollider.size.y, -boxCollider.size.z) * 0.5f;
        vertices[1] = boxCollider.center + new Vector3(boxCollider.size.x, -boxCollider.size.y, -boxCollider.size.z) * 0.5f;
        vertices[2] = boxCollider.center + new Vector3(boxCollider.size.x, -boxCollider.size.y, boxCollider.size.z) * 0.5f;
        vertices[3] = boxCollider.center + new Vector3(-boxCollider.size.x, -boxCollider.size.y, boxCollider.size.z) * 0.5f;
    }

    private void CalculateSizeInCells()
    {
        Vector3Int[] _vertices = new Vector3Int[vertices.Length];

        for (int i = 0; i < _vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformDirection(vertices[i]);
            _vertices[i] = BuildingSystem.current.gridLayout.WorldToCell(worldPos);
        }

        Size = new Vector3Int(Math.Abs((_vertices[0] - _vertices[1]).x), Math.Abs((_vertices[0] - _vertices[3]).y), 1);
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(vertices[0]);
    }

    private void Start()
    {
        GetColliderVertexPositionLocal();
        CalculateSizeInCells();
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0, 90, 0));
        Vector3[] __vertices = new Vector3[vertices.Length];
        for (int i = 0; i < __vertices.Length; i++)
        {
            __vertices[i] = vertices[(i+ 1) % vertices.Length];
        }

        vertices = __vertices;
    }

    public virtual void Place()
    {
        ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
        Destroy(drag);

        Placed = true;
    }
}
