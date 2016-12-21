using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillsButton : MonoBehaviour
{
    List<GameObject> dropDownList;
    bool showCharacterSkills;

	// Use this for initialization
	void Start ()
    {
        dropDownList = new List<GameObject>();
        showCharacterSkills = true;
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    public void OnClick(GameObject Parent)
    {
        if(showCharacterSkills)
        {
            CharacterManager tempCharManager = (CharacterManager)FindObjectOfType(typeof(CharacterManager));
            List<BaseSkill> tempSkillsList = tempCharManager.CharacterSkills();

            for(int i = 0; i < tempSkillsList.Count; i++)
            {
                GameObject tempButton = (GameObject)Instantiate(Resources.Load("Prefabs/Drop Down Skills Button"));
                tempButton.transform.SetParent(Parent.transform);
                tempButton.transform.position = new Vector3(Parent.transform.position.x + 160, Parent.transform.position.y - (60 * i), 0);

                tempButton.transform.GetChild(0).GetComponent<Text>().text = tempSkillsList[i].SkillName;

                dropDownList.Add(tempButton);
            }
        }
        else
        {
            if (dropDownList.Count != 0)
            {
                for (int i = 0; i < dropDownList.Count; i++)
                {
                    Destroy(dropDownList[i]);
                }
            }
            dropDownList.Clear();
        }

        showCharacterSkills = !showCharacterSkills;
    }
}
