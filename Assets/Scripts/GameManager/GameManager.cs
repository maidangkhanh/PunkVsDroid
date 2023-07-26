using UnityEngine;
public class GameManager : MonoBehaviour
{
    public Hero actor;
    public bool cameraFollows = true;
    public CameraBounds cameraBounds;

    void Start()
    {
        cameraBounds.SetXPosition(cameraBounds.minVisibleX);
    }

    void Update()
    {
        if (cameraFollows)
        {
            cameraBounds.SetXPosition(actor.transform.position.x);
        }
    }
}