using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UserUIElements;

namespace UserBook
{
    class Contents
    {
        public void Open(Book book, UIElements uI)
        {
            uI.Clear();

            uI.AddButton("Exit", 0, 0, () => { book.exit.Open(book, uI); });
            uI.AddButton("Items", 0, -4, () => { book.items.Open(book, uI); });
            uI.AddButton("Quest", 0, -8, () => { book.exit.Open(book, uI); });
            uI.AddButton("Options", 0, -12, () => { book.quest.Open(book, uI); });
            uI.AddButton("Controls", 0, -16, () => { book.controls.Open(book, uI); });
        }
    }
}