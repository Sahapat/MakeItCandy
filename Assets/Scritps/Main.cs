using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    private AudioClip theme = null;
    private AudioSource audioSource = null;

    public GameObject ObjectToMove;
    public Vector3 objCenter;
    public Vector3 clickPosition;
    public Vector3 offset;
    public Vector3 newObjCenter;

    private RaycastHit hit;
    public bool isDrag = false;

    private CandyMachine candyMachine;
    private PopPopMachine popMachine;
    private TopieMachine topieMachine;

    [SerializeField]
    private GameObject chocolateMachine;
    [SerializeField]
    private GameObject vanilaMachine;
    [SerializeField]
    private GameObject orangeMachine;

    private GamePause pause;
    private GameController gameController;
    private Trash trash = null;
    private void Awake()
    {
        candyMachine = FindObjectOfType<CandyMachine>();
        popMachine = FindObjectOfType<PopPopMachine>();
        topieMachine = FindObjectOfType<TopieMachine>();
        audioSource = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();
        pause = FindObjectOfType<GamePause>();
        trash = FindObjectOfType<Trash>();
    }
    private void Start()
    {
        audioSource.clip = theme;
        audioSource.loop = true;
        audioSource.Play();
    }
    private void Update()
    {
        if(gameController.isGameStart)
        {
            if (!pause.isPause)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("GameUnits"))
                        {
                            ObjectToMove = hit.collider.gameObject;
                            objCenter = ObjectToMove.transform.position;
                            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            offset = clickPosition - objCenter;
                            isDrag = true;
                        }
                    }
                }
                if (Input.GetMouseButton(0))
                {
                    if (isDrag)
                    {
                        clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        newObjCenter = clickPosition - offset;
                        ObjectToMove.transform.position = new Vector3(newObjCenter.x, newObjCenter.y, objCenter.z);
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    isDrag = false;
                    if (ObjectToMove != null)
                    {
                        SweetUnits unit = ObjectToMove.GetComponent<SweetUnits>();
                        if (unit.canPlace)
                        {
                            switch (unit.onMachine)
                            {
                                case OnMachine.Candy:
                                    unit.PlaceObject();
                                    candyMachine.Add();
                                    break;
                                case OnMachine.Topie:
                                    unit.PlaceObject();
                                    topieMachine.Add();
                                    break;
                                case OnMachine.PopPop:
                                    unit.PlaceObject();
                                    popMachine.Add();
                                    break;
                                case OnMachine.IceCreamChocolate:
                                    IceCreamMachine chochoc = chocolateMachine.GetComponent<IceCreamMachine>();
                                    chochoc.coneInput = ObjectToMove;
                                    chochoc.Add();
                                    chochoc.AddConeFlavor(ObjectToMove.GetComponent<Cone>().flavor);
                                    ObjectToMove.GetComponent<Cone>().PlaceOnMachine(chochoc.OnMachinePoint);
                                    break;
                                case OnMachine.IceCreamVanila:
                                    IceCreamMachine Vanivani = vanilaMachine.GetComponent<IceCreamMachine>();
                                    Vanivani.coneInput = ObjectToMove;
                                    Vanivani.Add();
                                    Vanivani.AddConeFlavor(ObjectToMove.GetComponent<Cone>().flavor);
                                    ObjectToMove.GetComponent<Cone>().PlaceOnMachine(Vanivani.OnMachinePoint);
                                    break;
                                case OnMachine.IceCreamOrange:
                                    IceCreamMachine Oror = orangeMachine.GetComponent<IceCreamMachine>();
                                    Oror.coneInput = ObjectToMove;
                                    Oror.Add();
                                    Oror.AddConeFlavor(ObjectToMove.GetComponent<Cone>().flavor);
                                    ObjectToMove.GetComponent<Cone>().PlaceOnMachine(Oror.OnMachinePoint);
                                    break;
                                case OnMachine.Trash:
                                    unit.PlaceObject();
                                    trash.MinusMoney(ObjectToMove.GetComponent<SweetUnits>().gameUnit);
                                    break;
                                case OnMachine.RequireBox:
                                    if (unit.InRequireBox != null)
                                    {
                                        RequireBox requireBox = unit.InRequireBox.GetComponent<RequireBox>();
                                        unit.PlaceObject();
                                        requireBox.AddProductToRequireBox();
                                    }
                                    break;
                                case OnMachine.SpecialRequireBox:
                                    if (unit.InRequireBox != null)
                                    {
                                        SpecialRequireBox requireBox = unit.InRequireBox.GetComponent<SpecialRequireBox>();
                                        unit.PlaceObject();
                                        requireBox.AddProductToRequireBox();
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            unit.ToLastPostion();
                        }
                        ObjectToMove = null;
                    }
                }
            }
        }
    }
    IEnumerator StopTheme()
    {
        for(int i = 0;i<60;i++)
        {
            audioSource.volume -= 0.116f;
            yield return new WaitForSeconds(0.01f);
        }
        audioSource.volume = 0;
    }
    public void EndGame()
    {
        StartCoroutine(StopTheme());
    }
}
