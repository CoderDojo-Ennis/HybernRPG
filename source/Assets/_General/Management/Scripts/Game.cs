[System.Serializable]
public class Game
{
    public int SceneIndex;          //which scene
    public int CPIndex;             //which checkpoint

    public Game(int sceneindex, int cpindex)
    {
        CPIndex = cpindex;
        SceneIndex = sceneindex;
    }
}