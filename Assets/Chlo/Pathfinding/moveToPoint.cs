using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class moveToPoint : MonoBehaviour
{


    public List<GameObject> path;
    // Start is called before the first frame update
    public GameObject pathOutput;
	public List<Vector2> pubDirs;

    private Coroutine mvCoroutine;


    // Update is called once per frame
    void Update()
    {
        path = pathOutput.GetComponent<AreaSelect>().path;
        if (Input.GetKeyDown(KeyCode.W)){
            mvCoroutine = StartCoroutine(sMoveObject(path, gameObject, 1.0f));
          

        }
    }

    
public string deconS(Vector2 vector)
	{
		string conv(float a)
		{
			return Convert.ToString(a);
		}
		return conv(vector.x) + conv(vector.y);
	}
	public IEnumerator sMoveObject(List<GameObject> targets, GameObject item, float speed)
	{
		for (var i = 0; i < targets.Count; i++)
		{
			float elapsedTime = 0;
			float timeToMove = speed;
			var origPos = item.transform.position;
			//string dir = deconS(pubDirs[i]);
			GameObject Gntarget = targets[i];
			if (Gntarget == null)
			{
				i++;
			}
			Transform target = targets[i].transform;
			while (elapsedTime < timeToMove)
			{
				float t = elapsedTime / timeToMove;
				item.transform.position = new Vector3(Mathf.Lerp(origPos.x, target.position.x, t), Mathf.Lerp(origPos.y, target.position.y, t), 0);
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForSeconds(1.0f);
			item.transform.position = target.position;
		}
	}
}
