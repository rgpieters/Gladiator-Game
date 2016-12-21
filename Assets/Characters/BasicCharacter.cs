using UnityEngine;
using System.Collections;

public class BasicCharacter : BaseCharacter
{

	// Use this for initialization
	void Start ()
    {

	}

    // Update is called once per frame
    protected new void Update()
    {
        if (isCharacterTurn)
        {
            if (Input.GetButtonDown("Mouse Left Button"))
            {
                PathManager tempPathManager = (PathManager)FindObjectOfType(typeof(PathManager));

                if (tempPathManager.ShowMarker)
                {
                    moveCharacter = true;
                }
            }

            if (!moveCharacter)
                return;

            if (pathIndex == -1 || pathIndex == pathList.Count)
                return;

            base.Update();
        }
    }
}
