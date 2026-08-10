using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [System.Serializable]
    public class Room
    {
        public GameObject roomBlocker; //collider for this room
        public GameObject enemy; //enemy for this room
    }

    public Room[] rooms; //room array

    public float startingTime = 30f; //first rooms starts at 30 secs 
    public float timeDecrease = 5f; //each room 5 secs less

    public Timer timer;

    private int currentRoom = 0; //track which room player in

    public bool enemyStarted; // Sienna - tracking enemy actions

    void Start() //turns off all collider + enemy to start clean
    {
        for (int i = 0; i < rooms.Length; i++) 
        {
            rooms[i].enemy.SetActive(false);

            Collider2D blocker = rooms[i].roomBlocker.GetComponentInChildren<Collider2D>();

            if (blocker != null) 
            {
                blocker.enabled = false; 
            }
        }

        StartRoom(0); //start Room 1
    }

    public void StartRoom(int roomIndex)
    {
        currentRoom = roomIndex; //which room is active

        
        float roomTime = startingTime - (roomIndex * timeDecrease); //calculate room timer for current room

        timer.SetTimer(roomTime); //assigns timer new amount of time

        Collider2D blocker = rooms[roomIndex].roomBlocker.GetComponentInChildren<Collider2D>(); //find room collider (inspector)

        if (blocker != null)
        {
            blocker.enabled = true; //turn room collider ON (cant leave room until after enemy scan)
        }

        rooms[roomIndex].enemy.SetActive(true); //activate rooms enemy

        EnemyMovement enemyMovement = rooms[roomIndex].enemy.GetComponent<EnemyMovement>();

        if (enemyMovement != null)
        {
            enemyMovement.ResetRoom();
        }

        Debug.Log("Starting Room " + (roomIndex + 1));
        Debug.Log("Timer: " + roomTime);
    }



    public void TimerFinished()
    {
        Collider2D blocker = rooms[currentRoom].roomBlocker.GetComponentInChildren<Collider2D>();

        if (blocker != null)
        {
            blocker.enabled = false; //turn off collider for player to leave (happens after timer end but before enemy scan)
        }

        Debug.Log("Timer finished for Room " + (currentRoom + 1));

        enemyStarted = true;
    }

    public void EnemyFinished()
    {

        Debug.Log("Enemy finished Room " + (currentRoom + 1));

        rooms[currentRoom].enemy.SetActive(false); //enemy despawn

        int nextRoom = currentRoom + 1; //next room no.

        if (nextRoom < rooms.Length) 
        {
            StartRoom(nextRoom); 
        }
        else
        {
            Debug.Log("ALL ROOMS COMPLETED!");
        }

        enemyStarted = false;
    }


}