using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Linq;

public class PlayerWithController : MonoBehaviour 
{
	[Range(0, 100)]
	public int RangeUp;

	
	public string portName; //Nombre del puerto al que está conectado el Arduino
	public int Serialspeed; //Velocidad de transferencia 

	string[] stringDelimiters = new string[] { ":", "R", };

		
	public Transform Giros;

	private SerialPort arduino;

	

	//

	public Animator anim;
	public AudioSource[] audioSrc;

	public float coins = 0;

	public float speed;
	public float rotationSpeed;
	public float isMoving = 0.0f;
	public float isRotating = 0.0f;



	// Use this for initialization
	void Start()
	{
		arduino = new SerialPort(portName, Serialspeed, Parity.None, 8, StopBits.One);
		arduino.DtrEnable = false;
		arduino.ReadTimeout = 1;
		arduino.WriteTimeout = 1;
		arduino.Open();
		isMoving = 0.0f;
		coins = 0;
	}

	void Update()
    {
		if (isMoving == 0.0f && isRotating == 0.0f)
		{
			anim.SetFloat("isMoving", isMoving);
			anim.SetFloat("isRotating", isRotating);
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}




		if (arduino.IsOpen)
		{
			try
			{
				string values = arduino.ReadLine();
				string[] valueArray = values.Split(',');
				//print(values);
				//Debug.Log(valueArray[2]);
				Debug.Log(values);
                //// Esta sección puede ser modificada para dar el comportamiento del  control,
                //// Esta seccion corresponde a los botones
                //// En este caso los valores de los botones son del 1 - 4
                //// Como se muestra a continuacion los valores de los botones están dados por la primera posicion del Array 
                //// Es decir,  Convert.ToInt32(valueArray[0]) <-  En la posicion 0 del valueArray el programa comprobara que valor ha sido mandado
                //// Y dependiendo de este, tendra un comportamiento diferente dependiendo ya del desarrollador que es lo que quiera hacer
                ////

                //// Ejemplo
                //// El botón al ser valor 1 irá a la derecha
                //if ((Convert.ToInt32(valueArray[0])) == 1)
                //{
                //	isRotating = 1.0f;
                //	transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
                //	anim.SetFloat("isRotating", isRotating);

                //	if (isRotating == 1.0f && !audioSrc[1].isPlaying && isMoving == 0.0f)
                //	{
                //		audioSrc[1].Play();
                //	}
                //}
                ////El botón  al ser valor 2 irá a la izquierda
                //else if ((Convert.ToInt32(valueArray[0])) == 2)
                //{
                //	isRotating = -1.0f;
                //	transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
                //	anim.SetFloat("isRotating", isRotating);

                //	if (isRotating == -1.0f && !audioSrc[1].isPlaying && isMoving == 0.0f)
                //	{
                //		audioSrc[1].Play();
                //	}
                //}
                ////El botón  al ser valor 3 irá hacia arriba
                //else if ((Convert.ToInt32(valueArray[0])) == 3)
                //{
                //	isMoving = 1.0f;
                //	transform.Translate(Vector3.forward * speed);
                //	anim.SetFloat("isMoving", isMoving);

                //	if (isMoving == 1.0f && !audioSrc[0].isPlaying)
                //	{
                //		audioSrc[0].Play();
                //	}
                //}
                ////El botón al ser valor 4 irá hacia abajo
                //else if ((Convert.ToInt32(valueArray[0])) == 4)
                //{
                //	isMoving = -1.0f;
                //	transform.Translate(Vector3.back * speed);
                //	anim.SetFloat("isMoving", isMoving);

                //	if (isMoving == -1.0f && !audioSrc[0].isPlaying)
                //	{
                //		audioSrc[0].Play();
                //	}
                //}
                //else
                //{
                //	isMoving = 0.0f;
                //	audioSrc[0].Stop();

                //	isRotating = 0.0f;
                //	audioSrc[1].Stop();
                //}


                // Fin de la sección de botones
                //////////////////////////////////////////////////////////


                // Esta seccion es para el comportamiento del Joystick en este caso el josytick solo rota, por motivos de prueba,
                // Pero al igual que en las secciones anteriores los valores que corresponden al joystick dentro del valueArray 
                // Se encuentran en la posicion 2 y 3 siendo X y Y respectivament.


                // El Joystick irá Derecha
                if ((Convert.ToInt32(valueArray[2])) == RangeUp)
                {
                    isRotating = 1.0f;
                    transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
                    anim.SetFloat("isRotating", isRotating);

                    if (isRotating == 1.0f && !audioSrc[1].isPlaying && isMoving == 0.0f)
                    {
                        audioSrc[1].Play();
                    }
                }
                //El Joystick irá Izquierda
                else if ((Convert.ToInt32(valueArray[2])) == 1022 || (Convert.ToInt32(valueArray[2]) == 1023))
                {
                    isRotating = -1.0f;
                    transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
                    anim.SetFloat("isRotating", isRotating);

                    if (isRotating == -1.0f && !audioSrc[1].isPlaying && isMoving == 0.0f)
                    {
                        audioSrc[1].Play();
                    }
                }
                //El Joystick irá hacia arriba
                else if ((Convert.ToInt32(valueArray[3])) == RangeUp)
                {
                    isMoving = 1.0f;
                    transform.Translate(Vector3.forward * speed);
                    anim.SetFloat("isMoving", isMoving);

                    if (isMoving == 1.0f && !audioSrc[0].isPlaying)
                    {
                        audioSrc[0].Play();
                    }
                }
                //El Joystick irá hacia abajo
                else if ((Convert.ToInt32(valueArray[3])) == 1023)
				{
                    isMoving = -1.0f;
                    transform.Translate(Vector3.back * speed);
                    anim.SetFloat("isMoving", isMoving);

                    if (isMoving == -1.0f && !audioSrc[0].isPlaying)
                    {
                        audioSrc[0].Play();
                    }
                }
                else
				{
					isMoving = 0.0f;
					audioSrc[0].Stop();

					isRotating = 0.0f;
					audioSrc[1].Stop();
				}



				// Fin de la seccion de Joystick

				
				// Seccion Giroscopio
				// Esta seccion difiere un poco de la anterior ya que no se accede a ella por medio de la posicion en el ValueArray sino que checa 
				// Los valores que comiencen con una R y tomarlos como los valores del giroscopio

				//string cmd = CheckForRecievedData();
				//if (cmd.StartsWith("R")) //Encuentra los valores que empiezan con R para despues separarlos ya que estos corresponden al giroscopio
				//{
				//	Vector3 accl = ParseAccelerometerData(cmd);

				//	Giros.transform.rotation = Quaternion.Slerp(Giros.transform.rotation, Quaternion.Euler(accl), Time.deltaTime * 2f);
				//}

				//if (Input.GetKeyDown(KeyCode.Escape) && arduino.IsOpen)
				//	arduino.Close();

				//Fin seccion Giroscopio




			}
			catch // Prueba de que los valores no se están mandando por que no se pudo inicializar la comunicacion con el Arduino
			{
				////Debug.Log("Can't show values ");
				//string valuesF = arduino.ReadLine();
				//string[] valueArrayF = valuesF.Split(',');
				//print(valuesF);
			}
		}

	}

	Vector3 lastAccData = Vector3.zero;
	Vector3 ParseAccelerometerData(string data)
	{
		try
		{
			string[] splitResult = data.Split(stringDelimiters, StringSplitOptions.RemoveEmptyEntries);
			int x = int.Parse(splitResult[0]);
			int y = int.Parse(splitResult[1]);
			int z = int.Parse(splitResult[2]);
			lastAccData = new Vector3(x, y, z);
			return lastAccData;
		}
		catch { Debug.Log("Malformed Serial Transmisison"); return lastAccData; }
	} // Parseo de los elementos del giroscopio

	public string CheckForRecievedData()
	{
		try
		{
			string inData = arduino.ReadLine();
			int inSize = inData.Count();
			if (inSize > 0)
			{
				Debug.Log("ARDUINO->|| " + inData + " ||MSG SIZE:" + inSize.ToString());
			}

			inSize = 0;
			arduino.BaseStream.Flush();
			arduino.DiscardInBuffer();
			return inData;
		}
		catch { return string.Empty; }
	} // Funcion para checar los valores que el Arduino está mandando a Unity más en especifico la del giroscopio

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Coin")
		{
			Score.scoreValue += 1;
			Destroy(other.gameObject);
			audioSrc[2].Play();
			coins++;

		}

	}

}




