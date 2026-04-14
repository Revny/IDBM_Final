using IDBM_Final.Models;
using Microsoft.EntityFrameworkCore;
using System;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace IDBM_Final.ViewModels
{
    internal class TitleViewModel : INotifyPropertyChanged
    {
        private readonly ImdbContext _context = new ImdbContext();

        private Title _selectedTitle;


        public ObservableCollection<Title> Titles { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Title SelectedTitle
        {
            get => _selectedTitle;
            set
            {
                _selectedTitle = value;
                OnPropertyChanged();
            }
        }


        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public TitleViewModel()
        {
            Titles = new ObservableCollection<Title>();
            LoadTitles();
        }

        private void LoadTitles()
        {
            var data = _context.Titles
             .Include(t => t.EpisodeParentTitles)
        .ThenInclude(e => e.Title)
    .OrderBy(t => t.PrimaryTitle)
                .Take(1000)
                .ToList();

            foreach (var t in data)
                Titles.Add(t);
        }
    }
}