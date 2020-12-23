using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TileManager : MonoBehaviour
{
    public static TileManager instance = null;
  
    //I Need the player to access a list or array of the tiles so im just playing around with options
    [SerializeField] private GameObject[] tilearray;
    public GameObject[] Tilearray { get => tilearray; set => tilearray = value; }
   public Dictionary<Vector2, GameObject> TileDic = new Dictionary<Vector2, GameObject>();
    private bool check = false;
    #region unneccesary_stuff
    //putting this stuff here for now as they are unneccesary and can be erased later
    //[SerializeField] private GameObject[][] tile;
    //public List<Tile> tileList = new List<Tile>();
    #endregion unneccesary_stuff
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        #region ZachNotes
      //Reference to derek I could only figure out a two step process to populate my dictionary
      //create an array first then use the array to populate the dictionary
      //lemme know if you find something better
        #endregion ZachNotes
        Tilearray = GameObject.FindGameObjectsWithTag("Tile");
        for (int i = 0; i < Tilearray.Length; i++)
        {
            TileDic.Add(tilearray[i].GetComponent<Tile>().Loc, tilearray[i]);
        }
        
    }
   
    public GameObject GetTileDic(Vector2 coordinates)
    {
        #region ZachNotes
        //GetTileDicDic takes the coordinates of a tile and will give you that gameobject (Not the tile script though)
        //so if you need to manipulate a specific tile use this
        #endregion ZachNotes
        if (TileDic.ContainsKey(coordinates))
        {
            if (TileDic[coordinates] != null)
                return TileDic[coordinates];
            else
                return null;
        }
        else
            return null;
    }
    public GameObject GetTileDicMove(Vector2 coordinates,Tile currenttile)
    {

        if (TileDic.ContainsKey(coordinates))
            return TileDic[coordinates];
        else
            return currenttile.gameObject;
    }
    public Tile GetAdjacentTile(int situation,Tile currentTile)
    {
        switch (situation)
        {
            case 1:
                GameObject tile = GetTileDic(new Vector2(currentTile.X - 1, currentTile.Y));
                if (tile != null)
                    return GetTileDic(new Vector2(currentTile.X - 1, currentTile.Y)).GetComponent<Tile>();
                else return null;
            case 2:
                tile = GetTileDic(new Vector2(currentTile.X + 1, currentTile.Y));
                if (tile != null)
                {
                    return tile.GetComponent<Tile>();
                }
                else return null;       
            case 3:
                tile = GetTileDic(new Vector2(currentTile.X, currentTile.Y - 1));
                if (tile != null)
                    return tile.GetComponent<Tile>();
                else return null;
            case 4:
                tile = GetTileDic(new Vector2(currentTile.X, currentTile.Y + 1));
                if (tile != null)
                    return tile.GetComponent<Tile>();
                else return null;
               
            default:
                return null;
        }

    }
    public void DeSelectAllTiles()
    {
        foreach(GameObject tile in tilearray)
        {
            tile.GetComponent<Tile>().IsSelected = false;
        }
    }

   
}
