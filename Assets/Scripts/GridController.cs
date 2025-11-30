using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController instance;

    public Transform minPoint, maxPoint;
    public GrowBlock baseGridBlock;

    private Vector2Int gridSize;

    public List<BlockRow> blockRows = new List<BlockRow>();

    public LayerMask gridBlockers;

    public void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateGrid()
    {
        minPoint.position = new Vector3(Mathf.Round(minPoint.position.x), Mathf.Round(minPoint.position.y), 0f);
        maxPoint.position = new Vector3(Mathf.Round(maxPoint.position.x), Mathf.Round(maxPoint.position.y), 0f);

        Vector3 startpoint = minPoint.position + new Vector3(.5f, .5f, 0f);

        //Instantiate(baseGridBlock, startpoint, Quaternion.identity);

        gridSize = new Vector2Int(Mathf.RoundToInt(maxPoint.position.x - minPoint.position.x), 
            Mathf.RoundToInt(maxPoint.position.y - minPoint.position.y));

        for (int y = 0; y < gridSize.y; y++)
        {
            blockRows.Add(new BlockRow());

            for (int x = 0; x < gridSize.x; x++)
            {
                GrowBlock newblock = Instantiate(baseGridBlock, startpoint + new Vector3(x, y, 0f), Quaternion.identity);

                newblock.transform.SetParent(transform);
                newblock.sr.sprite = null; //nulls the test image block

                blockRows[y].blocks.Add(newblock);

                if (Physics2D.OverlapBox(newblock.transform.position, new Vector2(.9f, .9f), 0, gridBlockers))
                {
                    newblock.sr.sprite = null;
                    newblock.preventUse = true;
                }

            }
           
        }

        baseGridBlock.gameObject.SetActive(false);

    }

    public GrowBlock GetBlock(float x, float y)
    {
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        x -= minPoint.position.x;
        y -= minPoint.position.y;

        int IntX = Mathf.RoundToInt(x);
        int IntY = Mathf.RoundToInt(y);

        if(IntX < gridSize.x && IntY < gridSize.y)
        {
            return blockRows[IntY].blocks[IntX];
        }

        return null;
    }


}

[System.Serializable]
public class BlockRow
{
    public List<GrowBlock> blocks = new List<GrowBlock>();
}
