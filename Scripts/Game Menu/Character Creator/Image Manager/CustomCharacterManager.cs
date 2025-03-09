using System.IO;
using EventSystem;
using Godot;
using System.Linq;
using System;
using System.Collections.Generic;

public partial class CustomCharacterManager : Node2D
{
    private static CustomCharacterManager _instance;

    private Dictionary<string, WeakReference> _textures;
    public static string imageSaveLocation { get; private set; }
    private Dictionary<string, CustomCharacterPortrait> _portraits;
    public static string portraitSaveLocation { get; private set; }
    private Dictionary<string, CustomGameCharacter> _characters;
    public static string characterSaveLocation { get; private set; }

    [Export] private VoidEvent OnPortraitAdded;
    [Export] private VoidEvent OnCharacterAdded;

    
    /***********************
     * Godot Functions
     * ***************/

    public override void _Ready()
    {
        if(_instance == null)
        {
            _instance = this;
            Init();
            CallDeferred(MethodName.Reparent);
        }
        else
        {
            QueueFree();
        }
    }

    private void Reparent()
    {
        var root = (Node)GetTree().Root;
        GetParent().RemoveChild(this);
        root.AddChild(this);
    }

    private void Init()
    {
        imageSaveLocation = ProjectSettings.GlobalizePath("user://Images");
        portraitSaveLocation = ProjectSettings.GlobalizePath("user://Portraits");
        characterSaveLocation = ProjectSettings.GlobalizePath("user://Characters");

        if (!Directory.Exists(imageSaveLocation))
            DirAccess.MakeDirRecursiveAbsolute(imageSaveLocation);
        _textures = new Dictionary<string, WeakReference>();
        //LoadAllSavedImages();

        if (!Directory.Exists(portraitSaveLocation))
            DirAccess.MakeDirRecursiveAbsolute(portraitSaveLocation);
        _portraits = new Dictionary<string, CustomCharacterPortrait>();
        LoadAllSavedPortraits();

        if (!Directory.Exists(characterSaveLocation))
            DirAccess.MakeDirRecursiveAbsolute(characterSaveLocation);
        _characters = new Dictionary<string, CustomGameCharacter>();
        LoadAllSavedCharacters();
    }

    /*********************
     * Public Getters
    *******************/

    public static Texture2D GetCustomTexture(string filename)
    {
        Texture2D result = null;

        if (_instance._textures.ContainsKey(filename))
        {
            var reference = _instance._textures[filename];
            if (reference.IsAlive)
            {
                result = (Texture2D)reference.Target;
            }
            else
            {
                result = _instance.LoadImage(filename);
            }
        }
        else if(File.Exists(filename))
        {
            result = _instance.LoadImage(filename);
        }

        return result;
    }

    public static CustomCharacterPortrait GetCustomPortrait(string filename)
    {
        CustomCharacterPortrait result = null;

        if (_instance._portraits.ContainsKey(filename))
        {
            result = _instance._portraits[filename];
        }

        return result;
    }


    /****************
     * Images
     * ****************/

    /// <summary>
    /// Filename is just the name of the file without the path
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="filename"></param>
    public static Texture2D SaveImage(Texture2D texture)
    {
        string filename = CreateFilenameFromTime();
        string filepath = $"{imageSaveLocation}/{filename}.png";
        var image = texture.GetImage();
        image.SavePng(filepath);
        var imageTexture = ImageTexture.CreateFromImage(image);
        imageTexture.ResourcePath = filepath;
        _instance._textures[filepath] = new WeakReference(imageTexture);
        return imageTexture;
    }

    public static void DeleteImage(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            _instance._textures.Remove(path);
        }
    }

    public Texture2D LoadImage(string filename)
    {
        var image = Image.LoadFromFile(filename);
        var texture = ImageTexture.CreateFromImage(image);
        _textures[filename] = new WeakReference(texture);
        return texture;
    }

    private void LoadAllSavedImages()
    {
        var files = Directory.GetFiles(imageSaveLocation);
        foreach(string file in files)
        {
            LoadImage(file);
        }
        
    }

    /***************
     * Portraits
     * ************/


    /// <summary>
    /// Takes the base filename without the path or extension
    /// </summary>
    /// <param name="portrait"></param>
    /// <param name="filename"></param>
    public static void SavePortrait(CustomCharacterPortrait portrait)
    {
        string path = portrait.ResourcePath;
        Utils.Print(_instance, path);
        if (path == null || path == "")
        {
            string filename = CreateFilenameFromTime();
            path = $"{portraitSaveLocation}/{filename}.tres";
            portrait.ResourcePath = path;
        }
        Utils.Print(_instance, path);

        ResourceSaver.Save(portrait, path);

        _instance._portraits[path] = portrait;
        _instance.OnPortraitAdded.RaiseEvent(_instance);
    }

    public static void LoadPortrait(string path)
    {
        //GodotHelper.Print(_instance, path);
        var portrait = ResourceLoader.Load<CustomCharacterPortrait>(path);
        _instance._portraits[path] = portrait;
        //_instance.PrintFileContents(path);

        //GodotHelper.Print(_instance, "Loaded portrait!");
    }
    public static void DeletePortrait(CustomCharacterPortrait portrait)
    {
        string path = portrait.ResourcePath;
        string texturePath = portrait.portraitFile;
        if (path == null || path == "") return;
        File.Delete(path);
        _instance._portraits.Remove(path);
        DeleteImage(texturePath);
        _instance.OnPortraitAdded.RaiseEvent(_instance);
    }

    private void LoadAllSavedPortraits()
    {
        var files = Directory.GetFiles(portraitSaveLocation);
        foreach(var file in files)
        {
            LoadPortrait(file);
        }
        //GodotHelper.Print(this, $"{_portraits.Count} files loaded");
    }

    public static List<CustomCharacterPortrait> GetAllPortraits()
    {
        var values = _instance._portraits.Values;
        var result = new List<CustomCharacterPortrait>();
        result.AddRange(values);
        //GodotHelper.Print(_instance, $"{values.Count}, {result.Count}");
        return result;
    }

    public static CharacterPortrait GetRandomPortrait()
    {
        var portraits = _instance._portraits.Values.ToList();
        var count = portraits.Count;

        if (count > 0)
        {
            int rand = GameState.GetRandomNumber(0, count - 1);
            return portraits[rand];
        }
        else
        {
            return null;
        }
    }


    /***************
     * Game Characters
     * **************/

    private void LoadCharacter(string path)
    {
        var character = ResourceLoader.Load<CustomGameCharacter>(path);
        _instance._characters[path] = character;
    }

    private void LoadAllSavedCharacters()
    {
        var files = Directory.GetFiles(characterSaveLocation);
        foreach (var file in files)
        {
            LoadCharacter(file);
        }
    }

    public static void SaveCharacter(CustomGameCharacter character)
    {
        string path = character.ResourcePath;
        if (path == null || path == "")
        {
            string filename = CreateFilenameFromTime();
            path = $"{characterSaveLocation}/{filename}.tres";
        }
        character.ResourcePath = path;
        ResourceSaver.Save(character, path);

        _instance._characters[path] = character;
        _instance.OnCharacterAdded.RaiseEvent(_instance);
    }

    public static void DeleteCharacter(CustomGameCharacter character)
    {
        string path = character.ResourcePath;
        if (path == null || path == "") return;
        File.Delete(path);
        _instance._characters.Remove(path);
    }

    public static List<CustomGameCharacter> GetAllCharacters()
    {
        var values = _instance._characters.Values;
        var result = new List<CustomGameCharacter>();
        result.AddRange(values);
        return result;
    }


    /**************
     * Helpers
     * ************/

    public static string CreateFilenameFromTime()
    {
        var now = System.DateTime.Now;
        var year = now.Year;
        var month = now.Month;
        var day = now.Day;
        var hour = now.Hour;
        var minute = now.Minute;
        var seconds = now.Second;
        var milliseconds = now.Millisecond;

        string result = $"{year}-{month}-{day}_{hour}-{minute}-{seconds}-{milliseconds}";
        
        return result;
    }

    private void PrintAllFilesInLocation(string path)
    {
        var files = Directory.GetFiles(path);
        foreach(string file in files)
        {
            Utils.Print(this, file + "\n");
        }
    }

    private void PrintFileContents(string path)
    {
        string text = File.ReadAllText(path);
        Utils.Print(this, text);
    }
}
