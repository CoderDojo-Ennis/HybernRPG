using System;
using UnityEngine;

[Serializable]
public static class CheckPointPositions
{
    //Gives the position of each numbered checkpoint
	public static Vector3 CheckPointsVector3(int CPIndex)
    {
        switch (CPIndex)
        {
            case 0:
                return new Vector3(1, 1, 0);

            case 1:
                return new Vector3(0, 0, 0);

            case 2:
                return new Vector3(0, 0, 0);

            default:
                return new Vector3(0, 0, 0);
        }
    }

    //Gives the scene of the checkpoint
    public static int CheckPointsScene(int CPIndex)
    {
        //Presuming 5 checkpoints per scene
        //Count starts at 0
        //World One
        if (CPIndex < 5)
        {
            if (CPIndex == 0)
                return 5;
            
            return 1;
        }

        //World Two
        else if (CPIndex < 10)
        {
            return 2;
        }

        //World Three
        else if (CPIndex < 15)
        {
            return 3;
        }

        //World Four
        else if (CPIndex < 20)
        {
            return 4;
        }

        //World Five
        else
        {
            return 5;
        }
    }
}
