using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    private Camera m_Self;
    public Transform m_Point;




    private void Awake()
    {
        //m_Self = GetComponent<Camera>();
        //List<int> vs = new List<int>();
        //vs.Add(1);
        //vs.Add(2);
        //vs.Add(3);
        //vs.Add(4);

        //vs.Sort((a, b) =>
        //{
        //    if(a>b)
        //    {
        //        return -1;
        //    }
        //    return 0;
        //});
        //foreach(int i in vs)
        //{
        //    Debug.Log(i);
        //}
    }



    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {         
            int result = RandRate(new int[] { 50,50 }, 100);
            Debug.Log(result);
        }
        //m_Self.orthographicSize
        //计算当前屏幕的比率 w/h=cameraH/cameraW   cameraH=w/h*cameraW

        //float rate = (float)m_Self.pixelHeight / m_Self.pixelWidth;
            //(float)Screen.width / Screen.height;
        //float cameraH = rate * m_Self.orthographicSize;

        //m_Point.position = m_Self.transform.position + new Vector3(m_Self.orthographicSize / 2f, cameraH / 2f, 0);
    }

    /**减*/
    //rate:几率数组（%），  total：几率总和（100%）
    // Debug.Log(randRate(new int[] { 10, 5, 15, 20, 30, 5, 5,10 }, 100));
    public int RandRate(int[] rate, int total)
    {
        int rand = Random.Range(0, total + 1);
        for (int i = 0; i < rate.Length; i++)
        {
            rand -= rate[i];
            if (rand <= 0)
            {
                return i;
            }
        }
        return 0;
    }

}
