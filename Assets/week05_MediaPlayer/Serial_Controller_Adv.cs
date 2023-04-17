using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 아래의 System.IO.Ports를 사용하기 위해서는
 * Unity Editor > Edit > Project Settings > Player > Other Settings > Configuration > Api Compatibility Level을
 * .NET 4.x로 설정하고 Unity Editor를 다시 시작해야 함 * 
 */

using System.IO.Ports;
using System; // catch exception 처리를 위해 사용
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
    /// 시리얼 통신을 담당하는 arduino 객체 생성
    /// </summary>
    SerialPort arduino;

    /// <summary>
    /// 시리얼 포트
    /// </summary>
    public string portName = "COM5"; // Windows
    //public string portName = "/dev/tty.usbmodem14201"; // OSX 

    /// <summary>
    /// 아두이노가 보내는 데이터를 저장하는 sting 변수
    /// </summary>
    bool hasFoundPort = false;

    /// <summary>
    /// 아두이노가 보내는 데이터를 저장하는 sting 변수
    /// </summary>
    public string serialIn;

    public GameObject Media_Controller;

    void Start()
    {
        ///
        // PC에 활성화된 시리얼 포트의 목록 추출
        ///
        string[] ports = SerialPort.GetPortNames();

        ///
        // ports 배열 안에 있는 각 요소(가령, port)를 출력
        foreach (string port in ports)
        {
            print(port);
        }

        if (ports.Contains(portName))
        {
            ///
            // arduino 객체를 포트 이름, 통신 속도에 맞춰 초기화
            ///
            arduino = new SerialPort(portName.ToString(), 9600);

            ///
            // arduino 포트 개방
            ///
            arduino.Open();
            hasFoundPort = true;

            ///
            // 읽기 작업을 마쳐야 하는 제한 시간(밀리초)
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
