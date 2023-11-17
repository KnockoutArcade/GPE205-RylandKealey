using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    // 2D array of the map
    private Room[,] grid;
    // Map of the day
    public int mapSeed;
    public bool isMapOfTheDay;
    
    // Start is called before the first frame update
    void Start()
    {
        //GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Returns a random Room
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    // Generate the map
    public void GenerateMap()
    {
        // Reset the map
        DestroyExistingMap();
        
        // Set the random seed for the map
        if (isMapOfTheDay)
        {
            UnityEngine.Random.InitState(DateToInt(DateTime.Now.Date));
        }
        else
        {
            UnityEngine.Random.InitState(DateToInt(DateTime.Now));
        }
        
        // clear out the grid - columns are the X, rows are the Y
        grid = new Room[cols, rows];

        // For each grid row...
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            //For each column in that row...
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {
                // figure out the location
                float xPosition = roomWidth * currentCol;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                // Create a new grid at the appropriate location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                // Set its parent
                tempRoomObj.transform.parent = this.transform;

                // Give it a meaningful name
                tempRoomObj.name = "Room_" + currentCol + "currentRow";

                // Get the room component (stores door data)
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                #region Open the doors

                // If we are on the bottom row, open the north door
                if (currentRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (currentRow == rows - 1)
                {
                    // Otherwise, if we are on the top row, open the south door
                    Destroy(tempRoom.doorSouth);
                }
                else
                {
                    // Otherwise, we are in the middle, so open both doors
                    Destroy(tempRoom.doorNorth);
                    Destroy(tempRoom.doorSouth);
                }

                // If we are on the west side, open the east door
                if (currentCol == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (currentCol == cols -1)
                {
                    // Otherwise, we are on the east side, so open the west door
                    Destroy(tempRoom.doorWest);
                }
                else
                {
                    // Otherwise, we are somewhere in the middle, so open both doors
                    Destroy(tempRoom.doorEast);
                    Destroy(tempRoom.doorWest);
                }
                #endregion

                // Save it to the grid array
                grid[currentCol, currentRow] = tempRoom;
            }
        }
    }

    public int DateToInt(DateTime dateToUse)
    {
        // Add up our date and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    // Destroy the existing map
    public void DestroyExistingMap()
    {
        // Since all of the map geometry is a child of the map generator, simply destroying all of the children will wipe the map clean
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
