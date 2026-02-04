using UnityEngine;
using UnityEngine.InputSystem;

public class TpsCameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 5.0f;
    [SerializeField] private float sensitivity = 0.2f;

    [SerializeField] private float verticalMin = -10f;
    [SerializeField] private float verticalMax = 70f;
    [SerializeField] private Vector3 positionOffset;

    private Vector2 lookInput;
    private float rotationX;
    private float rotationY;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // on récupère la rotation initiale de la cam
        Vector3 angle = transform.eulerAngles; // euleurAngle = ce qu'on voit dans l'inspecteur
        rotationX = angle.y;
        rotationY = angle.x;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    
    // Update is called once per frame
    void Update()
    {
        rotationX += lookInput.x * sensitivity;
        rotationY -= lookInput.y * sensitivity;

        rotationY = Mathf.Clamp(rotationY, verticalMin, verticalMax);

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        transform.rotation = rotation;

        Vector3 position = target.position - (transform.forward*distance) + positionOffset;
        transform.position = position;
    }
}
