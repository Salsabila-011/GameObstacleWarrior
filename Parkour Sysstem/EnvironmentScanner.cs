using UnityEngine;

public class EnvironmentScanner : MonoBehaviour
{
    [SerializeField] Vector3 forwardRayOffset = new Vector3(0, 2.5f, 0);
    [SerializeField] float forwardRayLenght = 0.8f;
    [SerializeField] float heightRayLenght = 5;
    [SerializeField] LayerMask obstacleLayer;
    public ObstacleHitData ObstacleCheck()
    {
        var hitData = new ObstacleHitData();
        var forwardOrigin = transform.position + forwardRayOffset;

        
        hitData.forwardHitFound = Physics.Raycast(transform.position + forwardRayOffset, transform.forward,
             out hitData.forwardHit, forwardRayLenght, obstacleLayer);

        Debug.DrawRay(forwardOrigin, transform.forward * forwardRayLenght, (hitData.forwardHitFound) ? Color.red : Color.white);

        if (hitData.forwardHitFound)
        {
            var hightOrigin = hitData.forwardHit.point + Vector3.up * heightRayLenght;
            hitData.heightHitFound = Physics.Raycast(hightOrigin, Vector3.down, out hitData.heightHit,
                heightRayLenght, obstacleLayer);

            Debug.DrawRay(hightOrigin, Vector3.down* heightRayLenght, (hitData.heightHitFound) ? Color.red : Color.white);
        }

        return hitData;
    }
}
public struct ObstacleHitData
{
    public bool forwardHitFound;
    public bool heightHitFound;
    public RaycastHit forwardHit;
    public RaycastHit heightHit;
}