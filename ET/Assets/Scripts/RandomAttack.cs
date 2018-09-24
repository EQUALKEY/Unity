using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttack : MonoBehaviour {
	
	// index 0~5까지 각각 sin, sec, tan, cos, cosec, cotan
	// TriFunc는 각 image object,
	// activated는 각 image object들의 활성화 여부 구분
	// isSolving은 활성화된 object가 있을 경우 true,
	// isSolving 상태에서 새로운 함수가 뜨면 GameOver
	public GameObject[] TriFunc = new GameObject[6];
	public GameObject GameOver;
	public GameObject EventController;
	
	private bool[] activated = new bool[6];
	private float f;
	private float faster;
	private bool isSolving;

	public void Awake() {
		isSolving = false;
		foreach (GameObject i in TriFunc) i.SetActive(false);
		for (int i =0; i<6; i++) activated[i] = false;
		f = 0.0f;
		faster = 3.0f;
		StartCoroutine("CountTime");
	}

	IEnumerator CountTime() {
		if(f == 0.0f) {
			if(isSolving) GameOver.SetActive(true);
			System.Random r = new System.Random();
			int rand = r.Next(0, 6);
			TriFunc[rand].SetActive(true);
			activated[rand] = true;
			isSolving = true;
		}
		if(f <= faster) {
			yield return new WaitForSeconds(0.1f);
			f += 0.1f;
		} else {
			f = 0.0f;
			faster -= 0.1f;
		}
		StartCoroutine("CountTime");
	}

	public void Anwser(int index) {
		if (activated[index]) {
			activated[index] = false;
			TriFunc[index].SetActive(false);
			isSolving = false;
			EventController.GetComponent<GamePlay>().Initiate();
		} else GameOver.SetActive(true);
	}

	public void Stop() {
		StopCoroutine("CountTime");
	}
}
