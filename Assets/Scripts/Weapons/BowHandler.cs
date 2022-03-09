using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowHandler : MonoBehaviour
{
    LineRenderer line;
    InputManager inputManager;
    bool pullingString;
    bool relaxingBow;
    Vector3 initialPos;
    float powerAmt;
    float moveAmt;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponentInChildren<LineRenderer>();
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
        initialPos = line.GetPosition(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(inputManager.shoot) && !pullingString)
            StartCoroutine(PullBow());
    }

    IEnumerator PullBow()
    {
        moveAmt = 0;
        pullingString = true;
        while (moveAmt < 0.1f)
        {
            print(moveAmt);
            //If the player is still holding the fire button then continue to pull the bowstring back until max amount.
            if (Input.GetKey(inputManager.shoot) && Input.GetKey(inputManager.aim))
            {
                //Move back the midpoint of the bowstring line renderer, move arrow back equal amount.
                line.SetPosition(1, new Vector3(line.GetPosition(1).x, line.GetPosition(1).y, line.GetPosition(1).z - 0.01f));

                //Update moveamt to break the while loop.
                moveAmt += 0.01f;

                //Increase the powerAmt multiplier to increase power as the bow is pulled further back.
                powerAmt = moveAmt * 10;

                yield return new WaitForSeconds(0.02f);
            }
            else
            {
                StartCoroutine(RelaxBow());
                break;
            }
            pullingString = false;
        }
    }

    IEnumerator RelaxBow()
    {
        //Bow is currently being relaxed, so set to true. bow can't be pulled until cooldown ends.
        relaxingBow = true;

        //Allow bow to return back to normal position over small amount of time .
        while (moveAmt > 0f)
        {
            line.SetPosition(1, new Vector3(line.GetPosition(1).x, line.GetPosition(1).y, line.GetPosition(1).z - 0.01f));
            moveAmt -= 0.02f;
            yield return new WaitForSeconds(0.0005f);
        }

        //Pulling string set to false, bow is no longer being pulled at this point.
        pullingString = false;

        //Set bow back to the initial position, without this the bow slowly goes further and further in with each use.
        line.SetPosition(1, initialPos);

        //Cooldown for the reload on arrow.
        yield return new WaitForSeconds(1f);

        relaxingBow = false;
    }
}
