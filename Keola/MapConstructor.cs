using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConstructor : MonoBehaviour
{
    [SerializeField] private GameObject startChunk;
    [SerializeField] private GameObject goalChunk;

    [SerializeField] private int chunksToGoal;
    [SerializeField] private int bonusChunksToGenerate;

    [SerializeField] private GameObject[] chunks;

    //Todo: Private/Serialize these

    public GameObject start;
    public GameObject previousChunk;
    public GameObject previousChunkConnectors;
    public List<GameObject> previousChunkUnmarkedConnectorsList;
    public GameObject randPreviousChunkConnector;
    public int previousChunkConnectorCount;

    public List<GameObject> allConnectors;
    public List<GameObject> allChunks;
    
    public FixedStack<GameObject> lastFourChunks;

    void Start()
    {   
        previousChunkUnmarkedConnectorsList = new List<GameObject>();
        allConnectors = new List<GameObject>();
        lastFourChunks = new FixedStack<GameObject>(10);

        Initialize();
        ConstructCorePath();
        AddBonusChunks();
        WrapUp();
    }

    private void Initialize() 
    {
        //spawn start chunk at 0,0,0
        start = Instantiate(startChunk, Vector3.zero, Quaternion.identity);
        previousChunk = start;
    }

    private void ConstructCorePath() 
    {
        for(int i = 0; i < chunksToGoal; i++) 
        {
            //generate the new chunk, selecting one at random for our list of possible chunk types
            int rand = Random.Range(0, chunks.Length);
            GameObject thisChunk = Instantiate(chunks[rand], Vector3.zero, Quaternion.identity);

            //keep track of this new chunk
            allChunks.Add(thisChunk);


            ////////////////////////////////////////////////////////////
            //* GENERATE/RECEIVE INFORMATION ABOUT THE PREVIOUS CHUNK */
            ////////////////////////////////////////////////////////////

            //keep information on previous chunk's connectors
            previousChunkConnectors = previousChunk.transform.Find("connectors").gameObject;
            previousChunkConnectorCount = previousChunkConnectors.transform.childCount;

            //keep track of the unmarked connectors. This is so we can find a clean point to attach to.
            //this is especially useful when we have to backtrack a few chunks after a collision.
            foreach (Transform connector in previousChunkConnectors.transform)
            {
                if (connector.gameObject.tag != "markfordestroy")
                {
                    previousChunkUnmarkedConnectorsList.Add(connector.gameObject);
                }
            }

            //randomize the list of unmarked connectors. Then, we just pick one off the top to become our destination connector and mark it for use.
            previousChunkUnmarkedConnectorsList = Shuffle (previousChunkUnmarkedConnectorsList);
            randPreviousChunkConnector = previousChunkUnmarkedConnectorsList[0];
            randPreviousChunkConnector.tag = "markforuse";


            ///////////////////////////////////////////////////////////////
            //* GENERATE/RECEIVE INFORMATION ABOUT THE NEW/CURRENT CHUNK */
            ///////////////////////////////////////////////////////////////

            //keep information on this chunk's connectors, and pick one at random
            GameObject thisChunkConnectors = thisChunk.transform.Find("connectors").gameObject;
            int thisChunkConnectorCount = thisChunkConnectors.transform.childCount;
            GameObject randThisChunkConnector = thisChunkConnectors.transform.GetChild(Random.Range(0, thisChunkConnectorCount)).gameObject;

            //we mark it for destruction later, so that when we add bonus chunks, this one can't be selected.
            randThisChunkConnector.tag = "markfordestroy";

            //store this new chunk's connectors in a list for bonus chunks later
            foreach (Transform connector in thisChunkConnectors.transform) 
            {
                allConnectors.Add(connector.gameObject);
            }


            ///////////////////////////////
            //* PUT THE CHUNK INTO PLACE */
            ////////////////////////////////

            //create dummy gameobject to contain everything at the point of the new chunk connector by parenting it
            GameObject connectorDragger = new GameObject();
            connectorDragger.transform.position = randThisChunkConnector.transform.position;
            connectorDragger.transform.rotation = randThisChunkConnector.transform.rotation;
            thisChunk.transform.parent = connectorDragger.transform;

            Debug.Log("Connecting to the connector at " + randPreviousChunkConnector.transform.position);

            //translate step
            connectorDragger.transform.position = randPreviousChunkConnector.transform.position;

            //rotation step
            connectorDragger.transform.localRotation = Quaternion.Euler(0, randPreviousChunkConnector.transform.eulerAngles.y, 0);
            connectorDragger.transform.Rotate(0f, 180f, 0f);


            ///////////////////
            //* HOUSEKEEPING */
            ///////////////////

            //clean up, remove parent, marked used connector to prevent reuse
            thisChunk.transform.parent = null;
            DestroyImmediate(connectorDragger.gameObject);
                //allConnectors.Remove(randThisChunkConnector.gameObject);
                //allConnectors.Remove(randPreviousChunkConnector.gameObject);
                //DestroyImmediate(randThisChunkConnector.gameObject);
                //DestroyImmediate(randPreviousChunkConnector.gameObject);

            //push this onto our stack
            lastFourChunks.Push(thisChunk);

            //Manually sync transforms and their gameobjects. Necessary since everything is happening on one frame
            Physics.SyncTransforms();


            ///////////////////////////
            //* COLLISION CORRECTION */
            ///////////////////////////

            //if we've run ourselves into another chunk, we need to backtrack and try again by getting rid of the last two chunks.
            if (thisChunk.GetComponent<CollisionCheck>().IsInappropriateTouchingHappening(previousChunk)) 
            {

                //Pop and deconstruct every chunk in the stack, except for the last one.
                for (int j = 0; j < lastFourChunks.Count() - 1; j++)
                {
                    GameObject topChunk = lastFourChunks.Pop();
                    Debug.Log("Last four chunks (after the pop) count " + lastFourChunks.Count() + ". Popped/Destroyed the chunk: " + topChunk.name + " at iteration " + j + ". It existed at " + topChunk.transform.position);
                    foreach (Transform connector in topChunk.transform.Find("connectors"))
                    {
                        allConnectors.Remove(connector.gameObject);
                    }
                    DestroyImmediate(topChunk);
                    chunksToGoal += 1;
                }

                //The last one stays intact and, and becomes our previousChunk for future iterations
                previousChunk = lastFourChunks.Peek();
                Debug.Log("Previouschunk Test (After Backtrack Loop): " + previousChunk.gameObject.name + " at " + previousChunk.transform.position);
                
            } 
            else 
            {
                //finish, reference the new chunk as the previous in future iterations
                previousChunk = thisChunk;
            }

            previousChunkUnmarkedConnectorsList.Clear();
        }


        //////////////////////////////////
        //* END CORE PATH / CREATE GOAL */
        //////////////////////////////////

        //previous chunk's info
        previousChunkConnectors = previousChunk.transform.Find("connectors").gameObject;
        previousChunkConnectorCount = previousChunkConnectors.transform.childCount;
        
        foreach (Transform connector in previousChunkConnectors.transform)
        {
            if (isUnmarkedConnector(connector.gameObject.tag))
            {
                previousChunkUnmarkedConnectorsList.Add(connector.gameObject);
            }
        }

        //randomize the list of unmarked connectors. Then, we just pick one off the top to become our destination connector and mark it for use.
        previousChunkUnmarkedConnectorsList = Shuffle (previousChunkUnmarkedConnectorsList);
        randPreviousChunkConnector = previousChunkUnmarkedConnectorsList[0];
        randPreviousChunkConnector.tag = "markforuse";

        //add the end chunk the same way we've added each chunk thus far
        GameObject endChunk = Instantiate(startChunk, Vector3.zero, Quaternion.identity);
        GameObject endChunkConnector = endChunk.transform.Find("connectors").GetChild(0).gameObject;
        endChunkConnector.tag = "markfordestroy";
        GameObject endDragger = new GameObject();
        endDragger.transform.position = endChunkConnector.transform.position;
        endDragger.transform.rotation = endChunkConnector.transform.rotation;
        endChunk.transform.parent = endDragger.transform;

        //translate step
        endDragger.transform.position = randPreviousChunkConnector.transform.position;

        //rotation step (must check orientation of connectors)
        endDragger.transform.localRotation = Quaternion.Euler(0, randPreviousChunkConnector.transform.eulerAngles.y, 0);
        endDragger.transform.Rotate(0f, 180f, 0f);

        //clean up, remove parent, remove/destroy used connector to prevent reuse
        endChunk.transform.parent = null;
        DestroyImmediate(endDragger.gameObject);
        
        //get rid of all marked (use or destroy connectors)
        purgeAllMarkedConnectors();

        //activate the wall colliders at all connectors
        closeAllEdgeConnectors();
    }

    /////////////////////////////
    //* BONUS CHUNK GENERATION */
    /////////////////////////////

    private void AddBonusChunks()
    {
        for (int i = 0; i < bonusChunksToGenerate; i++) 
        {
            //select and create a new chunk, keeping connector info on it
            int rand = Random.Range(0, chunks.Length);
            GameObject thisChunk = Instantiate(chunks[rand], Vector3.zero, Quaternion.identity);
            GameObject thisChunkConnectors = thisChunk.transform.Find("connectors").gameObject;
            int thisChunkConnectorCount = thisChunkConnectors.transform.childCount;

            //try to place this chunk five times, before giving up. If it can't be placed, the map loses a bonus chunk.
            for (int j = 0; j < 5; j++)
            {
                Debug.Log("Chunk number: " + i + ". Place Attempt: " + j + ". This chunk connector count: " + thisChunkConnectorCount );

                //select a random connector on the new chunk
                GameObject randThisChunkConnector = thisChunkConnectors.transform.GetChild(Random.Range(0, thisChunkConnectorCount)).gameObject;

                //package chunk to manipulate it like we have been
                GameObject connectorDragger = new GameObject();
                connectorDragger.transform.position = randThisChunkConnector.transform.position;
                connectorDragger.transform.rotation = randThisChunkConnector.transform.rotation;
                thisChunk.transform.parent = connectorDragger.transform;

                //pick a random connector in the world to test the selected chunk with
                GameObject foundConnector = allConnectors[Random.Range(0, allConnectors.Count)];

                //find the chunk that the connector belongs to
                GameObject foundChunk = foundConnector.transform.parent.parent.gameObject;

                //translate step (test)
                connectorDragger.transform.position = foundConnector.transform.position;

                //rotation step (test) (checks orientation of connectors)
                connectorDragger.transform.localRotation = Quaternion.Euler(0, foundConnector.transform.eulerAngles.y, 0);
                connectorDragger.transform.Rotate(0f, 180f, 0f);

                //unparent it
                thisChunk.transform.parent = null;
                DestroyImmediate(connectorDragger.gameObject);

                //update the physics engine's model of where everything is
                Physics.SyncTransforms();

                //did this work? Is this chunk touching something?
                //If the chunk is touching something other than itself or the parent of the connector, it failed.
                if (!thisChunk.GetComponent<CollisionCheck>().IsInappropriateTouchingHappening(foundChunk))
                {
                    //We're free to move on to the next chunk, clean up, remove parent, remove/destroy used connector to prevent reuse
                    allConnectors.Remove(foundConnector.gameObject);
                    allConnectors.Remove(randThisChunkConnector.gameObject);
                    DestroyImmediate(foundConnector.gameObject);
                    DestroyImmediate(randThisChunkConnector.gameObject);

                    //Debug.Log("We placed the chunk successfully! Onto the next chunk.");

                    //store this new chunk's connectors in the list
                    foreach (Transform connector in thisChunkConnectors.transform) 
                    {
                        allConnectors.Add(connector.gameObject);
                    }

                    break;
                } 
                else 
                {
                    //if it's the last iteration, we need to give up and destroy the chunk before we continue.
                    if (j == 4) 
                    {
                        DestroyImmediate(thisChunk);
                        //Debug.Log("Chunk Destroyed, we give up.");
                    }

                    continue;
                }
            }
        }
        closeAllEdgeConnectors();
    }

    private void WrapUp() 
    {
        
        ///////////////////////////
        //* PURGE CONNECTOR LIST */
        ///////////////////////////

        foreach (GameObject connector in allConnectors)
        {
            if (isUnmarkedConnector(connector.gameObject.tag))
            {
                connector.GetComponent<Gatekeeper>().Close();
            }
            else 
            {
                DestroyImmediate(connector.gameObject);
            }
        }
        allConnectors.Clear();
    }

    private void ClearMapAndRestart() 
    {
        //eh probably won't implement this unless we need to use it as a failsafe
    }

    private bool isUnmarkedConnector(string tag)
    {
        return tag != "markforuse" && tag != "markfordestroy";
    }

    private void purgeAllMarkedConnectors()
    {
        foreach (GameObject connector in allConnectors)
        {
            if (!isUnmarkedConnector(connector.gameObject.tag))
            {
                connector.gameObject.tag = "markfordestroy";
            }
        }

        foreach (GameObject connector in GameObject.FindGameObjectsWithTag("markfordestroy"))
        {
            allConnectors.Remove(connector);
            DestroyImmediate(connector);
        }
    }

    private void closeAllEdgeConnectors() 
    {
        foreach(GameObject connector in allConnectors)
        {
            connector.GetComponent<Gatekeeper>().Close();
        }
    }

    private List<GameObject> Shuffle(List<GameObject> list)
    {
        List<GameObject> result = new List<GameObject>();
        while (list.Count > 0)
        {
            int rand = Random.Range(0, list.Count);
            result.Add(list[rand]);
            list.RemoveAt(rand);
        }
        return result;
    }
}

///<summary>
/// Custom stack implementation. Pushing onto a full stack deletes (not pops) the bottom element automatically
///</summary>
public class FixedStack<GameObject>
{
    private int fixedSize;
    private LinkedList<GameObject> stack;

    public FixedStack(int maxSize)
    {
        fixedSize = maxSize;
        stack = new LinkedList<GameObject>();
    }

    public void Push(GameObject value)
    {
        if (stack.Count == fixedSize)
        {
            stack.RemoveLast();
        }
        stack.AddFirst(value);
    }

    public GameObject Pop()
    {
        if (stack.Count > 0)
        {
            GameObject value = stack.First.Value;
            stack.RemoveFirst();
            return value;
        }
        Debug.Log("I'm having to return a default gameobject for Pop()");
        return default(GameObject);
    }

    public GameObject Peek()
    {
        if (stack.Count > 0)
        {
            GameObject value = stack.First.Value;
            return value;
        }
        return default(GameObject);
    }

    public void Clear()
    {
        stack.Clear();     
    }

    public int Count()
    {
        return stack.Count;
    }

    public bool IsFull() 
    {
        if (stack.Count == fixedSize)
        {
            return true;
        }
        return false;
    }

    public string ToString()
    {
        string value = "";
        foreach (GameObject go in stack)
        {
            value += go.ToString() + " ";
        }
        return value;
    }
}