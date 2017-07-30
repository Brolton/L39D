using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nekki.SF2.GUI;

public class ContactCell : TableViewCell
{
    [SerializeField]
    Image _avatar;
    [SerializeField]
    UnityEngine.UI.Text _name;

    public void Init(string name)
    {
        _name.text = name;
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
    }

    public override void Display()
    {
        //          text.text = "Row " + RowNumber;
    }

    #endregion
}
