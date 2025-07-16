using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    #region Variables

    [Header("Platform Prefab Ref")]
    public GameObject Platform1;
    public GameObject Platform2;
    public GameObject Platform3;

    [Header("Platfrom Pool List Ref")]
    public List<GameObject> Platform1Pool;
    public List<GameObject> Platform2Pool;
    public List<GameObject> Platform3Pool;

    [Header("Pool Length of Platforms")]
    public float NumberOfPoolPlatform1;
    public float NumberOfPoolPlatform2;
    public float NumberOfPoolPlatform3;

    [Header("Platform Occurance")]
    public float Platform1Occurence;
    public float Platform2Occurence;

    [Header("Platform Distance Vector")]
    public Vector3 DistanceVector;

    [Header("PlayerRef")]
    public GameObject PlayerRef;

    [Header("UI Ref")]
    public Canvas GameplayCanvasRef;

    bool callOnlyonce = false;


    #endregion

    #region Custom Methods

    // Start is called before the first frame update
    public void StartGame()
    {
        if (callOnlyonce == false)
        {
            callOnlyonce = true;
            // Deactivate Canvas
            GameplayCanvasRef.enabled = false;

            // Initialize Objects
            InitializePlatforms(NumberOfPoolPlatform1, ref Platform1Pool, ref Platform1);
            InitializePlatforms(NumberOfPoolPlatform2, ref Platform2Pool, ref Platform2);
            InitializePlatforms(NumberOfPoolPlatform3, ref Platform3Pool, ref Platform3);

            // Setup Platforms Positions
            LevelSetup();

            // Activate Player
            PlayerRef.SetActive(true);
        }        
    }

    private void InitializePlatforms(float NumberofObjects, ref List<GameObject> ObjectListRef, ref GameObject InstatiateObject)
    {
        // Instanciate Platform One Model And Store into List
        for (int i = 0; i < NumberofObjects; i++)
        {
            GameObject IntanciatedObject = Instantiate(InstatiateObject, Vector3.zero, Quaternion.identity);
            ObjectListRef.Add(IntanciatedObject);
            IntanciatedObject.SetActive(false);
        }
    }

    public void LevelSetup()
    {
        int TempValuePlatfom1 = 11;
        int TempValuePlatfom2 = 0;        
        Vector3 PreviousPlatformPosition = Vector3.zero;
        // Setup Platforms
        for (int i = 0; i < Platform3Pool.Count; i++)
        {                        
            if (TempValuePlatfom1 > Platform1Occurence)
            {
                //Debug.Log("In first");
                for (int K = 0; K < Platform1Pool.Count; K++)
                {
                    if (!Platform1Pool[K].activeInHierarchy)
                    {
                        // Activate Object
                        Platform1Pool[K].SetActive(true);

                        // Set Previous Platform Position
                        Platform1Pool[K].transform.position = PreviousPlatformPosition;

                        // Set First Platform Positions From Vector3 zero
                        Platform1Pool[K].transform.position += DistanceVector;

                        // Store Previous Platform Position
                        PreviousPlatformPosition = Platform1Pool[K].transform.position;

                        TempValuePlatfom1 = 0;

                        //// Reduce Value to Manage Position
                        //TempValuePlatfom2--;

                        break;
                    }
                }                                                
            }            
            else if (TempValuePlatfom2 > Platform2Occurence)
            {
                //Debug.Log("In Second");
                for (int j = 0; j < Platform2Pool.Count; j++)
                {
                    if (!Platform2Pool[j].activeInHierarchy)
                    {
                        // Activate Object
                        Platform2Pool[j].SetActive(true);

                        // Set Previous Platform Position
                        Platform2Pool[j].transform.position = PreviousPlatformPosition;

                        // Set First Platform Positions From Vector3 zero
                        Platform2Pool[j].transform.position += DistanceVector;

                        // Store Previous Platform Position
                        PreviousPlatformPosition = Platform2Pool[j].transform.position;

                        TempValuePlatfom2 = 0;

                        break;
                    }
                }
            }
            else
            {
                TempValuePlatfom1++;
                TempValuePlatfom2++;

                //Debug.Log("In Third");
                if (!Platform3Pool[i].activeInHierarchy)
                {
                    // Activate Object
                    Platform3Pool[i].SetActive(true);

                    // Set Previous Platform Position
                    Platform3Pool[i].transform.position = PreviousPlatformPosition;

                    // Set First Platform Positions From Vector3 zero
                    Platform3Pool[i].transform.position += DistanceVector;

                    // Store Previous Platform Position
                    PreviousPlatformPosition = Platform3Pool[i].transform.position;                                        
                }
            }
        }
    }

    #endregion
}
