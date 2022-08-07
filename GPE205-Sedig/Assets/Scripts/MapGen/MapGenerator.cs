using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    
    public GameObject[] gridPrefabs;
    public GameObject RandomRoomPrefab() 
    {
     return gridPrefabs[Random.Range(0, gridPrefabs.Length)];
    }
    
    public int rows;
    public int cols;
    public float roomWidth  = 195.0f;
    public float roomHeight = 195.0f;
    private Room[,] grid;

    void Start()
    {
        GenerateMap();
    }

   
    void Update()
    {
        
    }
    void GenerateMap()
    {
        if(GameManager.instance!=null)
        {
            if(GameManager.instance.isUseSeed)
            {
             GameManager.instance.SetStringSeed();
             //set use date seed false if already useing a seed, to avoid conflicts.
             GameManager.instance.isUseDateSeed = false;
            }
            if(GameManager.instance.isUseDateSeed)
            {
             GameManager.instance.SetMapOfTheDaySeed();
            }
        }
        grid = new Room[cols,rows];
            //for each Row in the array,
        for(int currentRow = 0; currentRow < rows; currentRow ++)
        {

            //go through each Collumn of that row
            for(int currentCol = 0; currentCol < cols; currentCol ++)
            {
                //Spawn a new room using the width and height set for the room multiplied by the row and collumn
                // we are currently looking at to set its position.
                //Now each time this loop runs, it will instantiate a room right next to the last room spawned.
               float xPosition = roomWidth * currentCol;
               float zPosition = roomHeight * currentRow;
               Vector3 newPosition = new Vector3 (xPosition, 0.0f, zPosition);

               GameObject TempRoomObj = Instantiate (RandomRoomPrefab(), newPosition,Quaternion.identity) as GameObject;

               TempRoomObj.transform.parent = this.transform;
               TempRoomObj.name = "Zone_" + currentCol + "," + currentRow;
               Room TempRoom = TempRoomObj.GetComponent<Room>();
               
               //set the room at the coordinates of the array we are looking at to this room.
               grid[currentCol,currentRow] = TempRoom; 
               
               //check which doors should be open.
               CheckRowOpenings(currentRow, TempRoom);
               CheckCollumnOpenings(currentCol,TempRoom);
               GameManager.instance.LevelZones.Add(TempRoomObj);

            }
        }
         //Must reset the seed to random after generation completes to avoid
         //all random numbers used for gameplay following the same seed.
         GameManager.instance.RerandomizeSeed();

    }

    public void CheckRowOpenings(int row, Room room)
    {
        
        if(row == 0) //the bottom (soulth row).
        {
            room.doorNorth.SetActive(false);
        }
        else if(row == rows - 1) //the top (north row) 
        {
            Destroy(room.doorSouth);
        } 
        else // in an interior room
        {
            Destroy(room.doorSouth);
            Destroy(room.doorNorth);
        }
    }
    public void CheckCollumnOpenings(int collumn, Room room)
    {
        if(collumn == 0) //the right (east collumn).
        {
            room.doorEast.SetActive(false);
        }
        else if(collumn == cols - 1) //the left (west collumn) 
        {
            Destroy(room.doorWest);
        } 
        else // in an interior room
        {
            Destroy(room.doorWest);
            Destroy(room.doorEast);
        }
    }
    
}
