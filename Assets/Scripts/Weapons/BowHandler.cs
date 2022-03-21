using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowHandler : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject arrowSpawnPoint;
    GameObject currentArrow;
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
        else if (pullingString && Input.GetKeyUp(inputManager.shoot))
            StartCoroutine(RelaxBow());
        else if (pullingString && Input.GetKeyUp(inputManager.aim))
            StartCoroutine(RelaxBow());

        if (!pullingString && currentArrow == null)
            currentArrow = Instantiate(arrowPrefab, arrowSpawnPoint.transform);
    }

    IEnumerator PullBow()
    {
        moveAmt = 0;
        if (currentArrow == null)
            currentArrow = Instantiate(arrowPrefab, arrowSpawnPoint.transform);
        pullingString = true;
        while (moveAmt < 2f)
        {
            print(moveAmt);
            //If the player is still holding the fire button then continue to pull the bowstring back until max amount.
            if (Input.GetKey(inputManager.shoot) && Input.GetKey(inputManager.aim))
            {
                //Move back the midpoint of the bowstring line renderer, move arrow back equal amount.
                line.SetPosition(1, new Vector3(line.GetPosition(1).x, line.GetPosition(1).y, line.GetPosition(1).z - 0.01f));

                //Move arrow back with string being pulled.
                currentArrow.transform.localPosition += new Vector3(0, 0, -0.01f);

                //Update moveamt to break the while loop.
                moveAmt += 0.01f;

                //Increase the powerAmt multiplier to increase power as the bow is pulled further back.
                powerAmt = moveAmt * 10;

                yield return new WaitForSeconds(0.001f);
            }
            else
            {
                StartCoroutine(RelaxBow());
                break;
            }
        }
    }

    IEnumerator RelaxBow()
    {
        //Bow is currently being relaxed, so set to true. bow can't be pulled until cooldown ends.
        relaxingBow = true;

        //Allow bow to return back to normal position over small amount of time .
        while (moveAmt > 0f)
        {
            line.SetPosition(1, new Vector3(line.GetPosition(1).x, line.GetPosition(1).y, line.GetPosition(1).z + 0.04f));
            if(currentArrow != null)
                currentArrow.transform.localPosition += new Vector3(0, 0, 0.01f);
            moveAmt -= 0.04f;
            yield return new WaitForSeconds(0.00001f);
        }

        //Pulling string set to false, bow is no longer being pulled at this point.
        pullingString = false;

        //Set bow back to the initial position, without this the bow slowly goes further and further in with each use.
        line.SetPosition(1, initialPos);
        if (currentArrow != null)
            currentArrow.transform.position = arrowSpawnPoint.transform.position;

        //Cooldown for the reload on arrow.
        yield return new WaitForSeconds(1f);

        relaxingBow = false;
    }
}
