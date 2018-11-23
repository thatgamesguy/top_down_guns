using UnityEngine;
using System.Collections;

namespace TDGP.Demo
{
/// <summary>
/// Generates environment for demo scene.
/// </summary>
	public class Environment : MonoBehaviour
	{

		public GameObject Floor;
		public GameObject Wall_N;
		public GameObject Wall_NE;
		public GameObject Wall_E;
		public GameObject Wall_SE;
		public GameObject Wall_S;
		public GameObject Wall_SW;
		public GameObject Wall_W;
		public GameObject Wall_NW;
	
		public Vector2 RoomSize = new Vector2 (15, 15);

		private GameObject container;


		void Awake ()
		{
			container = new GameObject ("Tiles");

			float floorWidth = GetTileWidth (Floor);
			float floorHeight = GetTileHeight (Floor);
		
			for (int i = 0; i < RoomSize.x; i++) {
				var x = i * floorWidth;
				for (int j = 0; j < RoomSize.y; j++) {
					var y = j * floorHeight;
				
					var position = new Vector2 (x, y);
					var tile = GetTile (new Vector2 (i, j));
					
					var tileClone = (GameObject)Instantiate (tile, position, Quaternion.identity);
					tileClone.transform.SetParent (container.transform);
				}
			}
		}
	
		private GameObject GetTile (Vector2 pos)
		{
			if (pos.x == 0 && pos.y == 0) {
				return Wall_SW;
			} else if (pos.x == RoomSize.x - 1 && pos.y == RoomSize.y - 1) {
				return Wall_NE;
			} else if (pos.x == 0 && pos.y == RoomSize.y - 1) {
				return Wall_NW;
			} else if (pos.x == RoomSize.x - 1 && pos.y == 0) {
				return Wall_SE;
			} else if (pos.x == RoomSize.x - 1) {
				return Wall_E;
			} else if (pos.y == RoomSize.y - 1) {
				return Wall_N;
			} else if (pos.x == 0) {
				return Wall_W;
			} else if (pos.y == 0) {
				return Wall_S;
			}
		
			return Floor;
		}
	
		private float GetTileWidth (GameObject tile)
		{
			return tile.GetComponent<Renderer> ().bounds.size.x;
		}
	
		private float GetTileHeight (GameObject tile)
		{
			return tile.GetComponent<Renderer> ().bounds.size.y;
		}
	}
}
