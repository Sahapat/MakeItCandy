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
    public bool isMute = false;

    private CandyMachine candyMachine;
    private PopPopMachine popMachine;
    private TopieMachine topieMachine;
    private ConeMachine coneMachine;
    private SprinkleMachine sprinkleMachine;

    [SerializeField]
    private GameObject chocolateMachine;
    [SerializeField]
    private GameObject vanilaMachine;
    [SerializeField]
    private GameObject orangeMachine;

    private GamePause pause;
    private GameController gameController;
    private Trash trash = null;
    private const float defualtThemeVolume = 0.65f;
    private void Awake()
    {
        candyMachine = FindObjectOfType<CandyMachine>();
        popMachine = FindObjectOfType<PopPopMachine>();
        sprinkleMachine = FindObjectOfType<SprinkleMachine>();
        topieMachine = FindObjectOfType<TopieMachine>();
        coneMachine = FindObjectOfType<ConeMachine>();
        audioSource = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();
        pause = FindObjectOfType<GamePause>();
        trash = FindObjectOfType<Trash>();
    }
    private void Start()
    {
        audioSource.mute = isMute;
        audioSource.clip = theme;
        audioSource.volume = defualtThemeVolume;
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
                                    if(candyMachine.Add())
                                    {
                                        unit.PlaceObject();
                                    }
                                    else
                                    {
                                        unit.ToLastPostion();
                                    }
                                    break;
                                case OnMachine.Topie:
                                    if(topieMachine.Add())
                                    {
                                        unit.PlaceObject();
                                    }
                                    else
                                    {
                                        unit.ToLastPostion();
                                    }
                                    break;
                                case OnMachine.PopPop:
                                    if(popMachine.Add())
                                    {
                                        unit.PlaceObject();
                                    }
                                    else
                                    {
                                        unit.ToLastPostion();
                                    }
                                    break;
                                case OnMachine.IceCreamChocolate:
                                    IceCreamMachine chochoc = chocolateMachine.GetComponent<IceCreamMachine>();
                                    if (chochoc.canPlace)
                                    {
                                        chochoc.coneInput = ObjectToMove;
                                        chochoc.Add();
                                        chochoc.AddConeFlavor(ObjectToMove.GetComponent<Cone>().flavor);
                                        ObjectToMove.GetComponent<Cone>().PlaceOnMachine(chochoc.OnMachinePoint);
                                        chochoc.canPlace = false;
                                        coneMachine.canSuccess = true;
                                        coneMachine.spawnObject = null;
                                    }
                                    else
                                    {
                                        unit.ToLastPostion();
                                    }
                                    break;
                                case OnMachine.IceCreamVanila:
                                    IceCreamMachine Vanivani = vanilaMachine.GetComponent<IceCreamMachine>();
                                    if (Vanivani.canPlace)
                                    {
                                        Vanivani.coneInput = ObjectToMove;
                                        Vanivani.Add();
                                        Vanivani.AddConeFlavor(ObjectToMove.GetComponent<Cone>().flavor);
                                        ObjectToMove.GetComponent<Cone>().PlaceOnMachine(Vanivani.OnMachinePoint);
                                        Vanivani.canPlace = false;
                                        coneMachine.canSuccess = true;
                                        coneMachine.spawnObject = null;
                                    }
                                    else
                                    {
                                        unit.ToLastPostion();
                                    }
                                    break;
                                case OnMachine.IceCreamOrange:
                                    IceCreamMachine Oror = orangeMachine.GetComponent<IceCreamMachine>();
                                    if (Oror.canPlace)
                                    {
                                        Oror.coneInput = ObjectToMove;
                                        Oror.Add();
                                        Oror.AddConeFlavor(ObjectToMove.GetComponent<Cone>().flavor);
                                        ObjectToMove.GetComponent<Cone>().PlaceOnMachine(Oror.OnMachinePoint);
                                        Oror.canPlace = false;
                                        coneMachine.canSuccess = true;
                                        coneMachine.spawnObject = null;
                                    }
                                    else
                                    {
                                        unit.ToLastPostion();
                                    }
                                    break;
                                case OnMachine.Trash:
                                    unit.PlaceObject();
                                    trash.MinusMoney(ObjectToMove.GetComponent<SweetUnits>().gameUnit);
                                    break;
                                case OnMachine.RequireBox:
                                    if (unit.InRequireBox != null)
                                    {
                                        if(unit.GetComponent<CandyFloss>() != null)
                                        {
                                            RequireBox requireBoxtemp = unit.InRequireBox.GetComponent<RequireBox>();
                                            unit.PlaceObject();
                                            requireBoxtemp.FinishRequireBox();
                                        }
                                        RequireBox requireBox = unit.InRequireBox.GetComponent<RequireBox>();
                                        unit.PlaceObject();
                                        requireBox.AddProductToRequireBox();
                                    }
                                    break;
                                case OnMachine.SpecialRequireBox:
                                    if (unit.InRequireBox != null)
                                    {
                                        if (unit.GetComponent<CandyFloss>() != null)
                                        {
                                            SpecialRequireBox requireBoxtemp = unit.InRequireBox.GetComponent<SpecialRequireBox>();
                                            unit.PlaceObject();
                                            requireBoxtemp.FinishRequireBox();
                                        }
                                        SpecialRequireBox requireBox = unit.InRequireBox.GetComponent<SpecialRequireBox>();
                                        unit.PlaceObject();
                                        requireBox.AddProductToRequireBox();
                                    }
                                    break;
                                case OnMachine.Sprinkle:
                                    if(sprinkleMachine.Add())
                                    {
                                        sprinkleMachine.coneInput = ObjectToMove;
                                        ObjectToMove.GetComponent<IceCreamMachine_Making>().PlaceOnMachine(sprinkleMachine.OnMachinePoint);
                                    }
                                    else
                                    {
                                        unit.ToLastPostion();
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
        audioSource.mute = isMute;
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
