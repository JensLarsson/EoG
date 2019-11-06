using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableObjects
{
    public Rigidbody2D currentRigidBody;
    public GameObject currentGameObject;
    public List<Vector3> positions = new List<Vector3>();
    public List<Quaternion> rotations = new List<Quaternion>();
    public List<Vector3> velocities = new List<Vector3>();
}
public class RewindManager : MonoBehaviour
{
    [SerializeField] int amountOfSecondsMaxRewind = 20;
    #region singelton stuff

    private static RewindManager _instance;

    public static RewindManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError("more than one rewindManager!");
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    
    List<RewindableObjects> rewindableObjects = new List<RewindableObjects>();
    bool isRewinding = false;
    

    public int avgFrameRate;

    //function called on the rewind : ability script
    public void rewind()
    {
        //tell manager to rewind time
        isRewinding = true;
    }
    public void addObject(GameObject gameObject, Rigidbody2D rigidbody)
    {
        RewindableObjects newRO = new RewindableObjects();
        newRO.currentGameObject = gameObject;
        newRO.currentRigidBody = rigidbody;
        rewindableObjects.Add(newRO);
    }
    public void Update()
    {
        if (isRewinding)
        {
            //when the power is no longer activated stop rewinding
            if (Input.GetButtonUp("Fire1"))
            {
                isRewinding = false;
                return;
            }
            for (int i = 0; i < rewindableObjects.Count; i++)
            {
                if (rewindableObjects[i].positions.Count > 0)
                {
                    //set object to have the same position, rotation and velocity as it had one frame before the current one
                    rewindableObjects[i].currentGameObject.transform.position = rewindableObjects[i].positions[rewindableObjects[i].positions.Count - 1];
                    rewindableObjects[i].currentGameObject.transform.rotation = rewindableObjects[i].rotations[rewindableObjects[i].positions.Count - 1];
                    rewindableObjects[i].currentRigidBody.velocity = rewindableObjects[i].velocities[rewindableObjects[i].positions.Count - 1];

                    //remove the frame so that the object wont get stuck on current frame - 1
                    rewindableObjects[i].positions.RemoveAt(rewindableObjects[i].positions.Count - 1);
                    rewindableObjects[i].rotations.RemoveAt(rewindableObjects[i].rotations.Count - 1);
                    rewindableObjects[i].velocities.RemoveAt(rewindableObjects[i].velocities.Count - 1);
                }
                else
                {
                    isRewinding = false;

                }
            }
        }
        else
        {
            if (rewindableObjects.Count > 0)
            {
                //get fps
                avgFrameRate = (int)(Time.frameCount / Time.time);
                
                //seconds * frams/seconds = frames, what this essantially give us is if we have enough data saved
                //if we do we go the else statenebt where we have to remove some data
                if (amountOfSecondsMaxRewind * avgFrameRate > rewindableObjects[0].positions.Count)
                {
                    for (int i = 0; i < rewindableObjects.Count; i++)
                    {
                        //fill up data on current frame
                        rewindableObjects[i].positions.Add(rewindableObjects[i].currentGameObject.transform.position);
                        rewindableObjects[i].rotations.Add(rewindableObjects[i].currentGameObject.transform.rotation);
                        rewindableObjects[i].velocities.Add(rewindableObjects[i].currentRigidBody.velocity);
                    }
                }
                else
                {
                    //for every object that can be rewinded
                    for (int i = 0; i < rewindableObjects.Count; i++)
                    {
                        //for every index in that object
                        for (int j = 0; j < rewindableObjects[i].positions.Count; j++)
                        {
                            //remove those index which are to old
                            if (j == rewindableObjects[i].positions.Count - 1)
                            {
                                rewindableObjects[i].positions.RemoveAt(j);
                                rewindableObjects[i].rotations.RemoveAt(j);
                                rewindableObjects[i].velocities.RemoveAt(j);
                            }
                            else
                            { 
                                //push up the index one step, to make room for new
                                rewindableObjects[i].positions[j] = rewindableObjects[i].positions[j + 1];
                                rewindableObjects[i].rotations[j] = rewindableObjects[i].rotations[j + 1];
                                rewindableObjects[i].velocities[j] = rewindableObjects[i].velocities[j + 1];
                            }
                        }
                        //fill up with new data on current frame
                        rewindableObjects[i].positions.Add(rewindableObjects[i].currentGameObject.transform.position);
                        rewindableObjects[i].rotations.Add(rewindableObjects[i].currentGameObject.transform.rotation);
                        rewindableObjects[i].velocities.Add(rewindableObjects[i].currentRigidBody.velocity);
                    }
                }
            }
        }
    }
}
