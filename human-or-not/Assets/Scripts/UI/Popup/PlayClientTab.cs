using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayClientTab : PopupUI
{
    enum Buttons
    {
        Access,
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
        InputRoomKey,
    }

    enum Texts
    {
        TextA_Z,
        Text0_9,
    }

    enum Cursor
    {
        InputName,
        InputRoomKey,
        None,
    }

    protected override void OnStart()
    {
        base.OnStart();

        Bind<Button>(typeof(Buttons));
        Bind<InputField>(typeof(InputFields));
        Bind<Text>(typeof(Texts));

        InitSelection();
    }

    void InitSelection()
    {
        // Input Fields
        Cursor cursor = Cursor.None;
        InputField inputName = GetInputField((int)InputFields.InputName);
        InputField inputRoomKey = GetInputField((int)InputFields.InputRoomKey);

        BindEvent(inputName.gameObject, (PointerEventData) => { cursor = Cursor.InputName; });
        BindEvent(inputRoomKey.gameObject, (PointerEventData) => { cursor = Cursor.InputRoomKey; });

        Button clearBtn = GetButton((int)Buttons.Clear);

        BindEvent(clearBtn.gameObject, (PointerEventData) =>
        {
            // Clear
            if (cursor == Cursor.InputName)
                inputName.text = "";
            else if (cursor == Cursor.InputRoomKey)
                inputRoomKey.text = "";
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

        BindEvent(currBtnA_Z.gameObject, (PointerEventData) =>
        {
            if (cursor == Cursor.InputName)
                inputName.text += textA_Z.text;
            else if (cursor == Cursor.InputRoomKey)
                inputRoomKey.text += textA_Z.text;
        });

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

        BindEvent(currBtn0_9.gameObject, (PointerEventData) =>
        {
            if (cursor == Cursor.InputName)
                inputName.text += text0_9.text;
            else if (cursor == Cursor.InputRoomKey)
                inputRoomKey.text += text0_9.text;
        });

        BindEvent(nextBtn0_9.gameObject, (PointerEventData) =>
        {
            idx0_9 = idx0_9 < 9 ? idx0_9 + 1 : 0;
            text0_9.text = numbers[idx0_9];
        });

        Button accessBtn = GetButton((int)Buttons.Access);

        BindEvent(accessBtn.gameObject, (PointerEventData) =>
        {
            // Only upper-case and 5 characters and set name
            if (inputRoomKey.text.Length != 5 || inputRoomKey.text != inputRoomKey.text.ToUpper() || string.IsNullOrEmpty(inputName.text))
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
                    Manager.Network.Send<C_EnterRoom>(new C_EnterRoom() { userName = inputName.text, roomId = inputRoomKey.text });
                });
            }
        });
    }
}