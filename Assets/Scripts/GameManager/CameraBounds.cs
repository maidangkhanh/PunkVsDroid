using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
[RequireComponent(typeof(Camera))]
public class CameraBounds : MonoBehaviour
{

    public float minVisibleX;
    public float maxVisibleX;
    private float minValue;
    private float maxValue;
    public float cameraHalfWidth;

    private Camera activeCamera;

    public Transform cameraRoot;

    public Transform leftBounds;
    public Transform rightBounds;

    void Start()
    {

        activeCamera = Camera.main;

        // calculate the camera half width by  transforming the
        // screen’s left-most and right-most points from screen space to world space
        // equivalents using the camera’s ScreenToWorldPoint method.Then it takes the
        // absolute distance between these points as the camera’s half-view width
        cameraHalfWidth =
        Mathf.Abs(activeCamera.ScreenToWorldPoint(Vector3.zero).x - activeCamera.ScreenToWorldPoint(new Vector3(UnityEngine.Device.Screen.width, 0, 0)).x) * 0.5f;
        minValue = minVisibleX + cameraHalfWidth;
        maxValue = maxVisibleX - cameraHalfWidth;


        // move the wall to the edge of the camera’s view
        Vector3 position;
        position = leftBounds.transform.localPosition;
        position.x = transform.localPosition.x - cameraHalfWidth;
        leftBounds.transform.localPosition = position;
        position = rightBounds.transform.localPosition;
        position.x = transform.localPosition.x + cameraHalfWidth;
        rightBounds.transform.localPosition = position;
    }
    public void SetXPosition(float x)
    {
        Vector3 trans = cameraRoot.position;
        trans.x = Mathf.Clamp(x, minValue, maxValue);
        cameraRoot.position = trans;
    }
}
