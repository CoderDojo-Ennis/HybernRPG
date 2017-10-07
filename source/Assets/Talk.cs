using UnityEngine;

public class Talk : MonoBehaviour
{
    private JSONDialogueReader dialogue;
    private GameObject readyToSpeak;
    public int worldNumber;
    public string fileName;
    public string speaker;
    public string id;

    void OnEnable()
    {
        dialogue = GameObject.Find("TextBoxCanvas").GetComponent<JSONDialogueReader>();
        readyToSpeak = transform.GetChild(1).gameObject;
        readyToSpeak.SetActive(false);
    }

    void Update()
    {
        bool dist;

        if (dist = Vector3.Distance(FindClosestNPC().transform.position, GameObject.Find("Player Physics Parent").transform.position) < 5f && dialogue.talking == false)
        {
            readyToSpeak.SetActive(true);
            if (Input.GetKeyDown("e"))
            {
                dialogue.BeginDialogue(worldNumber, fileName, speaker, id);
            }
        }
        else
        {
            readyToSpeak.SetActive(false);
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
