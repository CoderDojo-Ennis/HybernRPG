using UnityEngine;

public class PelicanFight : PelicanBehaviour
{
    public void Think ()
    {
        switch (CurrentPhase)
        {
            case 1:
                PelicanBehaveOne();
                break;

            case 2:
                PelicanBehaveTwo();
                break;

            case 3:
                PelicanBehaveThree();
                break;

            default:
                break;
        }
    }

    private void PelicanBehaveOne()
    {
        //Peck 1 to 3 times
        int currentPeck = 0;
        int maxPeck;
        maxPeck = Random.Range(1, 4);
        while (currentPeck <= maxPeck)
        {
            Peck();
            currentPeck++;
        }
        //After pecking, WingAttack()
        if (currentPeck > maxPeck)
        {

        }
    }

    private void PelicanBehaveTwo()
    {
        Random.Range(1, 4);
    }

    private void PelicanBehaveThree()
    {
        Random.Range(1, 4);
    }
}
