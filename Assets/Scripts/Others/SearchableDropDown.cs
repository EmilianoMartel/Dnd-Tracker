using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class SearchableDropDown : MonoBehaviour
{
    [SerializeField] private Button _blockerButton;
    [SerializeField] private GameObject _buttonsPrefab = null;
    [SerializeField] private int _maxScrollRectSize = 180;
    [SerializeField] private List<string> _avlOptions = new List<string>();


    private Button _ddButton = null;
    private TMP_InputField _inputField = null;
    private ScrollRect _scrollRect = null;
    private Transform _content = null;
    private RectTransform _scrollRectTrans;
    private bool _isContentHidden = true;
    private List<Button> _initializedButtons = new List<Button>();

    public delegate void OnValueChangedDel(string val);
    public OnValueChangedDel OnValueChangedEvt;

    void Start()
    {
        Init();
    }

    /// <summary>
    /// Initilize all the Fields
    /// </summary>
    private void Init()
    {
        _ddButton = this.GetComponentInChildren<Button>();
        _scrollRect = this.GetComponentInChildren<ScrollRect>();
        _inputField = this.GetComponentInChildren<TMP_InputField>();
        _scrollRectTrans = _scrollRect.GetComponent<RectTransform>();
        _content = _scrollRect.content;

        //blocker is a button added and scaled it to screen size so that we can close the dd on clicking outside
        _blockerButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        _blockerButton.gameObject.SetActive(false);
        _blockerButton.transform.SetParent(this.GetComponentInParent<Canvas>().transform);

        _blockerButton.onClick.AddListener(OnBlockerButtClick);
        _ddButton.onClick.AddListener(OnDDButtonClick);
        _scrollRect.onValueChanged.AddListener(OnScrollRectvalueChange);
        _inputField.onValueChanged.AddListener(OnInputvalueChange);
        _inputField.onEndEdit.AddListener(OnEndEditing);

        AddItemToScrollRect(_avlOptions);

    }

    /// <summary>
    /// public method to get the selected value
    /// </summary>
    /// <returns></returns>
    public string GetValue()
    {
        return _inputField.text;
    }

    public void ResetDropDown()
    {
        _inputField.text = string.Empty;

    }

    //call this to Add items to Drop down
    public void AddItemToScrollRect(List<string> options)
    {
        foreach (var option in options)
        {
            var buttObj = Instantiate(_buttonsPrefab, _content);
            buttObj.GetComponentInChildren<TMP_Text>().text = option;
            buttObj.name = option;
            buttObj.SetActive(true);
            var butt = buttObj.GetComponent<Button>();
            butt.onClick.AddListener(delegate { OnItemSelected(buttObj); });
            _initializedButtons.Add(butt);
        }
        ResizeScrollRect();
        _scrollRect.gameObject.SetActive(false);
    }

    /// <summary>
    /// listner To Input Field End Editing
    /// </summary>
    /// <param name="arg"></param>
    private void OnEndEditing(string arg)
    {
        if (string.IsNullOrEmpty(arg))
        {
            Debug.Log("no value entered ");
            return;
        }
        StartCoroutine(CheckIfValidInput(arg));
    }

    /// <summary>
    /// Need to wait as end inputField and On option button  Contradicted and message was poped after selection of button
    /// </summary>
    /// <param name="arg"></param>
    /// <returns></returns>
    IEnumerator CheckIfValidInput(string arg)
    {
        yield return new WaitForSeconds(1);
        if (!_avlOptions.Contains(arg))
        {
            // Message msg = new Message("Invalid Input!", "Please choose from dropdown",
            //                 this.gameObject, Message.ButtonType.OK);
            //
            //             if (MessageBox.instance)
            //                 MessageBox.instance.ShowMessage(msg); 

            _inputField.text = String.Empty;
        }
        //else
        //    Debug.Log("good job " );
        OnValueChangedEvt?.Invoke(_inputField.text);
    }

    /// <summary>
    /// Called ever time on Drop down value is changed to resize it
    /// </summary>
    private void ResizeScrollRect()
    {
        //TODO Dont Remove this until checked on Mobile Deveice
        //var count = content.transform.Cast<Transform>().Count(child => child.gameObject.activeSelf);
        //var length = buttonsPrefab.GetComponent<RectTransform>().sizeDelta.y * count;

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_content.transform);
        var length = _content.GetComponent<RectTransform>().sizeDelta.y;

        _scrollRectTrans.sizeDelta = length > _maxScrollRectSize ? new Vector2(_scrollRectTrans.sizeDelta.x,
            _maxScrollRectSize) : new Vector2(_scrollRectTrans.sizeDelta.x, length + 5);
    }

    /// <summary>
    /// listner to the InputField
    /// </summary>
    /// <param name="arg0"></param>
    private void OnInputvalueChange(string arg0)
    {
        if (!_avlOptions.Contains(arg0))
        {
            FilterDropdown(arg0);
        }
    }

    /// <summary>
    /// remove the elements from the dropdown based on Filters
    /// </summary>
    /// <param name="input"></param>
    public void FilterDropdown(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            foreach (var button in _initializedButtons)
                button.gameObject.SetActive(true);
            ResizeScrollRect();
            _scrollRect.gameObject.SetActive(false);
            return;
        }

        var count = 0;
        foreach (var button in _initializedButtons)
        {
            if (!button.name.ToLower().Contains(input.ToLower()))
            {
                button.gameObject.SetActive(false);
            }
            else
            {
                button.gameObject.SetActive(true);
                count++;
            }
        }

        SetScrollActive(count > 0);
        ResizeScrollRect();
    }

    /// <summary>
    /// Listner to Scroll rect
    /// </summary>
    /// <param name="arg0"></param>
    private void OnScrollRectvalueChange(Vector2 arg0)
    {
        //Debug.Log("scroll ");
    }

    /// <summary>
    /// Listner to option Buttons
    /// </summary>
    /// <param name="obj"></param>
    private void OnItemSelected(GameObject obj)
    {
        _inputField.text = obj.name;
        foreach (var button in _initializedButtons)
            button.gameObject.SetActive(true);
        _isContentHidden = false;
        OnDDButtonClick();
        //OnEndEditing(obj.name);
        StopAllCoroutines();
        StartCoroutine(CheckIfValidInput(obj.name));
    }

    /// <summary>
    /// listner to arrow button on input field
    /// </summary>
    private void OnDDButtonClick()
    {
        if (GetActiveButtons() <= 0)
            return;
        ResizeScrollRect();
        SetScrollActive(_isContentHidden);
    }

    private void OnBlockerButtClick()
    {
        SetScrollActive(false);
    }

    /// <summary>
    /// respondisble to enable and disable scroll rect component 
    /// </summary>
    /// <param name="status"></param>
    private void SetScrollActive(bool status)
    {
        _scrollRect.gameObject.SetActive(status);
        _blockerButton.gameObject.SetActive(status);
        _isContentHidden = !status;
        _ddButton.transform.localScale = status ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1);
    }

    /// <summary>
    /// Return numbers of active buttons in the dropdown
    /// </summary>
    /// <returns></returns>
    private float GetActiveButtons()
    {
        var count = _content.transform.Cast<Transform>().Count(child => child.gameObject.activeSelf);
        var length = _buttonsPrefab.GetComponent<RectTransform>().sizeDelta.y * count;
        return length;
    }

    public void AddOptionAvl(string name)
    {
        _avlOptions.Add(name);
    }
}
