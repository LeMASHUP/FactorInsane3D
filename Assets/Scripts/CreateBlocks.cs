using UnityEngine;

public class CreateBlocks : MonoBehaviour
{
    public float interval = 3f;
    public float maxBlocks = 10;
    [SerializeField] private GameObject Block;

    private void Start()
    {
        InvokeRepeating("CreateBlock", 3f, interval);
    }

    private void CreateBlock()
    {
        if (gameObject.GetComponent<PlaceableObject>().Placed == true)
        {
            if (maxBlocks > 0)
            {
                float rotationY = gameObject.transform.rotation.eulerAngles.y;


                if (Mathf.Approximately(rotationY, 0f))
                {
                    Vector3 pos = new Vector3(this.transform.position.x + 1, this.transform.position.y + 1, this.transform.position.z);
                    Instantiate(Block, pos, Quaternion.identity);
                    maxBlocks -= 1;
                }
                else if (Mathf.Approximately(rotationY, 90f))
                {
                    Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z - 1);
                    Instantiate(Block, pos, Quaternion.identity);
                    maxBlocks -= 1;
                }
                else if (Mathf.Approximately(rotationY, 180f) || Mathf.Approximately(rotationY, -180f))
                {
                    Vector3 pos = new Vector3(this.transform.position.x - 0.85f, this.transform.position.y + 1, this.transform.position.z);
                    Instantiate(Block, pos, Quaternion.identity);
                    maxBlocks -= 1;
                }
                else if (Mathf.Approximately(rotationY, 270f))
                {
                    Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z + 1);
                    Instantiate(Block, pos, Quaternion.identity);
                    maxBlocks -= 1;
                }
            }
        }
    }
}
