using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
 * Class Name :  
 *     Button_UI

 * Class Description : 
 *     This class has multiple attributes for monitoring mouse operations which 
 *     is type of Action. They can call corresponding methods. At current stage, 
 *     only implement methods to meet the functional requirements of hover on are 
 *     expanded. More mouse action attributes are set for scalability of the software, 
 *     and this category can be expanded when there is more need to monitor mouse 
 *     actions in the future. 
 */
public class Button_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    //Some attributions of Button_UI
    public Action ClickFunc = null;
    public Action MouseRightClickFunc = null;
    public Action MouseMiddleClickFunc = null;
    public Action MouseDownOnceFunc = null;
    public Action MouseUpFunc = null;
    public Action MouseOverOnceTooltipFunc = null;
    public Action MouseOutOnceTooltipFunc = null;
    public Action MouseOverOnceFunc = null;
    public Action MouseOutOnceFunc = null;
    public Action MouseOverFunc = null;
    public Action MouseOverPerSecFunc = null; //Triggers every sec if mouseOver
    public Action MouseUpdate = null;
    public Action<PointerEventData> OnPointerClickFunc;

    //Three kinds of mouse hover behaviour
    public enum HoverBehaviour
    {
        Custom,
        Change_Color,
        Change_Image,
        Change_SetActive,
    }
    public HoverBehaviour hoverBehaviourType = HoverBehaviour.Custom;
    private Action hoverBehaviourFunc_Enter, hoverBehaviourFunc_Exit;

    public Color hoverBehaviour_Color_Enter, hoverBehaviour_Color_Exit;
    public Image hoverBehaviour_Image;
    public Sprite hoverBehaviour_Sprite_Exit, hoverBehaviour_Sprite_Enter;
    public bool hoverBehaviour_Move = false;
    public Vector2 hoverBehaviour_Move_Amount = Vector2.zero;
    private Vector2 posExit, posEnter;
    public bool triggerMouseOutFuncOnClick = false;
    private bool mouseOver;
    private float mouseOverPerSecFuncTimer;

    private Action internalOnPointerEnterFunc, internalOnPointerExitFunc, internalOnPointerClickFunc;
    
    // sound manager:
#if SOUND_MANAGER
    public Sound_Manager.Sound mouseOverSound, mouseClickSound;
#endif

    // coursor manager:
#if CURSOR_MANAGER
    public CursorManager.CursorType cursorMouseOver, cursorMouseOut;
#endif

    // =========================================methods about the button=============================================================
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (internalOnPointerEnterFunc != null) internalOnPointerEnterFunc();
        if (hoverBehaviour_Move) transform.localPosition = posEnter;
        if (hoverBehaviourFunc_Enter != null) hoverBehaviourFunc_Enter();
        if (MouseOverOnceFunc != null) MouseOverOnceFunc();
        if (MouseOverOnceTooltipFunc != null) MouseOverOnceTooltipFunc();
        mouseOver = true;
        mouseOverPerSecFuncTimer = 0f;
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (internalOnPointerExitFunc != null) internalOnPointerExitFunc();
        if (hoverBehaviour_Move) transform.localPosition = posExit;
        if (hoverBehaviourFunc_Exit != null) hoverBehaviourFunc_Exit();
        if (MouseOutOnceFunc != null) MouseOutOnceFunc();
        if (MouseOutOnceTooltipFunc != null) MouseOutOnceTooltipFunc();
        mouseOver = false;
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (internalOnPointerClickFunc != null) internalOnPointerClickFunc();
        if (OnPointerClickFunc != null) OnPointerClickFunc(eventData);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (triggerMouseOutFuncOnClick)
            {
                OnPointerExit(eventData);
            }
            if (ClickFunc != null) ClickFunc();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
            if (MouseRightClickFunc != null) MouseRightClickFunc();
        if (eventData.button == PointerEventData.InputButton.Middle)
            if (MouseMiddleClickFunc != null) MouseMiddleClickFunc();
    }
    public void Manual_OnPointerExit()
    {
        OnPointerExit(null);
    }
    public bool IsMouseOver()
    {
        return mouseOver;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (MouseDownOnceFunc != null) MouseDownOnceFunc();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (MouseUpFunc != null) MouseUpFunc();
    }

    //================== implement default methods of GameObject=============================================================================================
    void Update()
    {
        if (mouseOver)
        {
            if (MouseOverFunc != null) MouseOverFunc();
            mouseOverPerSecFuncTimer -= Time.unscaledDeltaTime;
            if (mouseOverPerSecFuncTimer <= 0)
            {
                mouseOverPerSecFuncTimer += 1f;
                if (MouseOverPerSecFunc != null) MouseOverPerSecFunc();
            }
        }
        if (MouseUpdate != null) MouseUpdate();

    }
    void Awake()
    {
        posExit = transform.localPosition;
        posEnter = (Vector2)transform.localPosition + hoverBehaviour_Move_Amount;
        /*SetHoverBehaviourType(hoverBehaviourType);*/

        // Sound Manager
#if SOUND_MANAGER
        internalOnPointerEnterFunc += () => { if (mouseOverSound != Sound_Manager.Sound.None) Sound_Manager.PlaySound(mouseOverSound); };
        internalOnPointerClickFunc += () => { if (mouseClickSound != Sound_Manager.Sound.None) Sound_Manager.PlaySound(mouseClickSound); };
#endif

        // Cursor Manager
#if CURSOR_MANAGER
        internalOnPointerEnterFunc += () => { if (cursorMouseOver != CursorManager.CursorType.None) CursorManager.SetCursor(cursorMouseOver); };
        internalOnPointerExitFunc += () => { if (cursorMouseOut != CursorManager.CursorType.None) CursorManager.SetCursor(cursorMouseOut); };
#endif
    }

    /*
    * Class name: 
    *       InterceptActionHandler
    * 
    * CLass description: 
    *       This class is using for Tutorial, not using in the final project.
    *       The function of the class is intercepting/listening a button action temporarily.
    *       Also, it can disable specific button's function.
    * */
    public class InterceptActionHandler
    {

        private Action removeInterceptFunc;

        public InterceptActionHandler(Action removeInterceptFunc)
        {
            this.removeInterceptFunc = removeInterceptFunc;
        }
        public void RemoveIntercept()
        {
            removeInterceptFunc();
        }
    }
    public InterceptActionHandler InterceptActionClick(Func<bool> testPassthroughFunc)
    {
        return InterceptAction("ClickFunc", testPassthroughFunc);
    }
    public InterceptActionHandler InterceptAction(string fieldName, Func<bool> testPassthroughFunc)
    {
        return InterceptAction(GetType().GetField(fieldName), testPassthroughFunc);
    }
    public InterceptActionHandler InterceptAction(System.Reflection.FieldInfo fieldInfo, Func<bool> testPassthroughFunc)
    {
        Action backFunc = fieldInfo.GetValue(this) as Action;
        InterceptActionHandler interceptActionHandler = new InterceptActionHandler(() => fieldInfo.SetValue(this, backFunc));
        fieldInfo.SetValue(this, (Action)delegate ()
        {
            if (testPassthroughFunc())
            {
                // Passthrough
                interceptActionHandler.RemoveIntercept();
                backFunc();
            }
        });

        return interceptActionHandler;
    }




}
