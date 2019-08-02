using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class OWP_Controller : MonoBehaviour
{
    //depth based generation, then fill holes
    public int radius = 8;
    public float spacing = 200;
    public int initialIterations;
    public static Dictionary<Vector2, GameObject> blockmap = new Dictionary<Vector2, GameObject>();
    public GameObject[] streetBlocks;
    public GameObject[] otherBlocks;
    private Transform playerPos;
    public int playerPosPrevX;
    public int playerPosPrevY;
    public bool generating = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //couple of vars being dudes, dudes being vars
        playerPos = GameObject.Find("Player_Car").GetComponent<Transform>();
        playerPosPrevX = (int)(playerPos.position.x / spacing);
        playerPosPrevY = (int)(playerPos.position.z / spacing);
        if(blockmap.Count > 0)
        {
            blockmap.Clear();
        }
        //add a street block at 0,0 to the blockmap to start the generation
        GameObject starter = Instantiate(streetBlocks[0], new Vector3(0, 2, 0), Quaternion.identity);
        starter.GetComponent<CityBlock>().isStreet = true;
        blockmap.Add(new Vector2(0, 0), starter);
        //loop through blockmap and extend off roads
        #region
        
        for (int r = 0; r < initialIterations; r++)
        {
            List<Vector2> keys = new List<Vector2>(blockmap.Keys);
            foreach (var i in keys)
            {
                Debug.Log(keys.ToArray().Length);
                CityBlock block = blockmap[i].GetComponent<CityBlock>();
                bool nSpawn = block.ONorth.Length > 0 && !blockmap.ContainsKey(new Vector2(i.x, i.y + 1));
                bool sSpawn = block.OEast.Length > 0 && !blockmap.ContainsKey(new Vector2(i.x, i.y - 1));
                bool eSpawn = block.OSouth.Length > 0 && !blockmap.ContainsKey(new Vector2(i.x + 1, i.y));
                bool wSpawn = block.OWest.Length > 0 && !blockmap.ContainsKey(new Vector2(i.x - 1, i.y));
                if (nSpawn || sSpawn || eSpawn || wSpawn)
                {
                    if (nSpawn)
                    {
                        GameObject nStreet = Instantiate(block.ONorth[Random.Range(0, block.ONorth.Length)],
                            new Vector3(i.x * spacing, 2, (i.y + 1) * spacing), Quaternion.identity);
                        blockmap.Add(new Vector2(i.x, i.y + 1), nStreet);
                        Debug.Log("n");
                    }
                    if (sSpawn)
                    {
                        GameObject sStreet = Instantiate(block.OEast[Random.Range(0, block.OEast.Length)],
                            new Vector3(i.x * spacing, 2, (i.y - 1) * spacing), Quaternion.identity);
                        blockmap.Add(new Vector2(i.x, i.y - 1), sStreet);
                        Debug.Log("s");
                    }
                    if (eSpawn)
                    {
                        GameObject eStreet = Instantiate(block.OSouth[Random.Range(0, block.OSouth.Length)],
                            new Vector3((i.x + 1) * spacing, 2, i.y * spacing), Quaternion.identity);
                        blockmap.Add(new Vector2(i.x + 1, i.y), eStreet);
                        Debug.Log("e");
                    }
                    if (wSpawn)
                    {
                        GameObject wStreet = Instantiate(block.OWest[Random.Range(0, block.OWest.Length)],
                            new Vector3((i.x - 1) * spacing, 2, i.y * spacing), Quaternion.identity);
                        blockmap.Add(new Vector2(i.x - 1, i.y), wStreet);
                        Debug.Log("w");
                    }
                }
            }
        }
        
        #endregion

        //UpdateMap();
    }

    // Update is called once per frame
    void Update()
    {
        int playerXGrid = (int)(playerPos.position.x / spacing);
        int playerYGrid = (int)(playerPos.position.z / spacing);



        if (playerXGrid != playerPosPrevX || playerYGrid != playerPosPrevY)
        {
            UpdateMap();
            GenerateBlocks();
            playerPosPrevX = playerXGrid;
            playerPosPrevY = playerYGrid;
        }
    }

    void GenerateBlocks()
    {
        int playerXGrid = (int)(playerPos.position.x / spacing);
        int playerYGrid = (int)(playerPos.position.z / spacing);
        print("START OF GENERATION LOOP");
        for(int i = -radius; i < radius; i++)
        {
            for(int j = -radius; j < radius; j++)
            {
                if (blockmap.ContainsKey(new Vector2(i + playerXGrid, j + playerYGrid)))
                {
                    CityBlock block = blockmap[new Vector2(i + playerXGrid, j + playerYGrid)].GetComponent<CityBlock>();
                    print("generating " + block.name + "at " + new Vector2(i + playerXGrid, j + playerYGrid));
                    bool nSpawn = block.ONorth.Length > 0 && !blockmap.ContainsKey(new Vector2(i, j + 1));
                    bool sSpawn = block.OEast.Length > 0 && !blockmap.ContainsKey(new Vector2(i, j - 1));
                    bool eSpawn = block.OSouth.Length > 0 && !blockmap.ContainsKey(new Vector2(i + 1, j));
                    bool wSpawn = block.OWest.Length > 0 && !blockmap.ContainsKey(new Vector2(i - 1, j));
                    if (block.isStreet)
                    {
                        //Debug.Log("is street");
                        //Debug.Log("is in radius");
                        if (block.ONorth.Length > 0 && !blockmap.ContainsKey(new Vector2(i + playerXGrid, j + playerYGrid + 1)))
                        {
                            int xPos = i + playerXGrid;
                            int yPos = j + playerYGrid + 1;
                            GameObject nStreet = Instantiate(block.ONorth[Random.Range(0, block.ONorth.Length)],
                                new Vector3((xPos) * spacing, 2, (yPos) * spacing), Quaternion.identity);
                            blockmap.Add(new Vector2(xPos, yPos), nStreet);
                            Debug.Log("n created " + nStreet.name);
                        }
                        if (block.OSouth.Length > 0 && !blockmap.ContainsKey(new Vector2(i + playerXGrid, j + playerYGrid - 1)))
                        {
                            int xPos = i + playerXGrid;
                            int yPos = j + playerYGrid - 1;
                            GameObject sStreet = Instantiate(block.OSouth[Random.Range(0, block.OSouth.Length)],
                                new Vector3((xPos) * spacing, 2, (yPos) * spacing), Quaternion.identity);
                            blockmap.Add(new Vector2(xPos, yPos), sStreet);
                            Debug.Log("s created " + sStreet.name);
                        }
                        if (block.OEast.Length > 0 && !blockmap.ContainsKey(new Vector2(i + playerXGrid + 1, j + playerYGrid)))
                        {
                            int xPos = i + playerXGrid + 1;
                            int yPos = j + playerYGrid;
                            GameObject eStreet = Instantiate(block.OEast[Random.Range(0, block.OEast.Length)],
                                new Vector3((xPos) * spacing, 2, (yPos) * spacing), Quaternion.identity);
                            blockmap.Add(new Vector2(xPos, yPos), eStreet);
                            Debug.Log("e created " + eStreet.name);
                        }
                        if (block.OWest.Length > 0 && !blockmap.ContainsKey(new Vector2(i + playerXGrid - 1, j + playerYGrid)))
                        {
                            int xPos = i + playerXGrid - 1;
                            int yPos = j + playerYGrid;
                            GameObject wStreet = Instantiate(block.OWest[Random.Range(0, block.OWest.Length)],
                                new Vector3((xPos) * spacing, 2, (yPos) * spacing), Quaternion.identity);
                            blockmap.Add(new Vector2(xPos, yPos), wStreet);
                            Debug.Log("w created " + wStreet.name);
                        }
                    }
                }
            }
        }
    }

    void UpdateMap()
    {
        
            List<Vector2> keys = new List<Vector2>(blockmap.Keys);
            foreach (var i in keys)
            {
                Transform playerPos = GameObject.Find("Player_Car").GetComponent<Transform>();
                //check if outside range, and if so, remove it from the blockmap and world.
                if (Mathf.Abs(i.x - (playerPos.position.x / spacing)) > initialIterations || Mathf.Abs(i.y - (playerPos.position.z / spacing)) > initialIterations)
                {
                    Destroy(blockmap[i]);
                    Debug.Log(blockmap[i].name + " Destroyed");
                    blockmap.Remove(i);
                    //yield return null;
                }
            }
    }
}
