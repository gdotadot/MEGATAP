using UnityEngine;
using UnityEngine.UI;

//<alex>
[RequireComponent(typeof(Image))]
public class MinimapVines : MonoBehaviour {

    [SerializeField] private NumberOfFloors numberOfFloors;

    [SerializeField] private Vine vine;
    [SerializeField] private Image minimapBackground;

    private float highestVinePoint;
    private Image vineBar;
	
	void Start ()
    {
        vineBar = GetComponent<Image>();
        highestVinePoint = 4 * numberOfFloors.NumFloors;
    }
	
	void Update ()
    {
        if (vine != null)
        {
            float currentVinePoint = vine.TotalFaceNumber;

            vineBar.fillAmount = currentVinePoint / highestVinePoint;
        }
        else
        {
            Debug.Log("MINIMAP : VINE IS NULL");
        }
	}
}
