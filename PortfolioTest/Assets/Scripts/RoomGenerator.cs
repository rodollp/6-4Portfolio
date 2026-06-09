using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;

    public int width = 3;
    public int height = 3;
    public float tileSize = 1f;

    void Start()
    {
        GenerateRoom();
    }

    void GenerateRoom()
    {
        Vector3 origin = transform.position;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = origin + new Vector3(x * tileSize, 0, z * tileSize);

                // ¹Ù´Ú
                Instantiate(floorPrefab, pos, Quaternion.identity, transform);

                // ¿Ü°û Ã¼Å©
                if (x == 0)
                    SpawnWall(pos, Vector3.up * 0, 270); // West
                else if (x == width - 1)
                    SpawnWall(pos, Vector3.up * 0, 90);   // East
                else if (z == 0)
                    SpawnWall(pos, Vector3.up * 0, 180);  // South
                else if (z == height - 1)
                    SpawnWall(pos, Vector3.up * 0, 0);    // North
            }
        }
    }

    void SpawnWall(Vector3 pos, Vector3 offset, float yRotation)
    {
        Vector3 wallPos = pos + offset + Vector3.up * 0.5f;

        Quaternion rot = Quaternion.Euler(0, yRotation, 0);

        Instantiate(wallPrefab, wallPos, rot, transform);
    }
}