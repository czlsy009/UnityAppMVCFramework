using System;
using UnityEngine;
using System.Collections;
using System.IO;
using BlankFramework;

public class BlankGalleryScreenshotExample : MonoBehaviour
{
    void OnGUI()
    {
        if (GUILayout.Button("Save", GUILayout.Width(200), GUILayout.Height(200)))
        {
            Debug.Log(Time.realtimeSinceStartup);
            Debug.Log(DateTime.Now.ToLongTimeString());
            StartCoroutine(a());
        }
        if (GUILayout.Button("Save Gallery Screenshot ", GUILayout.Width(200), GUILayout.Height(200)))
        {
            Debug.Log(Time.realtimeSinceStartup);
            Debug.Log(DateTime.Now.ToLongTimeString());
            BlankGalleryScreenshot.Instance.SaveGalleryScreenshot(() => { });
        }
    }

    private IEnumerator a()
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, true);
        texture2D.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture2D.Apply();
        string filePath = Application.persistentDataPath + "/" + DateTime.Now.ToFileTime() + ".png";
        File.WriteAllBytes(filePath, texture2D.EncodeToPNG());
        BlankGalleryScreenshot.Instance.SaveGalleryScreenshot(filePath);
    }
}
