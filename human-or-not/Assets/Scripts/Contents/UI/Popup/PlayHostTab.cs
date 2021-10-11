using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayHostTab : PopupUI
{
    enum Buttons
    {
        CreateRoom,
        Clear,
        PrevA_Z,
        CurrA_Z,
        NextA_Z,
        Prev0_9,
        Curr0_9,
        Next0_9,
    }

    enum InputFields
    {
        InputName,
    }

    enum Texts
    {
        TextA_Z,
        Text0_9,
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        Bind<Button>(typeof(Buttons));
        Bind<InputField>(typeof(InputFields));
        Bind<Text>(typeof(Texts));
    }

    void Start()
    {
        InitSelection();
    }

    void InitSelection()
    {
        // Input name
        InputField inputName = GetInputField((int)InputFields.InputName);

        Button clearBtn = GetButton((int)Buttons.Clear);

        BindEvent(clearBtn.gameObject, (PointerEventData) =>
        {
            // Clear
            inputName.text = "";
        });

        // Input A_Z
        Button prevBtnA_Z = GetButton((int)Buttons.PrevA_Z);
        Button currBtnA_Z = GetButton((int)Buttons.CurrA_Z);
        Button nextBtnA_Z = GetButton((int)Buttons.NextA_Z);
        Text textA_Z = GetText((int)Texts.TextA_Z);

        string[] alphabets = new string[26];
        for (int i = 0; i < 26; i++)
            alphabets[i] = ((char)('A' + i)).ToString();

        int idxA_Z = 0;
        textA_Z.text = alphabets[idxA_Z];

        BindEvent(prevBtnA_Z.gameObject, (PointerEventData) =>
        {
            idxA_Z = idxA_Z > 0 ? idxA_Z - 1 : 25;
            textA_Z.text = alphabets[idxA_Z];
        });

        BindEvent(currBtnA_Z.gameObject, (PointerEventData) => { inputName.text += textA_Z.text; });

        BindEvent(nextBtnA_Z.gameObject, (PointerEventData) =>
        {
            idxA_Z = idxA_Z < 25 ? idxA_Z + 1 : 0;
            textA_Z.text = alphabets[idxA_Z];
        });

        // Input 0_9
        Button prevBtn0_9 = GetButton((int)Buttons.Prev0_9);
        Button currBtn0_9 = GetButton((int)Buttons.Curr0_9);
        Button nextBtn0_9 = GetButton((int)Buttons.Next0_9);
        Text text0_9 = GetText((int)Texts.Text0_9);

        string[] numbers = new string[10];
        for (int i = 0; i <= 9; i++)
            numbers[i] = i.ToString();

        int idx0_9 = 0;
        text0_9.text = numbers[idx0_9];

        BindEvent(prevBtn0_9.gameObject, (PointerEventData) =>
        {
            idx0_9 = idx0_9 > 0 ? idx0_9 - 1 : 9;
            text0_9.text = numbers[idx0_9];
        });

        BindEvent(currBtn0_9.gameObject, (PointerEventData) => { inputName.text += text0_9.text; });

        BindEvent(nextBtn0_9.gameObject, (PointerEventData) =>
        {
            idx0_9 = idx0_9 < 9 ? idx0_9 + 1 : 0;
            text0_9.text = numbers[idx0_9];
        });

        Button createRoom = GetButton((int)Buttons.CreateRoom);

        BindEvent(createRoom.gameObject, (PointerEventData) =>
        {
            if (string.IsNullOrEmpty(inputName.text))
            {
                Manager.UI.ShowPopupUI<InvalidMessage>();
            }
            else
            {
                // 연결중 팝업 띄우기
                Manager.UI.ShowPopupUI<LoadingMessage>();

                Manager.Network.Open(() =>
                {
                    // Send packet callback
                    Manager.Network.Send<C_CreateRoom>(new C_CreateRoom() { userName = inputName.text });
                });
            }

        });
    }
}