using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

//Alex
//Invoke on click when button is selected
public class CallClick : MonoBehaviour, ISelectHandler// required interface when using the OnSelect method.
{
    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        GetComponent<Button>().onClick.Invoke();
    }
}