﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class World : MonoBehaviour
{

    // FIXME, enum?
    const int NOTHING = 0;
    const int WALL = 1;
    const int CARPET = 2;

    public int MAX_SIZE = 50;
    public int ROOM_SIZE = 10;

    public float TILE_SIZE = 1;

    // Start is called before the first frame update
    void Start()
    {
        int[,] map = generateRoomBox(ROOM_SIZE);
        createMesh(map);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    int[,] generateRoomBox(int size) {
        int startingCorner = (MAX_SIZE - size) / 2;
        int endCorner = startingCorner + size;
        Random random = new Random();
        int[,] map = new int[MAX_SIZE, MAX_SIZE]; 
        for (int x = startingCorner; x < endCorner +size; x++) {
            int type = CARPET; 
            if (x == startingCorner || x == size - 1) {
                type = WALL;
            }
            for (int y = startingCorner; y < endCorner; y++) {
                if (y == startingCorner || y == size - 1) {
                    type = WALL;
                }
                map[x,y] = type;
            }
        }
        return map;
    }

    void OnDrawGizmos() {
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawSphere(transform.position, 0.5f);
    }

    void createMesh(int[,] map) {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        List<Vector3> vertArray = new List<Vector3>();
        List<int> triangles = new List<int>();
        string output = "";
        if (map != null) {
            for (int x = 0; x < MAX_SIZE; x++) {
                for (int z = 0; z < MAX_SIZE; z++) {
                    if (map[x,z] == CARPET) {
                        generateSquare(x, z, vertArray, triangles);
                    } else if (map[x,z] == WALL) {
                        generateSquare(x, z, vertArray, triangles);
                        // generateWall(x, z, vertArray, triangles);
                    }
                    output += map[x,z];
                }
                output += '\n';
            }

            // Debug.Log(output);
        }

        mesh.vertices = vertArray.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.name = "terrain"; 

        MeshCollider meshCollider = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        mesh.RecalculateBounds();
        meshCollider.sharedMesh = mesh;
    }

    void generateSquare(int x, int z, List<Vector3> vertArray, List<int> triangles) {
        int start = vertArray.Count;

        float offset = (TILE_SIZE * MAX_SIZE /2);

        float xPos = x * TILE_SIZE - offset;
        float zPos = z * TILE_SIZE - offset;

        vertArray.Add(new Vector3(xPos, 0, zPos));
        vertArray.Add(new Vector3(xPos + TILE_SIZE, 0, zPos));
        vertArray.Add(new Vector3(xPos, 0, zPos + TILE_SIZE));
        vertArray.Add(new Vector3(xPos + TILE_SIZE, 0, zPos + TILE_SIZE));

        triangles.Add(start);
        triangles.Add(start + 2);
        triangles.Add(start + 1);

        triangles.Add(start + 2);
        triangles.Add(start + 3);
        triangles.Add(start + 1);
    }
}