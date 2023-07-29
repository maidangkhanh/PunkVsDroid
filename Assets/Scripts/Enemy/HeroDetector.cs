using UnityEngine;
//1
[RequireComponent(typeof(Collider))]
public class HeroDetector : MonoBehaviour
{
    //2
    public bool heroIsNearby;
    //3
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Hero")
        {
            heroIsNearby = true;
        }
    }
    public void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Hero")
        {
            heroIsNearby = false;
        }
    }
}