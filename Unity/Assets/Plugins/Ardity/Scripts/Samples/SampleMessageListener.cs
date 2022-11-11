/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * When creating your message listeners you need to implement these two methods:
 *  - OnMessageArrived
 *  - OnConnectionEvent
 */
public class SampleMessageListener : MonoBehaviour
{
    [SerializeField] private Text text;
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        Debug.Log("Message arrived: " + msg);
        switch(msg)
        {
            case "KM_KEYUP (1)" :
            text.text = "1";
            break;

            case "KM_KEYDOWN (1)" :
            text.text = "1 CARALHO";
            break;

            case "KM_KEYUP (2)" :
            text.text = "2";
            break;

            case "KM_KEYDOWN (2)" :
            text.text = "2 CARALHO";
            break;

            case "KM_KEYUP (3)" :
            text.text = "3";
            break;

            case "KM_KEYDOWN (3)" :
            text.text = "3 CARALHO";
            break;

            case "KM_KEYUP (4)" :
            text.text = "4";
            break;

            case "KM_KEYDOWN (4)" :
            text.text = "4 CARALHO";
            break;

            case "KM_KEYUP (5)" :
            text.text = "5";
            break;

            case "KM_KEYDOWN (5)" :
            text.text = "5 CARALHO";
            break;

            case "KM_KEYUP (6)" :
            text.text = "6";
            break;

            case "KM_KEYDOWN (6)" :
            text.text = "6 CARALHO";
            break;

            case "KM_KEYUP (7)" :
            text.text = "7";
            break;

            case "KM_KEYDOWN (7)" :
            text.text = "7 SOLTO CARALHO";
            break;

            case "KM_KEYUP (8)" :
            text.text = "8";
            break;

            case "KM_KEYDOWN (8)" :
            text.text = "8 CARALHO";
            break;

            case "KM_KEYUP (9)" :
            text.text = "9";
            break;

            case "KM_KEYDOWN (9)" :
            text.text = "9 CARALHO";
            break;


        }
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}
