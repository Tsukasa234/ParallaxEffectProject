using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Necesitamos que el objeto que tenga este script tambien contenga un "SpriteRenderer"
[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxEffect : MonoBehaviour
{
    //Necesitamos una variable para la velocidad en la que ira el fondo
    [SerializeField] private float parallaxSpeed;
    [SerializeField] private float parallaxSpeedY;

    //tambien necesitamos una referencia al transform de la camara
    private Transform cameraTransform;

    //Necesitamos una variable para tomar la posicion inicial en X, si queremos tambien en Y
    private float startPositionX, startPositionY;

    //Para hacer un loop necesitamos conocer el tamaño del sprite que se repetira
    //Para eso obtendremos ese tamaño en X
    private float spriteSizeX;

    // Start is called before the first frame update
    void Start()
    {
        //asignamos el transform de la camara principal a la variable de la referencia
        cameraTransform = Camera.main.transform;
        //Obtenemos la posicion inicial
        startPositionX = transform.position.x;
        startPositionY = transform.position.y;
        //Obtendremos el tamaño del sprite de esta forma, obteniendo el componente, sus limites, su tamaño basado en sus limites, y en el eje X
        spriteSizeX = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //entonces obtenemos la distancia de la camara en X multiplicada por una velocidad
        float relativeDistance = cameraTransform.position.x * parallaxSpeed;
        float relativeDistanceY = cameraTransform.position.y * parallaxSpeedY;
        //Luego damos esa posicion en X para que se mueva el sprite junto con la camara
        transform.position = new Vector3(relativeDistance + startPositionX, startPositionY + relativeDistanceY, transform.position.z);

        //Ahora necesitamos obtener la posicion de la camara en X en relacion a nuestro sprite
        //Loop Parallax Effect
        //Para obtener la distancia de la camara necesitamos nuestra posicion del sprite en X, sumada a la velocidad -1
        float relativeCameraDist = cameraTransform.position.x * (1 - parallaxSpeed);
        //luego vemos que si la distancia relativa es mayor a la posicion inicial del sprite (que seria en medio del sprite) sumada al tamaño del sprite
        if (relativeCameraDist > startPositionX + spriteSizeX)
        {
            //De ser cierto la posicion inicial sera sumada con el tamaño del sprite
            //Esto pasara si la camara se mueve hacia la derecha
            startPositionX += spriteSizeX;
        }
        //tambien debemos ver que si la distancia es menor al anterior condicion, se debe restar la posicion inicial con el tamaño del sprite
        else if (relativeCameraDist < startPositionX - spriteSizeX)
        {
            //De ser asi se debe restar la posicion inicial con la del tamaño del sprite
            //Esto pasara si la camara se mueve hacia la izquierda
            startPositionX -= spriteSizeX;
        }
    }
}

//NT- por alguna razon me iba invertido el movimiento, asi que cualquier caso colocar el valor de la velocidad del parallax en negativo
//Si no funciona volver a colocarlo en positivo

//Para hacerlo en el eje Y es el mismo proceso