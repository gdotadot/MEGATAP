using UnityEngine;

public class DestroyByTime : MonoBehaviour {
    [SerializeField] private int timeToDestroy;

    private void Start()
    {
        Destroy(this.gameObject, timeToDestroy);
    }
}
