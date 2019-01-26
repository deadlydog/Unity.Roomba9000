using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class world : MonoBehaviour
{
    // FIXME, enum?
    const int NOTHING = 0;
    const int WALL = 1;
    const int CARPET = 2;

    public int MAX_SIZE = 50;
    public int ROOM_SIZE = 10;

    public float TILE_SIZE = 1;

    private int[,] map; 

    // List<MonoBehaviour> map = new List<MonoBehaviour>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("test");
        generateRoomBox(ROOM_SIZE);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    int[,] generateRoomBox(int size) {
        int startingCorner = (MAX_SIZE - size) / 2;
        Random random = new Random();
        map = new int[MAX_SIZE, MAX_SIZE]; 
        for (int x = startingCorner; x < size; x++) {
            int type = CARPET; 
            if (x == startingCorner || x == size - 1) {
                type = WALL;
            }
            for (int y = startingCorner; y < size; y++) {
                if (y == startingCorner || y == size - 1) {
                    type = WALL;
                }
                map[x,y] = type;
            }
        }
        return map;
    }

    void OnDrawGizmos() {
        if (map != null) {
            for (int x = 0; x < MAX_SIZE; x++) {
                for (int y = 0; y < MAX_SIZE; y++) {
                    Vector3 location = new Vector3(x * TILE_SIZE, 0, y * TILE_SIZE);

                    if (map[x,y] == CARPET) {
                        Gizmos.color = Color.white;
                        Gizmos.DrawCube(location, Vector3.one);
                    } else if (map[x,y] == WALL) {
                        Gizmos.color = Color.black;
                        Gizmos.DrawCube(location, Vector3.one);
                    }
                }
            }
        }

    }
}
