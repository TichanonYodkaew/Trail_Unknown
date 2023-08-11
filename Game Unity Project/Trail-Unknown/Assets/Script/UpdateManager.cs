using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UpdateManager : MonoBehaviour
{
    public string folderUrl;
    public string versionFileName = "version.txt";
    public string patchFileName = "update.zip";

    public Text updateText;

    private string currentVersion;
    private bool updateAvailable = false;

    private void Start()
    {
        // Load the current version from the local "version.txt" file
        string versionFilePath = Path.Combine(Application.streamingAssetsPath, versionFileName);
        if (File.Exists(versionFilePath))
        {
            currentVersion = File.ReadAllText(versionFilePath).Trim();
        }
        else
        {
            Debug.LogWarning("Version file not found: " + versionFilePath);
            currentVersion = "0.0";
        }
    }

    private void OnGUI()
    {
        if (updateAvailable)
        {
            if (GUILayout.Button("Update"))
            {
                StartCoroutine(DownloadUpdate());
            }
        }
    }

    public void CheckForUpdates()
    {
        StartCoroutine(DownloadVersionFile());
    }

    private IEnumerator DownloadVersionFile()
    {
        UnityWebRequest versionRequest = UnityWebRequest.Get(folderUrl + "/" + versionFileName + "?alt=media");
        yield return versionRequest.SendWebRequest();

        if (versionRequest.result == UnityWebRequest.Result.ConnectionError || versionRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            updateText.text = "Error downloading version file: " + versionRequest.error;
            yield break;
        }

        string newVersion = versionRequest.downloadHandler.text.Trim();

        if (newVersion != currentVersion)
        {
            updateText.text = "Update available. Click the Update button to download and install.";

            updateAvailable = true;
        }
        else
        {
            updateText.text = "Game is up-to-date.";
        }
    }

    private IEnumerator DownloadUpdate()
    {
        UnityWebRequest downloadRequest = UnityWebRequest.Get(folderUrl + "/" + patchFileName + "?alt=media");
        downloadRequest.downloadHandler = new DownloadHandlerFile(Application.dataPath + "/../" + patchFileName);
        yield return downloadRequest.SendWebRequest();

        if (downloadRequest.result == UnityWebRequest.Result.ConnectionError || downloadRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            updateText.text = "Failed to download update: " + downloadRequest.error;
        }
        else
        {
            updateText.text = "Update downloaded. Installing...";

            System.IO.Compression.ZipFile.ExtractToDirectory(Application.dataPath + "/../" + patchFileName, Application.dataPath);

            updateText.text = "Update installed. Restarting game...";
            yield return new WaitForSeconds(2);

            System.Diagnostics.Process.Start(Application.dataPath + "/../" + Application.productName + ".exe");
            Application.Quit();
        }
    }
}
