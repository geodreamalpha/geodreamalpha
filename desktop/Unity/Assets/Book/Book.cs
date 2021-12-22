using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UserSettings;
using UserUIElements;

namespace UserBook
{
    class Book : MonoBehaviour
    {
        public UIElements uI;

        public Settings settings;

        public Contents contents = new Contents();
        public Exit exit = new Exit();
        public Items items = new Items();
        public Quest quest = new Quest();
        public Options options = new Options();
        public Controls controls = new Controls();

        // Start is called before the first frame update
        void Start()
        {
            uI = GetComponent<UIElements>();
            contents.Open(this, uI);
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}