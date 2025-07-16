using UnityEngine;

public class PlatformObstacles : MonoBehaviour
{
    #region Variables

    [Header("Obstacles")]
    public GameObject[] Obstacles;
    private int NumberofActiveObjects;

    #endregion

    #region Unity Default Methods

    // Start is called before the first frame update
    void Start()
    {
        // Set Random Number of Obstacles
        NumberofActiveObjects = Random.Range(0, 4);

        // Setup Obstacles on Start
        SetupObstacles();
    }    

    #endregion

    #region Custom Methods

    public void SetupObstacles()
    {        
        if (Obstacles.Length > NumberofActiveObjects)
        {
            int temp = 0;
            while (temp < NumberofActiveObjects)
            {
                int index = Random.Range(0, Obstacles.Length - 1);
                if (!Obstacles[index].activeInHierarchy)
                {
                    temp++;
                    Obstacles[index].SetActive(true);
                }
            }
        }        
    }

    #endregion
}
