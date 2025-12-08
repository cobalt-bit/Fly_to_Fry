using System.IO;
using UnityEngine;

namespace SaveData
{
    public static class JsonSavere
    {
        readonly static string ROOTPATH = Application.dataPath + "Data";

        public static void Save<T>(T data, string filename)
        {
            string path = ROOTPATH + "/" + filename + ".json";
            string jdata;
#if UNITY_EDITOR
            jdata = JsonUtility.ToJson(data, prettyPrint: true);
#else
            jdata = JsonUtility.ToJson(data, prettyPrint: false);
#endif
            using (StreamWriter sw = new(path, append:false))
            {
                sw.WriteLine(jdata);
            }
            Debug.Log(path);
        }

        public static T Load<T>(string filename)
        {
            string path = ROOTPATH + "/" + filename + ".json";
            return JsonUtility.FromJson<T>(path);
        }

        public static T LoadAndDelete<T>(string filename)
        {
            string path = ROOTPATH + "/" + filename + ".json";
            T data = JsonUtility.FromJson<T>(path);
            File.Delete(path);
            return data;
        }
    }
}