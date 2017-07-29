using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nekki.SF2.GUI
{
	public class TableViewScroll : ScrollRect
    {
        #region Property

        public float Size
        {
            get
            {
				if (_orientation == TableViewOrientation.Horizontal)
                {
                    return content.rect.width;
                }
                else
                {
                    return content.rect.height;
                }
            }
        }

        public float SizeDelta
        {
            get
            {
				if (_orientation == TableViewOrientation.Horizontal)
                {
                    return content.sizeDelta.y;
                }
                else
                {
                    return content.sizeDelta.x;
                }
            }
            set
            {
				if (_orientation == TableViewOrientation.Horizontal)
                {
                    content.sizeDelta = new Vector2(value, content.sizeDelta.y);
                }
                else
                {
                    content.sizeDelta = new Vector2(content.sizeDelta.x, value);
                }
            }
        }

		private TableViewOrientation _orientation;

        #endregion

        #region Public

		public void Init()
        {
			CreateContent ();
        }

		public void SetOrientation(TableViewOrientation layoutOrientation)
        {
            _orientation = layoutOrientation;

			if (_orientation == TableViewOrientation.Horizontal) {
				content.anchorMin = new Vector2(0f, 0f);
				content.anchorMax = new Vector2(0f, 1f);
				content.pivot = new Vector2(0.0f, 0.5f);
			}
			else {
				content.anchorMin = new Vector2(0f, 1f);
				content.anchorMax = new Vector2(1f, 1f);
				content.pivot = new Vector2(0.5f, 1.0f);
			}

			horizontal = (_orientation == TableViewOrientation.Horizontal);
			vertical = !horizontal;
        }

        public void SetNormalizedPosition(float normalizedPosition)
        {
			if (_orientation == TableViewOrientation.Horizontal) {
                horizontalNormalizedPosition = normalizedPosition;
            } else {
                verticalNormalizedPosition = normalizedPosition;
            }
        }

        #endregion

		#region Private

		void CreateContent()
		{
			content = new GameObject("Table View Content", typeof(RectTransform)).GetComponent<RectTransform>();
			content.SetParent(gameObject.GetComponent<RectTransform>(), false);
			content.offsetMin = Vector2.zero;
			content.offsetMax = Vector2.zero;
			content.gameObject.AddComponent<NonDrawingGraphic> (); // for touching
		}

		#endregion

		#region ItemsScroll

		[SerializeField]
		public UnityEvent onDragBegin = new UnityEvent();

		[SerializeField]
		public UnityEvent onDragEnd = new UnityEvent();

		public override void OnBeginDrag(PointerEventData eventData)
		{
			base.OnBeginDrag(eventData);
			onDragBegin.Invoke();
		}

		public override void OnEndDrag(PointerEventData eventData)
		{
			base.OnEndDrag(eventData);
			onDragEnd.Invoke();
		}

		#endregion
    }
}