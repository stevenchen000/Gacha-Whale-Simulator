using Godot;
using System;

namespace SaveSystem
{
    public partial class FileSaver : Node
    {
        private FileSaver instance;
        private string baseSaveLocation = "user://";


        public override void _Ready()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                QueueFree();
            }
        }

        public static void SaveFile(string path, string data)
        {

        }
    }
}