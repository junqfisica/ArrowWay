using UnityEngine;
using System.Collections;

namespace CameraBounds{

	public static class GetCameraBoundaries  {

		//public static GetCameraBoundaries Boundare;


		public static Vector2 BoundsMin(Camera camera)
		{
			return (Vector2)camera.transform.position - Extents (camera);
		}

		public static Vector2 BoundsMax(Camera camera)
		{
			return (Vector2)camera.transform.position + Extents(camera);
		}

		private static Vector2 Extents(Camera camera){

			if (camera.orthographic)
				return new Vector2(camera.orthographicSize * Screen.width/Screen.height, camera.orthographicSize);
			else
			{
				Debug.LogError("Camera is not orthographic!", camera);
				return new Vector2();
			}
		}

	}
}
