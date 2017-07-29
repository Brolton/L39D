using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine
{
	public static class TransformExtensions
	{
		public static void SetLocalPositionX(this Transform transform, float value)
		{
			transform.localPosition = new Vector3 (
				value,
				transform.localPosition.y,
				transform.localPosition.z);
		}

		public static void SetLocalPositionY(this Transform transform, float value)
		{
			transform.localPosition = new Vector3 (
				transform.localPosition.x,
				value,
				transform.localPosition.z);
		}

		public static void SetLocalPositionZ(this Transform transform, float value)
		{
			transform.localPosition = new Vector3 (
				transform.localPosition.x,
				transform.localPosition.y,
				value);
		}
	}
}

