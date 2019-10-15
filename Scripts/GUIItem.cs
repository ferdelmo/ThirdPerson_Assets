using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GUIItem : MonoBehaviour
{

	public GameManager.DoorColor m_DoorColor;
	private GameManager m_gameManager;
	private GameObject m_doorGameObject;

	private Image m_Image;

	void Awake()
	{
		GameObject m_GameManagerGo = GameObject.FindGameObjectWithTag("GameManager");
		m_gameManager = m_GameManagerGo.GetComponent<GameManager>();

		m_Image = this.GetComponent<Image>();
	}

	// Use this for initialization
	void Start ()
	{
		m_doorGameObject = m_gameManager.GetDoorGameObject(m_DoorColor);
	}

	// Update is called once per frame
	void Update () {
        // TODO 1. En cada tick tenemos que comprobar si la puerta está activa


        // TODO 2. Si es así, tenemos que pintar la imagen m_Image del mismo color que la puerta
       
        // TODO 3. Por último, para que el componente no haga chequeos inútiles 
        // lo desactivamos (sólo si la puerta está activa)

    }
}
