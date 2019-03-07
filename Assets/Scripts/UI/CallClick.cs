using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

//Alex
//Invoke on click when button is selected
public class CallClick : MonoBehaviour, ISelectHandler// required interface when using the OnSelect method.
{
    [SerializeField] private bool isThisTrap;

    private CastSpell cs;
    private PlaceTrap pt;
    private Image controllerCursor;
    private MoveControllerCursor cursorMove;
    private int currentLastSpell;
    private int currentFirstTrap;

    private void Start()
    {
        GameObject player = GameObject.Find("Player 2");
        cs = player.GetComponent<CastSpell>();
        pt = player.GetComponent<PlaceTrap>();
        controllerCursor = GameObject.Find("ControllerCursor").GetComponent<Image>() ;
        cursorMove = player.GetComponent<MoveControllerCursor>();
    }
    
    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        if(isThisTrap)
        {
            if(Input.GetAxis("Horizontal_Menu") > 0)
            {
                cs.DestroyTarget();
                controllerCursor.transform.localPosition = new Vector3(0, 130);
                cursorMove.MovingTraps = true;
            }
        }
        else
        {
            if (Input.GetAxis("Horizontal_Menu") < 0)
            {
                pt.DestroyGhost();
                controllerCursor.transform.localPosition = new Vector3(0, -100);
                cursorMove.MovingTraps = false;
            }

        }
        GetComponent<Button>().onClick.Invoke();
    }
}