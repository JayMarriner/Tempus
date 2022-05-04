using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackPuzzle : MonoBehaviour
{
    [Header("Move amt")]
    [SerializeField] Vector3 moveAmt;

    [Header("Controls")]
    [SerializeField] float speed;
    [SerializeField] float delay;

    Vector3 initPos;
    Vector3 goalPos;

    bool moveTowardTarget;
    // Start is called before the first frame update
    void Start()
    {
        initPos = gameObject.transform.position;
        goalPos = gameObject.transform.position + moveAmt;
        StartCoroutine(Movement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Movement()
    {
        yield return new WaitForSeconds(delay);
        while (gameObject.activeSelf)
        {
            if (Vector3.Distance(transform.position, goalPos) < 0.2f)
                moveTowardTarget = false;
            if (Vector3.Distance(transform.position, initPos) < 0.2f)
                moveTowardTarget = true;

            if (moveTowardTarget)
            {
                if (transform.position.x > goalPos.x)
                    transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);
                if (transform.position.x < goalPos.x)
                    transform.position += new Vector3(Time.deltaTime * speed, 0, 0);

                if (transform.position.y > goalPos.y)
                    transform.position -= new Vector3(0, Time.deltaTime * speed, 0);
                if (transform.position.y < goalPos.y)
                    transform.position += new Vector3(0, Time.deltaTime * speed, 0);

                if (transform.position.z > goalPos.z)
                    transform.position -= new Vector3(0, 0, Time.deltaTime * speed);
                if (transform.position.z < goalPos.z)
                    transform.position += new Vector3(0, 0, Time.deltaTime * speed);
            }
            else
            {
                if (transform.position.x > initPos.x)
                    transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);
                if (transform.position.x < initPos.x)
                    transform.position += new Vector3(Time.deltaTime * speed, 0, 0);

                if (transform.position.y > initPos.y)
                    transform.position -= new Vector3(0, Time.deltaTime * speed, 0);
                if (transform.position.y < initPos.y)
                    transform.position += new Vector3(0, Time.deltaTime * speed, 0);

                if (transform.position.z > initPos.z)
                    transform.position -= new Vector3(0, 0, Time.deltaTime * speed);
                if (transform.position.z < initPos.z)
                    transform.position += new Vector3(0, 0, Time.deltaTime * speed);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
