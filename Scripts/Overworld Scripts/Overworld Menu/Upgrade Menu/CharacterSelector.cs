using CombatSystem;
using Godot;
using System;
using System.Collections.Generic;

public partial class CharacterSelector : Node
{
    private List<CharacterData> allCharacters;

    [Export] private PackedScene buttonScene;
    [Export] private int buttonsPerPage = 20;
    private List<CharacterSelectionButton> buttons;
    public int PageNumber { get; private set; } = 1;
    public int NumberOfPages { get; private set; }

    public List<CharacterData> CharacterList { get; private set; }
    public List<CharacterFilter> CharacterFilters { get; private set; }
    public CharacterSorter CharacterSorter { get; private set; }

    public override void _Ready()
    {
        allCharacters = new List<CharacterData>();
        //add all collected characters
        var collectedChars = GameState.GetAllOwnedCharacters();
        SetCharacterList(collectedChars);
    }

    public void SubscribeEvent(CharacterSelectionButton.OnButtonClick func)
    {
        foreach(var button in buttons)
        {
            button.SubscribeEvent(func);
        }
    }

    /****************
     * Grid Updater
     ***************/

    #region Grid Updater

    public void SetCharacterList(IEnumerable<CharacterData> newData)
    {
        allCharacters = new List<CharacterData>(newData);
        FilterAndSort();

        FindAllButtonChildren();
        AddEnoughButtons();
        UpdateGrid();
    }

    private void FindAllButtonChildren()
    {
        var children = GetChildren();
        buttons = new List<CharacterSelectionButton>();

        foreach (var child in children)
        {
            if(child is CharacterSelectionButton)
            {
                buttons.Add((CharacterSelectionButton)child);
            }
        }
    }

    private void AddEnoughButtons()
    {
        for(int i = buttons.Count; i < buttonsPerPage; i++)
        {
            var button = Utils.InstantiateCopy<CharacterSelectionButton>(buttonScene);
            buttons.Add(button);
            AddChild(button);
        }
    }

    private void UpdateGrid()
    {
        int startIndex = (PageNumber - 1) * buttonsPerPage;
        
        for (int i = 0; i < buttonsPerPage; i++)
        {
            int currIndex = startIndex + i;
            var button = buttons[i];

            if (currIndex < CharacterList.Count)
            {
                var character = CharacterList[currIndex];
                button.SetupData(character);
                button.Visible = true;
            }
            else
            {
                button.Visible = false;
            }

        }
    }

    #endregion

    /**************
     * Page Numbers
     * ************/

    #region Page Numbers

    public void IncrementPage()
    {
        if(HasPagesToRight())
        {
            PageNumber++;
            UpdateGrid();
        }
    }

    public void DecrementPage()
    {
        if (HasPagesToLeft())
        {
            PageNumber--;
            UpdateGrid();
        }
    }

    public bool HasPagesToRight()
    {
        return PageNumber < NumberOfPages;
    }

    public bool HasPagesToLeft()
    {
        return PageNumber > 1;
    }

    private void CalculateNumberOfPages()
    {
        int numOfChars = CharacterList.Count;
        NumberOfPages = numOfChars / buttonsPerPage;

        if (numOfChars % buttonsPerPage > 0)
            NumberOfPages++;
    }

    #endregion

    /****************
     * Filters
     ****************/

    #region Filter

    public void SetFilterList(List<CharacterFilter> filters)
    {
        CharacterFilters = filters;
        FilterAndSort();
        UpdateGrid();
    }

    private void ApplyFilters(List<CharacterData> characters, List<CharacterFilter> filters)
    {
        int index = 0;
        while (index < CharacterList.Count)
        {
            var character = characters[index];
            bool keep = true;

            foreach (var filter in filters)
            {
                if (!filter.Filter(character))
                {
                    keep = false;
                    break;
                }
            }

            if (!keep)
            {
                characters.RemoveAt(index);
            }
            else
            {
                index++;
            }
        }
    }

    #endregion

    /*************
     * Sort
     ************/

    #region Sort

    public void SetListSorter(CharacterSorter sorter)
    {
        CharacterSorter = sorter;
        FilterAndSort();
        UpdateGrid();
    }
    #endregion

    /*****************
     * Filter And Sort
     ****************/

    private void FilterAndSort()
    {
        CharacterList = new List<CharacterData>();
        CharacterList.AddRange(allCharacters);

        if (CharacterFilters != null)
            ApplyFilters(CharacterList, CharacterFilters);
        if (CharacterSorter != null)
            CharacterSorter.Sort(CharacterList);

        CalculateNumberOfPages();
    }

}
