using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GetLocation : MonoBehaviour {

	// Use this for initialization
    IEnumerator Start()
    {
        Text textLogger = GameObject.Find("LogText").GetComponent<Text>();
        textLogger.text = "Starting search location\n";

        if (!Input.location.isEnabledByUser) {
            textLogger.text += "Location service not enabled\n";
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            textLogger.text += "Timed out\n";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            textLogger.text += "Unable to determine device location\n";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            textLogger.text += "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}
