using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Brolton.GUI
{
    [Serializable]
    public class TableViewCellDidSelectEvent : UnityEvent<int>
    {

    }

    [Serializable]
    public class TableViewCellDidHighlightEvent : UnityEvent<int>
    {

    }

    public abstract class TableViewCell : MonoBehaviour, ISelectHandler, ISubmitHandler, IPointerClickHandler
    {
        #region Property

        public TableViewCellDidSelectEvent DidSelectEvent;
        public TableViewCellDidHighlightEvent DidHighlightEvent;

        private int rowNumber;
        public int RowNumber
        {
            get { return rowNumber; }
            set {
                rowNumber = value;
                Display();
            }
        }

        #endregion

        #region Lifecycle

        void Awake()
        {
            this.gameObject.AddComponent<Selectable>();
        }

        #endregion

        #region Public

        public abstract void SetHighlighted();
        public abstract void SetSelected();
        public abstract void Display();

        public void OnSelect(BaseEventData eventData)
        {
            SetHighlighted();

            if (DidHighlightEvent == null) return;

            DidHighlightEvent.Invoke(rowNumber);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            SetSelected();

            if (DidSelectEvent == null) return;

            DidSelectEvent.Invoke(rowNumber);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SetSelected();

            if (DidSelectEvent == null) return;

            DidSelectEvent.Invoke(rowNumber);
        }

        private void OnDestroy()
        {
            DidSelectEvent.RemoveAllListeners();
            DidHighlightEvent.RemoveAllListeners();
        }

        #endregion
    }
}