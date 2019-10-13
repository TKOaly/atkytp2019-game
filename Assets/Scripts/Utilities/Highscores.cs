using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class Highscores {
    private static string baseUrl = "https://atkytpgame.herokuapp.com/api/highscores";

    public static async Task<Highscore[]> GetTop10 () {
        string url = baseUrl + "/top";
        string json = await GetRequest (url);

        if (json.Length == 0) {
            return new Highscore[0];
        } else {
            return JsonHelper.FromJson<Highscore> (json);
        }
    }

    public static async Task<Highscore> GetOne (string id) {
        if (id == null || id.Length == 0) {
            return null;
        }
        string url = baseUrl + "/" + id;
        string json = await GetRequest (url);
        if (json.Length == 0) {
            return null;
        } else {
            return JsonUtility.FromJson<Highscore> (json);
        }
    }

    public static async Task<Highscore> Create (string user, string token) {
        Highscore h = new Highscore ();
        h.user = user;
        h.token = token;

        string jsonHighscore = JsonUtility.ToJson (h);
        string json = await PostRequest (baseUrl, jsonHighscore);
        if (json.Length == 0) {
            return null;
        } else {
            return JsonUtility.FromJson<Highscore> (json);
        }
    }

    public static async Task<Highscore> Update (string id, int score) {
        if (id == null || id.Length == 0 || score == 0) {
            return null;
        }
        string url = baseUrl + "/" + id;
        string jsonScore = "{ \"score\": " + score + "}";
        string json = await PutRequest (url, jsonScore);
        if (json.Length == 0) {
            return null;
        } else {
            return JsonUtility.FromJson<Highscore> (json);
        }
    }

    private static async Task<string> GetRequest (string url) {
        UnityWebRequest req = UnityWebRequest.Get (url);
        req.SetRequestHeader("X-Authorization-Token", "73phpQdL#9XvioX4E#Nxpi89");
        await req.SendWebRequest ();
        if (req.isNetworkError || req.isHttpError) {
            Debug.Log (req.error);
            return "";
        } else {
            return req.downloadHandler.text;
        }
    }

    private static async Task<string> PostRequest (string url, string body) {
        UnityWebRequest req = new UnityWebRequest (url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes (body);
        req.uploadHandler = (UploadHandler) new UploadHandlerRaw (bodyRaw);
        req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer ();
        req.SetRequestHeader ("Content-Type", "application/json");
        req.SetRequestHeader("X-Authorization-Token", "73phpQdL#9XvioX4E#Nxpi89");

        await req.SendWebRequest ();

        if (req.isNetworkError) {
            string error = "No connection try again later";
            Debug.Log (error);
            return "";
        } else if (req.isHttpError) {
            string error = req.downloadHandler.text;
            PlayerPrefs.SetString ("error", error);
            return "";
        }
        else {
            return req.downloadHandler.text;
        }
    }

    private static async Task<string> PutRequest (string url, string body) {
        UnityWebRequest req = new UnityWebRequest (url, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes (body);
        req.uploadHandler = (UploadHandler) new UploadHandlerRaw (bodyRaw);
        req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer ();
        req.SetRequestHeader ("Content-Type", "application/json");
        req.SetRequestHeader("X-Authorization-Token", "73phpQdL#9XvioX4E#Nxpi89");

        await req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError) {
            Debug.Log (req.downloadHandler.text);
            return "";
        } else {
            return req.downloadHandler.text;
        }
    }
}