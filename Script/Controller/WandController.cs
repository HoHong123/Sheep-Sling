using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
    [SerializeField]
    private GameObject slingShot;
    private float maxHeight = 1f;
    private float minHeight = 0.5f;
    private float y;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonPressed = false;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonPressed = false;

    //새총 높이조절용
    private Valve.VR.EVRButtonId upButton = Valve.VR.EVRButtonId.k_EButton_DPad_Up;
    public bool upButtonPressed = false;
    private Valve.VR.EVRButtonId downButton = Valve.VR.EVRButtonId.k_EButton_DPad_Down;
    public bool downButtonPressed = false;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    public string This_Tag; // 오른손인지 왼손인지 구분을 위하여 사용

    private InteractableItem interactingItem; // 상호작용 중인 아이템

    private Vector3 Haptic_Check_Position; //Haptic engine check position
    
    // Use this for initialization
    void Start()
    {
        y = slingShot.transform.localPosition.y;
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        This_Tag = this.gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        gripButtonPressed = controller.GetPress(gripButton);
        triggerButtonPressed = controller.GetPress(triggerButton);
        upButtonPressed = controller.GetPress(upButton);
        downButtonPressed = controller.GetPress(downButton);

        if (upButtonPressed == true)
        {
            if (slingShot.transform.localPosition.y >= y + maxHeight)
            {
                slingShot.transform.localPosition = new Vector3(-0.175f, y + maxHeight, 2.959999f);
            }
        }
        else if (downButtonPressed == true)
        {
            if (slingShot.transform.localPosition.y <= y - minHeight)
            {
                slingShot.transform.localPosition = new Vector3(-0.175f, y - minHeight, 2.959999f);
            }
        }
    }
}
