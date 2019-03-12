﻿using UnityEngine;
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
    private GameObject currentLastSpell;
    private GameObject currentFirstTrap;

    private AudioSource audioSource;
    private Vector3 buttonScale;
    EventSystem es;
    private void Start()
    {
        GameObject player = GameObject.Find("Player 2");
        audioSource = GetComponentInParent<AudioSource>();

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
        if(audioSource != null) audioSource.Play();
        if (isThisTrap)
        {
            GetCurrentFirstTrap();
            if (Input.GetAxis("Horizontal_Menu") > 0 || IsSpellQueueNull())
            {
                if(cs != null) cs.DestroyTarget();
                if (currentFirstTrap != null && currentFirstTrap.gameObject == this.gameObject)
                {
                    currentFirstTrap = null;
                    controllerCursor.transform.localPosition = new Vector3(0, 130);
                    cursorMove.MovingTraps = true;

                }
            }
            if (cursorMove != null) cursorMove.MovingTraps = true;
        }
        else
        {
            GetCurrentLastSpell();
            if (Input.GetAxis("Horizontal_Menu") < 0 || IsTrapQueueNull())
            {
                if(pt != null) pt.DestroyGhost();
                if (currentLastSpell != null && currentLastSpell.gameObject == this.gameObject)
                {
                    controllerCursor.transform.localPosition = new Vector3(0, -100);
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
        buttonScale = this.transform.localScale;
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

    private void GetCurrentFirstTrap()
    {
        if (pt != null)
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
        if (cs != null)
        {
            for (int s = cs.queue.Length - 1; s >= 0; s--)
            {
                if (cs.queue[s] != null && cs.queue[s].activeInHierarchy)
                {
                    currentLastSpell = cs.queue[s];
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
                if (cs.queue[s] != null && cs.queue[s].activeInHierarchy)
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
                if (pt.queue[s] != null && pt.queue[s].activeInHierarchy)
                {
                    return false;
                }
            }
        }

        return true;
    }
}