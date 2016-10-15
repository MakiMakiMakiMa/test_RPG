using UnityEngine;
using System.Collections;

public class Controller_Effect : MonoBehaviour {

	// Use this for initialization
    public float showTime = 0.2f;
	void Start () {
     //transform.GetChild(0).GetChild(0).GetComponent<Animation>().Play("Take 001");
     StartCoroutine(Hide(showTime));
	
	}
    IEnumerator Hide(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
