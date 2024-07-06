using UnityEngine;


public class Node : MonoBehaviour
{
  public LayerMask unwalkableMask;
  public bool walkable;
  public bool occupied;
  public Vector3 gcord;

  public Color originalColor;
  public Vector3 Gcord
  {
    get
    {
      return gcord;
    }
    set
    {
      gcord = value;
    }
  }

  void Start()
  {
    walkable = checkForObstacle();
    if (walkable != true)
    {
      //gameObject.GetComponent<Renderer>().enabled = true;
    }
    else
    {


    }
  }


  void Update()
  {

    if (occupied != true)
    {
      gameObject.GetComponent<Renderer>().material.color = originalColor;

    }
    else
    {
      gameObject.GetComponent<Renderer>().enabled = true;
      gameObject.GetComponent<Renderer>().material.color = Color.yellow;
      //gameObject.GetComponent<Renderer>().material.color = Color.red;

    }

  }
  public bool checkForObstacle()
  {
    return Physics.CheckSphere(transform.position, 0.5f, unwalkableMask);
  }
}
