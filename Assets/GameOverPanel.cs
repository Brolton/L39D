using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour 
{
	[SerializeField]
	UnityEngine.UI.Text _contact1Result;
	[SerializeField]
	UnityEngine.UI.Text _contact2Result;
	[SerializeField]
	UnityEngine.UI.Text _contact3Result;
	[SerializeField]
	UnityEngine.UI.Text _totalResult;

	[SerializeField]
	Button _btnTryAgain;

	// Use this for initialization
	void Start () {
		_btnTryAgain.onClick.AddListener (OnBtnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateCounters()
	{
		_contact1Result.text = AppController.Instance.MessagesController.GetTotalPointsOfSendedQuestions (0).ToString();
		_contact2Result.text = AppController.Instance.MessagesController.GetTotalPointsOfSendedQuestions (1).ToString();
		_contact3Result.text = AppController.Instance.MessagesController.GetTotalPointsOfSendedQuestions (2).ToString();
		_totalResult.text = AppController.Instance.MessagesController.GetTotalPoints ().ToString();
	}

	void OnBtnClick()
	{
		//Reload
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
