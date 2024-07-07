using System.Collections.Generic;
using UnityEngine;

public class ArcRenderer : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject dotPrefab;
	private GameObject arrowReference;

	public int dotPoolSize = 50;
    private List<GameObject> dotPool = new List<GameObject> ();
    public float dotSpacing = 50;
	public int dotsToSkip = 1;

	public float arrowAngleCorrection = 0;
    private Vector3 arrowDirection;

	public float baseScreenWidth = 1920f;
	[SerializeField] private float spacingScale;

    void Start()
    {
		//create arrow and set position to 0
        arrowReference = Instantiate(arrowPrefab, transform);
        arrowReference.transform.localPosition = Vector3.zero;
        InitializeDotPool(dotPoolSize);

		spacingScale = Screen.width / baseScreenWidth; //Scales dot spacing based on current screen width
    }

	void OnEnable()
	{
		spacingScale = Screen.width / baseScreenWidth;
	}

	void Update()
	{
		//store position of mouse
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 0;

		Vector3 startPos = transform.position;
		Vector3 midPoint = CalculateMidPoint(startPos, mousePos);

		UpdateArc(startPos, midPoint, mousePos);
		PositionAndRotateArrow(mousePos);
	}

	void UpdateArc(Vector3 start, Vector3 mid, Vector3 end)
	{
		//Distance / spacing rounded up to int
		int numDots = Mathf.CeilToInt(Vector3.Distance(start, end) / (dotSpacing * spacingScale));

		//checks for exceeding the number of dots or the total pool of dots
		for (int i = 0; i < numDots && i < dotPool.Count; i++)
		{
			float t = i / (float)numDots;
			t = Mathf.Clamp(t, 0f, 1f); //ensure t stays between [0, 1]

			Vector3 position = QuadraticBezierPoint(start, mid, end, t);

			//skips the ending dots to leave space for the arrowhead
			if (i != numDots - dotsToSkip)
			{
				dotPool[i].transform.position = position;
				dotPool[i].SetActive(true);
			}
			if(i == numDots - (dotsToSkip + 1) && i - dotsToSkip + 1 >= 0)
			{
				arrowDirection = dotPool[i].transform.position;
			}
		}

		//Deactivate unused dots
		for (int i = numDots - dotsToSkip; i < dotPool.Count; i++)
		{
			if(i > 0)
			{
				dotPool[i].SetActive(false);
			}
		}
	}

	void PositionAndRotateArrow(Vector3 position)
	{
		arrowReference.transform.position = position;
		Vector3 direction = arrowDirection - position;

		//returns x and y in radians and converts to angle
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		//correcting arrow angle, and setting arrow rotation on z axis
		angle += arrowAngleCorrection;
		arrowReference.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //same as (0,0,1)
	}

	Vector3 CalculateMidPoint(Vector3 start, Vector3 end)
	{
		Vector3 midpoint = (start + end) / 2;

		//takes a third of the distance to add to the y creating a curve
		float arcHeight = Vector3.Distance(start, end) / 3f;
		midpoint.y += arcHeight;
		return midpoint;
	}

	Vector3 QuadraticBezierPoint(Vector3 start, Vector3 control, Vector3 end, float t)
	{
		float u = 1 - t;
		float tt = t * t;
		float uu = u * u;

		Vector3 point = uu * start;
		point += 2 * u * t * control;
		point += tt * end;
		return point;
	}

	void InitializeDotPool(int count)
	{
		for (int i = 0; i < count; i++) 
		{
			//create dots to use and deactivate them adding them to list
			GameObject dot = Instantiate(dotPrefab, Vector3.zero, Quaternion.identity, transform);
			dot.SetActive(false);
			dotPool.Add(dot);
		}
	}
}
