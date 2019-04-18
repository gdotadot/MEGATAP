using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad instanceRef;

    void Awake()
    {
        //Make this object w/ music audio source continue throughout all scenes.
        //Only if there isn't already one that exists - to prevent stacking
        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
}
