using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity;

    private float rotY;

    private void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 mouseLookInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime;
        mouseLookInput *= mouseSensitivity;

        rotY -= mouseLookInput.y;
        rotY = Mathf.Clamp(rotY, -90, 90);

        transform.localRotation = Quaternion.Euler(rotY, 0, 0);
        transform.root.Rotate(mouseLookInput.x * Vector3.up);
    }
}
