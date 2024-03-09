using Godot;
using System;
using System.Collections.Generic;

namespace DialogueSystem
{
    public partial class DialoguesBase : Node
    {
        public Dictionary<int, string> Dialogue = new Dictionary<int, string>();
        [Export] public Godot.Collections.Array<string> texts;
        public void DialogueAdder(Godot.Collections.Array<string> text)
        {
            int i = 0;
            foreach (var item in text)
            {
                Dialogue.Add(i, item);
                i++;
            }
        }
        public int GetLenght()
        {
            return texts.Count;
        }
    }
}
