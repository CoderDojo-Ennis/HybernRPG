using UnityEngine;
using UnityEngine.EventSystems;

public class TextOnClick : MonoBehaviour
{
    private JSONDialogueReader dialouge;
    public int worldNumber;
    public string fileName;
    public string speaker;
    public string id;

    void OnEnable()
    {
        dialouge = GameObject.Find("TextBoxCanvas").GetComponent<JSONDialogueReader>();
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            bool dist;
            if (dist = Vector3.Distance(FindClosestNPC().transform.position, GameObject.Find("Player Physics Parent").transform.position) < 5f)
            {
                dialouge.BeginDialogue(worldNumber, fileName, speaker, id);
            }
        }
    }

    //Self Explanatory Name
    public GameObject FindClosestNPC()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("NPC");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
