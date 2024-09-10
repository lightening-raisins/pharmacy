using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI����ݭn�W�[����
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class toPHP : MonoBehaviour
{
    public Text UItext; //�n�]���}���~��q�~���[�JUItext
    public Text MYtext;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void ConnectTophp()
    {
        StartCoroutine(Test());
    }
    IEnumerator Test()
    {
        string strText = "hello";
        string strUrl = string.Format("http://localhost/unitymysql/test.php?a={0}", strText);
        UnityWebRequest request = UnityWebRequest.Get(strUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
            yield break;
        }
        string html = request.downloadHandler.text;
        changeUItext(html);
        Debug.Log(html);
    }
    public void changeUItext(string html)
    { //�]���}���~��Q��Unity����ButtonĲ�o�覡�ӳ�X
        UItext.text = html;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void ConnectMysql()
    {
        StartCoroutine(Testdb());
    }
    IEnumerator Testdb()
    {
        string strUrl = "http://localhost/unitymysql/connectmysql.php";
        UnityWebRequest request = UnityWebRequest.Get(strUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
            yield break;
        }
        string html = request.downloadHandler.text;
        Debug.Log(html);
        var received_data = Regex.Split(html, "</next>");
        int dataNum = (received_data.Length - 1) / 2;
        Debug.Log("dataNum = " + dataNum.ToString());
        for (int i = 0; i < dataNum; i++)
        {
            string myhtml = "Name: " + received_data[2 * i] + " email: " + received_data[2 * i + 1];
            changeMYtext(myhtml);
            Debug.Log("Name: " + received_data[2 * i] + " email: " + received_data[2 * i + 1]);
        }
    }
    public void changeMYtext(string myhtml)
    { //�]���}���~��Q��Unity����ButtonĲ�o�覡�ӳ�X
        MYtext.text = myhtml;
    }
}
