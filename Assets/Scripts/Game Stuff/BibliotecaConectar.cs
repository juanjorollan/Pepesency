using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este script conectará el juego con una base de datos MySQL y cogerá los datos para mostrarlos en una interfaz
public class BibliotecaConectar : MonoBehaviour
{
    public string servidorBaseDatos;
    public string nombreBaseDatos;
    public string usuarioBaseDatos;
    public string contraseñaBaseDatos;

    private string datosConexion;
    private MySqlConnection conexion;

    public Pepe personaje;
    public Texture2D foto;

    private int numPag=0;

    public Text nombre;
    public Text descripcion;
    public Image fotografia;

    List<Pepe> listapepes = null;

    /*Asigno los valores necesarios para conectarme a la base de datos y se los paso a una variable para luego conectarme con esa variable.
     Cojo la imagen, el nombre y la descripción del primer elemento de la lista para mostrarla nada mas abra la enciclopedia,*/
    IEnumerator Start()
    {
        servidorBaseDatos = "flo.no-ip.info";
        nombreBaseDatos = "Enciclopedia";
        usuarioBaseDatos = "Juanjo";
        contraseñaBaseDatos = "1234";

        datosConexion = "Server=" + servidorBaseDatos
                    + ";Database=" + nombreBaseDatos
                    + ";Uid=" + usuarioBaseDatos
                    + ";Pwd=" + contraseñaBaseDatos
                    + ";";

        listapepes = SacarDatos();

        yield return 0;
        WWW imagen = new WWW(listapepes[0].foto);
        yield return imagen;
        foto = imagen.texture;
        nombre.text = listapepes[0].nombre;
        descripcion.text = listapepes[0].descripcion;
        fotografia.sprite = Sprite.Create(foto, new Rect(0, 0, 600, 600), new Vector2(0.5f, 0.5f));

    }

    //Método que utiliza el botón de avanzar, que va avanzando en 1 cada vez que el botón sea pulsado, logrando así que se muestren todos los personajes de la base de datos
    IEnumerator alanteCo()
    {
        if (numPag + 1 < listapepes.Count)
        {
            yield return 0;
            WWW imagen = new WWW(listapepes[numPag + 1].foto);
            yield return imagen;
            foto = imagen.texture;
            nombre.text = listapepes[numPag + 1].nombre;
            descripcion.text = listapepes[numPag + 1].descripcion;
            fotografia.sprite = Sprite.Create(foto, new Rect(0, 0, 600, 600), new Vector2(0.5f, 0.5f));
            numPag++;
            Debug.Log(numPag + 1);
        }
        
    }

    //Método que utiliza el botón de regresar, que va retrocediendo en 1 cada vez que el botón sea pulsado, logrando así poder ir hacia atras en la lista
    IEnumerator atrasCo()
    {
        if (numPag - 1 >= 0)
        {
            yield return 0;
            WWW imagen = new WWW(listapepes[numPag - 1].foto);
            yield return imagen;
            foto = imagen.texture;
            nombre.text = listapepes[numPag - 1].nombre;
            descripcion.text = listapepes[numPag - 1].descripcion;
            fotografia.sprite = Sprite.Create(foto, new Rect(0, 0, 600, 600), new Vector2(0.5f, 0.5f));
            numPag--;
            Debug.Log(numPag+1);
        }

    }

    //Se le asigna al botón de avanzar
    public void alante()
    {
        StartCoroutine(alanteCo());
        
    }
    //Se le asigna al botón de retroceder
    public void atras()
    {
        StartCoroutine(atrasCo());
    }

    /*Realiza la consulta MySQL, conectandome a la base de datos con la variable con las credenciales
     Por cada elemento de la base de datos se crea un objeto de tipo Pepe y se añade a la lista*/
    private List<Pepe> SacarDatos()
    {
        string obtenerDatos = "SELECT * FROM pepe";
        MySqlDataReader reader = null;
        MySqlConnection conexionbd = ConectarBase();
        conexionbd.Open();
        List<Pepe> listapepes = new List<Pepe>();
        MySqlCommand sentencia = new MySqlCommand(obtenerDatos, conexionbd);
        reader = sentencia.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read()) 
            {
                Pepe i = new Pepe();
                i.nombre = reader.GetString(0);
                i.descripcion = reader.GetString(1);
                i.foto = reader.GetString(2);
                listapepes.Add(i);
            }
        }
        return listapepes;
    }

    //Conecta con la base de datos utilizando la variable con las credenciales
    private MySqlConnection ConectarBase()
    {
        conexion = new MySqlConnection(datosConexion);

        try
        {
            return conexion;
            Debug.Log("Conexion realizada");
        }
        catch (MySqlException e)
        {
            Debug.Log("Error de conexion" + e);
            return null;
        }
    }
}
