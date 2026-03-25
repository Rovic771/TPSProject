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

    private Vector2 _lookInput;
    private float _rotationX;
    private float _rotationY;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // on récupère la rotation initiale de la cam
        Vector3 angle = transform.eulerAngles; // euleurAngle = ce qu'on voit dans l'inspecteur
        _rotationX = angle.y;
        _rotationY = angle.x;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        _rotationX += _lookInput.x * sensitivity;
        _rotationY -= _lookInput.y * sensitivity;

        _rotationY = Mathf.Clamp(_rotationY, verticalMin, verticalMax);

        Quaternion rotation = Quaternion.Euler(_rotationY, _rotationX, 0);
        transform.rotation = rotation;

        Vector3 position = target.position - (transform.forward*distance);
        position = position + (transform.right * positionOffset.x) + (transform.up * positionOffset.y);
        transform.position = position;
    }
}
