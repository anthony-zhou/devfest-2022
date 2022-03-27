using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

[Serializable]
public class RedbubbleResult {
    public String thumbnail;
    public String itemUrl;
    public String title;
    public String creator;
}

[Serializable]
public class RedbubbleResponse {
    public List<RedbubbleResult> results;
}

public class RedbubbleAPI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("INITIALIZING REDBUBBLE");
        RedbubbleResponse res = SearchRedbubble("cat");
        print(res);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private RedbubbleResponse SearchRedbubble(String query) {
        print(String.Format("https://redbubble-api.herokuapp.com/search/{0}", query));
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://redbubble-api.herokuapp.com/search/{0}", query));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        RedbubbleResponse info = JsonUtility.FromJson<RedbubbleResponse>("{\"results\":" + jsonResponse + "}");
        return info;
    }
}
