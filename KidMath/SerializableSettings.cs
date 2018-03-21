using System;

namespace KidMath
{
    [Serializable]
    struct SerializableSettings
    {
        public int time;
        public int questions;
        public int maxOperand;
        public string playerName;
        public bool add;
        public bool subtract;
        public bool multiply;
        public bool divide;
    }
}
