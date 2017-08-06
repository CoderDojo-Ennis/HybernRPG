[System.Serializable]
public class Game
{
    public int SceneIndex;          //which scene
    public int CPIndex;             //which checkpoint

    public Game(int cpindex)
    {
        //CPIndex = GameObject.Find("Player Physics Parent").GetComponent<PlayerStats>().checkPoint;
        CPIndex = cpindex;
        SceneIndex = CheckPointPositions.CheckPointsScene(CPIndex);
    }
}