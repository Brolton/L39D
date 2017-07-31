using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Brolton.GUI;
using LudumDare39;

public class ContactCell : TableViewCell
{
    [SerializeField]
    Image _avatar;
    [SerializeField]
    UnityEngine.UI.Text _name;
    [SerializeField]
    Image _msgIndicator;

    public void Init(ContactData contactData)
    {
        _avatar.sprite = Resources.Load<Sprite>("Textures/Contacts/" + contactData.AvatarImg);
        _name.text = contactData.Name;
        _msgIndicator.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region TableViewCell

    public override void SetHighlighted()
    {
        //          print("CellSetHighlighted : " + RowNumber);
    }

    public override void SetSelected()
    {
        //          print("CellSetSelected : " + RowNumber);
        _msgIndicator.gameObject.SetActive(false);
    }

    public override void Display()
    {
        //          text.text = "Row " + RowNumber;
    }

    #endregion

    public void TurnOnIndicator()
    {
        _msgIndicator.gameObject.SetActive(true);
    }
}
