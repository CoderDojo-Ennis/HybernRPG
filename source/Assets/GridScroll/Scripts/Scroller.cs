using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {

	//Properties of tiles
	public float width;
	public int cellsX;
	//Propertie of scrolling structure overall
	public Vector2 position;
	//variables controlling scrolling
	public int index;
	//Value of offset will be clamped between 0 and 1, representing the percentage of a tile width
	//the first tile is offset from a position of 1 tile width to the left of the screen edge.
	//			|
	//	   [....][....][....][....][....] ...tile list continues
	//	   0 - 1|
	public float offset;
	
	private GameObject prefab;
	private GameObject[] tiles;
	void Start ()
	{
		//Searches through resources folder to find Tile prefab.
		prefab = (GameObject)Resources.Load("Prefabs/Tile", typeof(GameObject));
		//Creates an array to hold the tiles in.
		tiles = new GameObject[cellsX];
		
		//Loops through tiles and fills them with Tile gameobjects.
		for(int counter = 0; counter < tiles.Length; counter++)
		{
			//Create clone.
			Vector3 positionTemp = new Vector3(width * counter, 0, 0);
			tiles[counter] = (GameObject)Instantiate(prefab, positionTemp, Quaternion.identity);
			
			//Basic GameObject stuff.
			tiles[counter].transform.parent = this.transform;
			tiles[counter].name = "Tile" + counter;
			//Specific interaction with TileDisplay script.
			tiles[counter].GetComponent<TileDisplay>().width = width;
			//Reset position as making it a child messes with the values.
			tiles[counter].transform.position = positionTemp;
		}
	}
	void Update()
	{
		Time.timeScale = 0.02f;
		Move(17);
	}
	public void Move(float movement)
	{
		index += (int)Mathf.Floor(movement/width);
		offset += movement%width;
		/*/index += (int)Mathf.Floor(offset/width);
		offset = (offset%width)/width;*/
		DisplayCurrentTiles();
	}
	public void DisplayCurrentTiles()
	{
		ChangeIndex(index);
		ChangeCellsX(cellsX);
		//GetComponent<Transform>().position = new Vector2((offset -1) * width,0) + position;
		GetComponent<Transform>().position = new Vector2((offset - 1) * width,0) + position;
	}
	public void ChangeIndex(int newIndex)
	{
		//the variable index points to the member of the map array from which counting begins.
		//Due to the fact that a change to the value of index means a change to the values of
		//all members of the array, but a constant update loop would slow things down
		//unecessarily, changes to the value of index can only be made using the function,
		//making sure that the necessary changes are made once and once only.
		
		//Change value of index.
		index = newIndex;
		//Prepare for accessing map data.
		GameObject storage = GameObject.Find("MapStorage");
		MapStorage mapStorage = storage.GetComponent<MapStorage>();
		//Change the tiles array so that it reads a section of the map data, beginning
		//from the value of index.
		for(int counter = 0; counter < tiles.Length; counter++)
		{
			//Length of array we're reading from.
			int length = mapStorage.map.Length;
			if(index + counter >= length || index + counter < 0)
			{
				//If no such array member exists, then switch to default sprite texture
				//and hide the tile.
				tiles[counter].GetComponent<TileDisplay>().spriteIndex = 0;
				tiles[counter].GetComponent<TileDisplay>().show = false;
			}
			else
			{
				//Otherwise, set the sprite to that specified by the map array and then show
				//the tile.
				tiles[counter].GetComponent<TileDisplay>().spriteIndex = mapStorage.map[index + counter];
				tiles[counter].GetComponent<TileDisplay>().show = true;
			}
			tiles[counter].GetComponent<TileDisplay>().Refresh();
		}
	}
	public void ChangeCellsX(int newCellsx)
	{
		//CellX is the name of the variable controlling holding the number of tiles in the x direction
		//on screen. Thus, to update its value is to update the array. As a result of this, every time
		//it's value is changed, this function needs to be called, instead of the script it contains
		//running all the time and slowing down the game.
		//However, the copying is quite ineffiecient and would be better with a list class, but due to
		//a current unfamiliarity with c# class usage, this uses this clunky method. Recommmended that
		//resising takes place as little as possible.
		GameObject[] temporary = new GameObject[newCellsx];
		if(newCellsx < tiles.Length)
		{
			//If we are shortening the array, then we can delete the items that we
			//no longer need.
			for(int counter = newCellsx; counter < tiles.Length; counter++)
			{
				GameObject.Destroy(tiles[counter].gameObject);
			}
			System.Array.Copy(tiles, 0, temporary, 0, newCellsx);
			tiles = temporary;
		}
		else if(newCellsx > tiles.Length)
		{
			//If adding items to the array, we need to fill up that extra space,
			//so we'll create some new array members.
			
			//Searches through resources folder to find Tile prefab.
			prefab = (GameObject)Resources.Load("Prefabs/Tile", typeof(GameObject));
			
			System.Array.Copy(tiles, 0, temporary, 0, tiles.Length);
			//Store length of old tiles array to act as index to begin
			//editing from in the for loop.
			int oldLength = tiles.Length;
			tiles = temporary;
			for(int counter = oldLength; counter < tiles.Length; counter++)
			{
				//Create clone.
				Vector3 positionTemp = new Vector3(width * counter, 0, 0);
				tiles[counter] = (GameObject)Instantiate(prefab, positionTemp, Quaternion.identity);
				
				//Basic GameObject stuff.
				tiles[counter].transform.parent = this.transform;
				tiles[counter].name = "Tile" + counter;
				//Specific interaction with TileDisplay script.
				tiles[counter].GetComponent<TileDisplay>().width = width;
				//Reset position as making it a child messes with the values.
				tiles[counter].transform.localPosition = positionTemp;
			}
		}
		//Call change index function to make sure everything is sorted out within the new array.
		ChangeIndex(index);
	}
}
