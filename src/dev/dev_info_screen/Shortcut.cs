using UnityEngine;

namespace rose.row.dev.dev_info_screen
{
    public class Shortcut
    {
        public KeyCode[] modifiers;
        public KeyCode key;

        public Shortcut(KeyCode[] modifiers, KeyCode key)
        {
            this.modifiers = modifiers;
            this.key = key;
        }

        public bool down()
        {
            foreach (var modifier in modifiers)
                if (!Input.GetKey(modifier))
                    return false;

            return Input.GetKeyDown(key);
        }
    }
}