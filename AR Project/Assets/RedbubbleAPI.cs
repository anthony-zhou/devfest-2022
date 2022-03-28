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

public class RedbubbleAPI
{
    public static RedbubbleResponse SearchRedbubble(String query) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://redbubble-api.herokuapp.com/search/{0}", query));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        RedbubbleResponse info = JsonUtility.FromJson<RedbubbleResponse>("{\"results\":" + jsonResponse + "}");
        return info;
    }
}
