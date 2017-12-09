using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenIndicator : MonoBehaviour
{
    public GameObject player;

    public GameObject arrowPrefab;
    private GameObject arrow;

    public GameObject indicatorPrefab;
    private GameObject indicator;

    List<GameObject> arrowPool = new List<GameObject>();
    int arrowPoolCursor = 0;

    List<GameObject> indicatorPool = new List<GameObject>();
    int indicatorPoolCursor = 0;

    List<GameObject> enemies = new List<GameObject>();

    private void LateUpdate()
    {
        Paint();
    }

    private void Paint()
    {
        ResetPool();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(var enemy in enemies)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);

            Color color;
            color = Color.red;

            if (screenPos.z > 0 &&
                screenPos.x > 0 && screenPos.x < Screen.width &&
                screenPos.y > 0 && screenPos.y < Screen.height)
            {
                /*
                arrow.gameObject.SetActive(false);
                indicator.gameObject.SetActive(true);
                */
                GameObject indicatorArrow = GetIndicator();
                indicatorArrow.GetComponent<SpriteRenderer>().color = color;
                indicatorArrow.transform.localPosition = screenPos;

                indicatorArrow.transform.LookAt(player.transform);
                //Debug.Log(indicatorArrow.transform.position);
            }
            else
            {/*
                arrow.gameObject.SetActive(true);
                indicator.gameObject.SetActive(false);*/
                if (enemy.GetComponent<EnemyInfo>().DangerApproaching)
                {
                    if (screenPos.z < 0)
                    {
                        // stuff is flipped when behind us
                        screenPos *= -1;
                    }

                    Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

                    // make (0,0) the center of the screen instead of bottom left
                    screenPos -= screenCenter;

                    // find angle from center of screen to mouse position
                    float angle = Mathf.Atan2(screenPos.y, screenPos.x);
                    angle -= 90 * Mathf.Deg2Rad;

                    float cos = Mathf.Cos(angle);
                    float sin = -Mathf.Sin(angle);

                    screenPos = screenCenter + new Vector3(sin * 150, cos * 150, 0);

                    // y=mx+b format
                    float m = cos / sin;

                    Vector3 screenBounds = screenCenter * 0.9f;

                    // check up and down first
                    if (cos > 0)
                    {
                        screenPos = new Vector3(screenBounds.y / m, screenBounds.y, 0);
                    }
                    else
                    {
                        // down
                        screenPos = new Vector3(-screenBounds.y / m, -screenBounds.y, 0);
                    }

                    // if out of bounds, get point on appropriate side
                    if (screenPos.x > screenBounds.x)
                    {
                        // out of bounds, must be on the right
                        screenPos = new Vector3(screenBounds.x, screenBounds.x * m, 0);
                    }
                    else // else in bounds
                    {
                        if (screenPos.x < -screenBounds.x)
                        {
                            // out if bounds left
                            screenPos = new Vector3(-screenBounds.x, -screenBounds.x * m, 0);
                        }
                    }

                    // remove coordinate translation
                    screenPos += screenCenter;

                    arrow = GetArrow();
                    arrow.GetComponent<SpriteRenderer>().color = color;
                    arrow.transform.localPosition = screenPos;
                    arrow.transform.localRotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
                }
            }
        }
        CleanPool();
    }

    void ResetPool()
    {
        indicatorPoolCursor = 0;
        arrowPoolCursor = 0;
    }

    GameObject GetArrow()
    {
        GameObject output;
        if(arrowPoolCursor < arrowPool.Count)
        {
            output = arrowPool[arrowPoolCursor]; // get existing
        }
        else
        {
            output = Instantiate(arrowPrefab);
            output.transform.parent = transform;
            output.transform.localScale = new Vector3(100, 100, 100);
            arrowPool.Add(output);
        }

        arrowPoolCursor++;

        return output;
    }

    GameObject GetIndicator()
    {
        GameObject output;
        if (indicatorPoolCursor < indicatorPool.Count)
        {
            output = indicatorPool[indicatorPoolCursor]; // get existing
        }
        else
        {
            output = Instantiate(indicatorPrefab);
            output.transform.parent = transform;
            output.transform.localScale = new Vector3(300, 300, 300);
            indicatorPool.Add(output);
        }

        indicatorPoolCursor++;

        return output;
    }

    void CleanPool()
    {
        while (indicatorPool.Count > indicatorPoolCursor)
        {
            GameObject obj = indicatorPool[indicatorPool.Count - 1];
            indicatorPool.Remove(obj);
            Destroy(obj.gameObject);
        }

        while (arrowPool.Count > arrowPoolCursor)
        {
            GameObject obj2 = arrowPool[arrowPool.Count - 1];
            arrowPool.Remove(obj2);
            Destroy(obj2.gameObject);
        }
    }

    #region SecondTry
    /*
    public GameObject goTarget;

    void Update()
    {
        PositionArrow();
    }

    void PositionArrow()
    {
        GetComponent<Renderer>().enabled = false;

        Vector3 v3Pos = Camera.main.WorldToViewportPoint(goTarget.transform.position);

        if (v3Pos.z < Camera.main.nearClipPlane)
            return;  // Object is behind the camera

        if (v3Pos.x >= 0.0f && v3Pos.x <= 1.0f && v3Pos.y >= 0.0f && v3Pos.y <= 1.0f)
            return; // Object center is visible

        GetComponent<Renderer>().enabled = true;
        v3Pos.x -= 0.5f;  // Translate to use center of viewport
        v3Pos.y -= 0.5f;
        v3Pos.z = 0;      // I think I can do this rather than do a 
                          //   a full projection onto the plane

        float fAngle = Mathf.Atan2(v3Pos.x, v3Pos.y);
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, -fAngle * Mathf.Rad2Deg);

        v3Pos.x = 0.5f * Mathf.Sin(fAngle) + 0.5f;  // Place on ellipse touching 
        v3Pos.y = 0.5f * Mathf.Cos(fAngle) + 0.5f;  //   side of viewport
        v3Pos.z = Camera.main.nearClipPlane + 0.01f;  // Looking from neg to pos Z;
        transform.position = Camera.main.ViewportToWorldPoint(v3Pos);
    }*/
    #endregion
    #region FirstTry
    /*
    [SerializeField]
    private Transform t;
    [SerializeField]
    private Transform t2;
    [SerializeField]
    private Transform player;

    private void Update()
    {
        Vector3 targetPosOnScreen = Camera.main.WorldToScreenPoint(t.position);

        if (OnScreen(targetPosOnScreen))
        {
            //Some code to destroy indicator or make it invisible
            return;
        }

        Vector3 center = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        float angle = Mathf.Atan2(targetPosOnScreen.y - center.y, targetPosOnScreen.x - center.x) * Mathf.Rad2Deg;

        float coef;
        if (Screen.width > Screen.height)
            coef = Screen.width / Screen.height;
        else
            coef = Screen.height / Screen.width;

        float degreeRange = 360f / (coef + 1);

        if (angle < 0) angle = angle + 360;
        int edgeLine;
        if (angle < degreeRange / 4f) edgeLine = 0;
        else if (angle < 180 - degreeRange / 4f) edgeLine = 1;
        else if (angle < 180 + degreeRange / 4f) edgeLine = 2;
        else if (angle < 360 - degreeRange / 4f) edgeLine = 3;
        else edgeLine = 0;

        t2.position = Camera.main.ScreenToWorldPoint(Intersect(edgeLine, center, targetPosOnScreen) + new Vector3(0, 0, 10));
        t2.eulerAngles = new Vector3(0, 0, angle);

        t2.LookAt(player);
    }

    Vector3 Intersect(int edgeLine, Vector3 line2point1, Vector3 line2point2)
    {
        float[] A1 = { -Screen.height, 0, Screen.height, 0 };
        float[] B1 = { 0, -Screen.width, 0, Screen.width };
        float[] C1 = { -Screen.width * Screen.height, -Screen.width * Screen.height, 0, 0 };

        float A2 = line2point2.y - line2point1.y;
        float B2 = line2point1.x - line2point2.x;
        float C2 = A2 * line2point1.x + B2 * line2point1.y;

        float det = A1[edgeLine] * B2 - A2 * B1[edgeLine];

        return new Vector3((B2 * C1[edgeLine] - B1[edgeLine] * C2) / det, (A1[edgeLine] * C2 - A2 * C1[edgeLine]) / det, 0);
    }

    bool OnScreen(Vector2 input)
    {
        return !(input.x > Screen.width || input.x < 0 || input.y > Screen.height || input.y < 0);
    }
    */

    #endregion
}
