using UnityEngine.SocialPlatforms;

namespace Brolton.GUI
{
    public class CellSizes
    {
        #region Property

        private float[] rowSize;
        private float[] cumulativeRowSize;
        public int CumulativeIndex = -1;

        public int RowsCount
        {
            get { return rowSize.Length; }
        }

        public int CumulativeRowsCount
        {
            get { return cumulativeRowSize.Length; }
        }

		private float _spacing = 0;
		public float Spacing
		{
			get { return _spacing; }
			set { _spacing = value; }
		}

        #endregion

        #region Public

        public void SetRowsCount(int count)
        {
            CumulativeIndex = -1;
            rowSize = new float[count];
            cumulativeRowSize = new float[count];
        }

        public void SetSizeForRow(float size, int row)
        {
            if (size <= 0) return;
            if (row >= RowsCount) return;

            rowSize[row] = size;
        }

        public float SizeForRow(int row)
        {
			if (row < 0) {
				return 0;
			}
            return rowSize[row];
        }

        public float SumWithRange(Range range)
        {
			if (range.count == 0)
				return 0;
			
			return GetCumulativeRowSize (range.from + range.count - 1) - GetCumulativeRowSize (range.from - 1);
        }

        public float GetCumulativeRowSize(int row)
        {
			if (row < 0) {
				return 0;
			}
			
            while (CumulativeIndex < row)
            {
                CumulativeIndex++;
                cumulativeRowSize[CumulativeIndex] = rowSize[CumulativeIndex];

                if (CumulativeIndex > 0)
                {
					cumulativeRowSize[CumulativeIndex] += _spacing;
                    cumulativeRowSize[CumulativeIndex] += cumulativeRowSize[CumulativeIndex - 1];
                }
            }
            return cumulativeRowSize[row];
        }

        #endregion
    }
}