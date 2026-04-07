using Unity.VisualScripting;
using UnityEngine;

public abstract class Detection : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] Color rayColor = Color.green;
    [SerializeField] private Transform rayCastOrigin;
    [SerializeField] private LayerMask whatToHit;
    public Pathfindings _pathfindingsScript;
    public bool canDetect = true;
    private Vector3 direction;
    public Vector3 playerPosition;
    
    private void Start()
    {
        _pathfindingsScript = GetComponentInParent<Pathfindings>();
    }

    public abstract void OnTriggerEnter(Collider other);

    public abstract void OnTriggerStay(Collider other);

    public abstract void DetectedPlayer(bool _isDetect);

    public abstract void OnTriggerExit(Collider other);

    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }
    
    protected bool TestIfWall()
    {
        direction = (player.transform.position - rayCastOrigin.position);
        Vector3 directionFlat = new Vector3(direction.x, direction.y +0.5f, direction.z);
        Ray raycast = new Ray(rayCastOrigin.position, directionFlat);
        RaycastHit hit;
        Debug.DrawRay(rayCastOrigin.position, directionFlat, rayColor, 5 );
        if (Physics.Raycast(raycast, out hit, Mathf.Infinity, whatToHit))
        {
            Debug.Log("Raycast touche " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return false;
            }
        }
        return true;
    }
}
