﻿using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;

namespace Credentia.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

        [ObservableProperty]
        private bool _isPaneOpen = true;

        [ObservableProperty]
        private ViewModelBase _currentPage = new HomePageViewModel();

        [ObservableProperty]
        private ListItemTemplate? _selectedListItem;
        
        public AvaloniaList<MainAppSearchItem> SearchTerms { get; } = new AvaloniaList<MainAppSearchItem>();
        
        partial void OnSelectedListItemChanged(ListItemTemplate? value)
        {
            if (value is null) return;
            var instance = Activator.CreateInstance(value.ModelType);
            if (instance is null) return;
            CurrentPage = (ViewModelBase)instance;
        }
        
        public ObservableCollection<ListItemTemplate> Items { get; } = new()
        {
            new ListItemTemplate(typeof(HomePageViewModel), "HomeRegular"),
            new ListItemTemplate(typeof(CardPageViewModel), "ContactCardRegular"),
            new ListItemTemplate(typeof(IdentityPageViewModel), "PeopleRegular"),
            new ListItemTemplate(typeof(NotePageViewModel), "NotebookRegular"),
            new ListItemTemplate(typeof(GeneratorPageViewModel), "PasswordRegular"),
            new ListItemTemplate(typeof(SettingsPageViewModel), "SettingsRegular"),
            
        };
        
        [RelayCommand]
        private void TriggerPane()
        {
            IsPaneOpen = !IsPaneOpen;
        }
}

public class ListItemTemplate
{
    public ListItemTemplate(Type type, string iconKey)
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", "");

        Application.Current!.TryFindResource(iconKey, out var res);
        ListItemIcon = (StreamGeometry)res!;
    }

    public string Label { get; }
    public Type ModelType { get; }
    public StreamGeometry ListItemIcon { get; }

}

public class MainAppSearchItem
{
    public MainAppSearchItem() { }

    public MainAppSearchItem(string pageHeader, Type pageType)
    {
        Header = pageHeader;
        PageType = pageType;
    }

    public string Header { get; set; }
    
    public string Namespace { get; set; }

    public Type PageType { get; set; }
}




