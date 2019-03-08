using UnityEngine;
using UnityEngine.UI;

//<alex>
[RequireComponent(typeof(Image))]
public class MinimapVines : MonoBehaviour {

    [SerializeField] private NumberOfFloors numberOfFloors;

    private float highestVinePoint;
    //private float highestMinimapPoint;

    private GameObject vine;
    [SerializeField] private Image minimapBackground;

    private Image vineBar;
	
	void Start ()
    {
        vine = GameObject.Find("botWall");
        vineBar = GetComponent<Image>();

        //highestMinimapPoint = minimapBackground.rectTransform.sizeDelta.y;
        highestVinePoint = 40 * numberOfFloors.NumFloors;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (vine != null)
        {
            //Calculate current vine position as value between 0 and 1
            float currentVinePosition = 1 - ((highestVinePoint - vine.transform.localScale.y) / highestVinePoint);
            
            //Calculate position on minimap bar as value between 0 and 1
            //Vector2 sizeDelta = new Vector2(vineBar.rectTransform.sizeDelta.x,
            //                                              highestMinimapPoint * currentVinePosition);

            vineBar.fillAmount = currentVinePosition;
        }
        else
        {
            Debug.Log("MINIMAP : VINE WALL IS NULL");
        }
	}
}
