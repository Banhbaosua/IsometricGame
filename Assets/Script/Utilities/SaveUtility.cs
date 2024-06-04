using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;


public static class SaveUtility
{
    /// <summary>
    /// Save List of Type T to file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <param name="list"></param>
    public static void SaveToJSON<T>(string fileName, List<T> list)
    {
        var content = JSONHelper.FromListToJson(list.ToArray());
        WriteFile(GetPath(fileName), content);
    }

    /// <summary>
    /// Load List of Type T from Json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static List<T> LoadListFromJSON<T>(string fileName)
    {
        string content = ReadFile(GetPath(fileName));
        if (string.IsNullOrEmpty(content) || content == "{}")
            return new List<T>();

        List<T> list = JSONHelper.FromJsonToList<T>(content).ToList();
        return list;
    }

    /// <summary>
    /// Save Object of type T to file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <param name="obj"></param>
    public static void SaveToJSON<T>(string fileName, T obj)
    {
        var content = JsonUtility.ToJson(obj);
        WriteFile(GetPath(fileName), content);
    }

    /// <summary>
    /// Load Object of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static T LoadFromJSON<T>(string fileName)
    {
        string content = ReadFile(GetPath(fileName));
        if (string.IsNullOrEmpty(content) || content == "{}")
            return default;

        T obj = JsonUtility.FromJson<T>(content);
        return obj;
    }

    public static void LoadSOFromJSON(string fileName,object loadTo)
    {
        string content = ReadFile(GetPath(fileName));
        if (string.IsNullOrEmpty(content) || content == "{}")
            return ;

        JsonUtility.FromJsonOverwrite(content,loadTo);
    }

    public static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using(StreamWriter writer = new StreamWriter(fileStream)) 
        { 
            writer.Write(content);
        }
    }

    public static string ReadFile(string path) 
    { 
        if(File.Exists(path)) 
        { 
            using(StreamReader reader = new StreamReader(path)) 
            { 
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }

    public static string GetPath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
[Serializable]
public class SaveData<T>
{
     public T value;
    public SaveData(T newData)
    { 
        this.value = newData; 
    }
}
public static class JSONHelper
{
    public static T[] FromJsonToList<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.List;
    }

    public static string FromListToJson<T>(T[] stat)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.List = stat;
        return  JsonUtility.ToJson(wrapper.List);
    } 

    private class Wrapper<T>
    {
        public T[] List; 
    }
}