using UnityEngine;

public class GridManager : MonoBehaviour
{
    //each level will contain its maps
    //every time a level is loaded, set a grid according to the level layout,
    //the player or enemies cannot gain height and will always walk on level 0,
    //get the level laid out so that only the cells who are the heighest on their y axis in all grids can be walked on
    
    private Level activeLevel;

    public Level ActiveLevel { get => activeLevel; }
}
