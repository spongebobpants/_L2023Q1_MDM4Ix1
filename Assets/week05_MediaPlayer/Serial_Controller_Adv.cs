using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * �Ʒ��� System.IO.Ports�� ����ϱ� ���ؼ���
 * Unity Editor > Edit > Project Settings > Player > Other Settings > Configuration > Api Compatibility Level��
 * .NET 4.x�� �����ϰ� Unity Editor�� �ٽ� �����ؾ� �� * 
 */

using System.IO.Ports;
using System; // catch exception ó���� ���� ���
using System.Linq;

/*
Arduino Code
------------- 
int value = 0;
void setup() {
  Serial.begin(9600);
}

void loop() {
  Serial.print(value);
  Serial.println();
  delay(1000);
  value++;
}
------------- 
*/

public class Serial_Controller_Adv : MonoBehaviour
{
    /// <summary>
    /// �ø��� ����� ����ϴ� arduino ��ü ����
    /// </summary>
    SerialPort arduino;

    /// <summary>
    /// �ø��� ��Ʈ
    /// </summary>
    public string portName = "COM5"; // Windows
    //public string portName = "/dev/tty.usbmodem14201"; // OSX 

    /// <summary>
    /// �Ƶ��̳밡 ������ �����͸� �����ϴ� sting ����
    /// </summary>
    bool hasFoundPort = false;

    /// <summary>
    /// �Ƶ��̳밡 ������ �����͸� �����ϴ� sting ����
    /// </summary>
    public string serialIn;

    public GameObject Media_Controller;

    void Start()
    {
        ///
        // PC�� Ȱ��ȭ�� �ø��� ��Ʈ�� ��� ����
        ///
        string[] ports = SerialPort.GetPortNames();

        ///
        // ports �迭 �ȿ� �ִ� �� ���(����, port)�� ���
        foreach (string port in ports)
        {
            print(port);
        }

        if (ports.Contains(portName))
        {
            ///
            // arduino ��ü�� ��Ʈ �̸�, ��� �ӵ��� ���� �ʱ�ȭ
            ///
            arduino = new SerialPort(portName.ToString(), 9600);

            ///
            // arduino ��Ʈ ����
            ///
            arduino.Open();
            hasFoundPort = true;

            ///
            // �б� �۾��� ���ľ� �ϴ� ���� �ð�(�и���)
            ///
            arduino.ReadTimeout = 3000;
        }
        else
        {
            print("Arduino NOT Connected!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasFoundPort && arduino.IsOpen)
        {
            //serialIn = arduino.ReadLine();
            //print(serialIn);

            try
            {
                serialIn = arduino.ReadLine();
                print(serialIn);
                if (!string.IsNullOrEmpty(serialIn))
                {
                    serialIn = serialIn.Trim();
                    print("got " + serialIn);
                    if (serialIn.ToLower() == "5")
                    {                        
                        Media_Controller.SendMessage("PlayMedia");
                    }

                    if (serialIn.ToLower() == "10")
                    {
                        Media_Controller.SendMessage("StopMedia");
                    }
                }
            }
            catch (Exception e)
            {
                print(e);
            }

        }

    }

    void OnApplicationQuit()
    {
        if (hasFoundPort)
        {
            arduino.Close();
        }
    }
}
