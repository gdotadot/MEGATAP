using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using TMPro;

//Alex
//Invoke on click when button is selected
public class ButtonSelect : MonoBehaviour, ISelectHandler, IDeselectHandler// required interface when using the OnSelect & OnDeselect methods.
{
    [SerializeField] private bool isThisTrap;
    
    private CastSpell cs;
    private PlaceTrap pt;
    private Image controllerCursor;
    private TextMeshProUGUI tooltip;
    private MoveControllerCursor cursorMove;
    private GameObject currentFirstSpell;
    private GameObject currentLastTrap;

    private AudioSource audioSource;
    private Vector3 buttonScale;
    EventSystem es;

    private InputManager inputManager;


    private void Awake()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    private void Start()
    {
        GameObject player = GameObject.Find("Player 2");
        audioSource = GetComponentInParent<AudioSource>();
        buttonScale = new Vector3(0.75f, 0.75f, 0.75f);
        TextMeshProUGUI[] tooltips = transform.parent.parent.GetComponentsInChildren<TextMeshProUGUI>();
        foreach(TextMeshProUGUI t in tooltips)
        {
            if(t.name == "Tooltip")
            {
                tooltip = t;
                tooltip.transform.SetAsLastSibling();
            }
        }
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        if(es.currentSelectedGameObject != null) ChangeTooltip(es.currentSelectedGameObject.name);

        cs = player.GetComponent<CastSpell>();
        pt = player.GetComponent<PlaceTrap>();
        controllerCursor = GameObject.Find("ControllerCursor").GetComponent<Image>();
        cursorMove = player.GetComponent<MoveControllerCursor>();
    }

    public void Update()
    {
        //if (es.currentSelectedGameObject == null) tooltip.text = "";
    }

    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        if (audioSource != null) audioSource.Play();
        if (isThisTrap)
        {
            GetCurrentLastTrap();
            if (inputManager.GetAxis(InputCommand.TopPlayerMenu) < 0 || IsSpellQueueNull())
            {
                if(cs != null) cs.DestroyTarget();
                if (currentLastTrap != null && currentLastTrap.gameObject == this.gameObject && currentLastTrap.gameObject.GetComponent<Button>().interactable)
                {
                    currentLastTrap = null;
                    controllerCursor.transform.localPosition = new Vector3(0, 130);
                    cursorMove.MovingTraps = true;

                }
            }
            if (cursorMove != null) cursorMove.MovingTraps = true;
        }
        else
        {
            GetCurrentFirstSpell();
            if (inputManager.GetAxis(InputCommand.TopPlayerMenu) > 0 || IsTrapQueueNull())
            {
                if (pt != null) pt.DestroyGhost();
                if (currentFirstSpell != null && currentFirstSpell.gameObject == this.gameObject)
                {
                    //Debug.Log("First");
                    controllerCursor.transform.localPosition = new Vector3(0, -130);
                    cursorMove.MovingTraps = false;
                }
            }
            if (cursorMove != null) cursorMove.MovingTraps = false;
        }

        //Button Scaling
        ScaleUp();

        GetComponent<Button>().onClick.Invoke();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        this.transform.localScale = buttonScale;
        tooltip.text = "";
    }

    public void ScaleUp()
    {
        if(this.GetComponent<Button>().interactable) this.transform.localScale *= 1.35f;
        ChangeTooltip(this.name);
    }


    private void ChangeTooltip(string toCheck)
    {
        if(tooltip != null)
        {
            switch(toCheck)
            {
                //Spells
                case "Blur Spell Button(Clone)":
                    tooltip.text = "Blur";
                    break;
                case "Gust Spell Button(Clone)":
                    tooltip.text = "Wind";
                    break;
                case "Lightning Spell Button(Clone)":
                    tooltip.text = "Lightning";
                    break;
                case "NarrowPOV Spell Button(Clone)":
                    tooltip.text = "Narrow Vision";
                    break;
                case "Slow Spell Button(Clone)":
                    tooltip.text = "Slow";
                    break;
                case "Stun Spell Button(Clone)":
                    tooltip.text = "Petrify";
                    break;
                //Traps
                case "ArrowButton(Clone)":
                    tooltip.text = "Arrow Shooter";
                    break;
                case "BananaButton(Clone)":
                    tooltip.text = "Banana";
                    break;
                case "SapButton(Clone)":
                    tooltip.text = "Sap";
                    break;
                case "SpikeButton(Clone)":
                    tooltip.text = "Spike";
                    break;

            }
        }
    }

    private void GetCurrentLastTrap()
    {
        if (pt != null)
        {
            //foreach (GameObject t in pt.queue)
            //{
            //    if (t != null && t.activeInHierarchy && t.GetComponent<Button>().interactable)
            //    {
            //        currentFirstTrap = t;
            //        return;
            //    }
            //}

            for(int t = pt.queue.Count - 1; t >= 0; t--)
            {
                if (pt.queue[t] != null && pt.queue[t].activeInHierarchy && pt.queue[t].GetComponent<Button>().interactable)
                {
                    currentLastTrap = pt.queue[t];
                    return;
                }
            }
        }
    }

    private void GetCurrentFirstSpell()
    {
        if (cs != null)
        {
            //for (int s = cs.queue.Length - 1; s >= 0; s--)
            //{
            //    if (cs.queue[s] != null && cs.queue[s].activeInHierarchy && cs.queue[s].GetComponent<Button>().interactable)
            //    {
            //        currentLastSpell = cs.queue[s];
            //        return;
            //    }
            //}

            for(int s = 0; s < cs.queue.Length; s++)
            {
                if (cs.queue[s] != null && cs.queue[s].activeInHierarchy && cs.queue[s].GetComponent<Button>().interactable)
                {
                    currentFirstSpell = cs.queue[s];
                    return;
                }
            }
        }
    }

    private bool IsTrapQueueNull()
    {
        if(pt!= null)
        {
            for (int s = cs.queue.Length - 1; s >= 0; s--)
            {
                if (cs.queue[s] != null && cs.queue[s].activeInHierarchy && cs.queue[s].GetComponent<Button>().interactable) 
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool IsSpellQueueNull()
    {
        if (cs != null)
        {
            for (int s = pt.queue.Count - 1; s >= 0; s--)
            {
                if (pt.queue[s] != null && pt.queue[s].activeInHierarchy && pt.queue[s].GetComponent<Button>().interactable)
                {
                    return false;
                }
            }
        }

        return true;
    }
}