using UnityEngine;

public class CubeCollisionDebug : MonoBehaviour
{
    public Vector3 size = new Vector3(1, 1, 1); 

    private void OnDrawGizmos()
    {
       
        Gizmos.color = Color.red;


        Gizmos.DrawWireCube(transform.position, size);
    }

  
    public bool CheckCubeCollision(Vector3 posA, Vector3 sizeA, Vector3 posB, Vector3 sizeB)
    {
        Vector3 minA = posA - sizeA / 2f;
        Vector3 maxA = posA + sizeA / 2f;

        Vector3 minB = posB - sizeB / 2f;
        Vector3 maxB = posB + sizeB / 2f;

        bool overlapX = maxA.x >= minB.x && minA.x <= maxB.x;
        bool overlapY = maxA.y >= minB.y && minA.y <= maxB.y;
        bool overlapZ = maxA.z >= minB.z && minA.z <= maxB.z;

        return overlapX && overlapY && overlapZ;
    }
}
