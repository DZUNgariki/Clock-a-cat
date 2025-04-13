using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class dieScript : MonoBehaviour
{
    public float max_HP = 3f;
    public float HP = 3f;

    void OnTriggerEnter2D(Collider2D col) {

		if (col.gameObject.CompareTag("Die")) {
            if (HP <= 1) {Application.LoadLevel(Application.loadedLevel);}
            else {
                GetComponent<Rigidbody2D>().MovePosition(col.gameObject.transform.GetChild(0).position);
                HP -= 1f;
                }
            }
	}
}
