using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccumTriggerScript : MonoBehaviour {
	Vector2 velocity;
	VaccumScript vaccumScript;

	public Transform EndTrigger;

	void Start () {
		vaccumScript = transform.parent.parent.GetComponent<VaccumScript> ();
		Vector2 force = new Vector2 (Mathf.Cos (180 * Mathf.Deg2Rad), Mathf.Sin (180 * Mathf.Deg2Rad));
	}

	void OnTriggerEnter2D(Collider2D pCol){
		if (pCol.tag == "Projectile" && !vaccumScript.isBallInside) {
			if (gameObject.tag == "BezierTrigger") {
				velocity = pCol.GetComponent<Rigidbody2D> ().velocity;
				pCol.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				pCol.GetComponent<Rigidbody2D> ().gravityScale = 0;
				SetBezierSiblingState (false);
				StartCoroutine (MoveBall (pCol.gameObject));
				vaccumScript.isBallInside = true;
				vaccumScript.Ball = pCol.gameObject;
			//	print (gameObject);
			} else if (gameObject.tag == "VaccumTrigger") {
				
			}
		}
	}

	void OnTriggerExit2D(Collider2D pCol){
		if (pCol.tag == "Projectile" && gameObject.tag == "VaccumTrigger") {
			vaccumScript.isBallInside = false;
			SetBezierSiblingState (true);
			vaccumScript.Ball = null;
		}
	}

	IEnumerator MoveBall(GameObject pCol){
		float value = 0.0f;
		while (value < 1.0f) {
			yield return new WaitForSeconds (Time.deltaTime);
			pCol.transform.position = gameObject.transform.GetChild (0).GetComponent<BezierCurve> ().GetPointAt (value);
			value += Time.deltaTime;
		}
		pCol.GetComponent<Rigidbody2D> ().gravityScale = 1;
		ThrowBall ();
	}
		
	void SetBezierSiblingState(bool state){
		for (int i = 0; i < vaccumScript.BezierTriggers.transform.childCount; i++) {
			vaccumScript.BezierTriggers.transform.GetChild (i).GetComponent<BoxCollider2D> ().enabled = state;
		}
	}

	void ThrowBall(){
		if (vaccumScript.Ball == null)
			return;

		float angle = EndTrigger.localEulerAngles.z;
		Vector2 force = new Vector2 (Mathf.Cos (angle * Mathf.Deg2Rad), Mathf.Sin (angle * Mathf.Deg2Rad));
		force *= vaccumScript.ballForce;
		vaccumScript.Ball.GetComponent<Rigidbody2D> ().AddForce (force, ForceMode2D.Impulse);
	}
}
