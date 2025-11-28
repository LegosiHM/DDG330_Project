using UnityEngine;
using UnityEngine.InputSystem;

public class FreeLookCamera : MonoBehaviour
{
    [SerializeField] private float sensitivity = 0.1f;
    [SerializeField] private float moveSpeed = 3f;

    private Vector2 _angles;                  
    private Vector3 _defaultLocalPosition;    
    private Quaternion _defaultLocalRotation; 

    private void Awake()
    {
        _defaultLocalPosition = transform.localPosition;
        _defaultLocalRotation = transform.localRotation;
    }

    public void FreeLook()
    {
        if (PauseMenu.IsPaused)
            return;

        if (Mouse.current != null)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();

            _angles.x += delta.x * sensitivity;   
            _angles.y -= delta.y * sensitivity;   
            _angles.y = Mathf.Clamp(_angles.y, -80f, 80f);

            Quaternion pitch = Quaternion.Euler(_angles.y, 0f, 0f);
            Quaternion yaw = Quaternion.Euler(0f, _angles.x, 0f);

            transform.localRotation = yaw * pitch;
        }

        Vector3 move = Vector3.zero;
        var kb = Keyboard.current;
        if (kb != null)
        {
            if (kb.wKey.isPressed) move += Vector3.forward;
            if (kb.sKey.isPressed) move += Vector3.back;
            if (kb.aKey.isPressed) move += Vector3.left;
            if (kb.dKey.isPressed) move += Vector3.right;
            if (kb.eKey.isPressed) move += Vector3.up;
            if (kb.qKey.isPressed) move += Vector3.down;
        }

        transform.Translate(move * moveSpeed * Time.deltaTime, Space.Self);
    }

    public void ResetToDefault()
    {
        _angles = Vector2.zero;
        transform.localPosition = _defaultLocalPosition;
        transform.localRotation = _defaultLocalRotation;
    }
}
