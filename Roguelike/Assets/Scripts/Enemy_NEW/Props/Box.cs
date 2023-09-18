using UnityEngine;

public class Box : Destructible
{
    private void OnDestroy()
    {
        // spawn particles 
        Debug.Log("DESTROYED: " + gameObject.name);
    }
}
