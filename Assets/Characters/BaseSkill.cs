using UnityEngine;
using System.Collections;

public class BaseSkill : MonoBehaviour
{
    string skillName;

	// Use this for initialization
	void Start ()
    {
	
	}

    public string SkillName
    {
        get { return skillName; }
        set { skillName = value; }
    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
