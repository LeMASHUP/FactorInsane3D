using UnityEngine;

public class CreateBlocks : MonoBehaviour
{
    private float interval = 3f;
    public GameObject Block;

    private void Start()
    {
        InvokeRepeating("CreateBlock", 3f, interval);
    }

    private void Update()
    {

    }
    private void CreateBlock()
    {
        Vector3 pos = new Vector3(this.transform.position.x+1, this.transform.position.y+1, this.transform.position.z);
        Instantiate(Block, pos, Quaternion.identity);
    }
}
