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
    private GameObject currentLastSpell;
    private GameObject currentFirstTrap;

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
            GetCurrentFirstTrap();
            if(Input.GetAxis("Horizontal_Menu") > 0)
            {
                cs.DestroyTarget();
                if (currentFirstTrap.gameObject == this.gameObject)
                {
                    currentFirstTrap = null;
                    controllerCursor.transform.localPosition = new Vector3(0, 130);
                    cursorMove.MovingTraps = true;

                }
            }
            cursorMove.MovingTraps = true;
        }
        else
        {
            GetCurrentLastSpell();
            if (Input.GetAxis("Horizontal_Menu") < 0)
            {
                pt.DestroyGhost();
                if(currentLastSpell.gameObject == this.gameObject)
                {
                    controllerCursor.transform.localPosition = new Vector3(0, -100);
                    cursorMove.MovingTraps = false;
                }
            }
            cursorMove.MovingTraps = false;
        }
        
        GetComponent<Button>().onClick.Invoke();
    }

    private void GetCurrentFirstTrap()
    {
        if(pt != null)
        {
            foreach (GameObject t in pt.queue)
            {
                if (t != null && t.activeInHierarchy)
                {
                    currentFirstTrap = t;
                    return;
                }
            }
        }
    }

    private void GetCurrentLastSpell()
    {
        if(cs != null)
        {
            for(int s = cs.queue.Length - 1; s >= 0; s--)
            {
                if(cs.queue[s] != null && cs.queue[s].activeInHierarchy)
                {
                    currentLastSpell = cs.queue[s];
                    return;
                }
            }
        }
    }
}