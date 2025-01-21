using People.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace People
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _list = new(App.PersonRepo.GetAllPeople());
        private Person _selectedPerson;
        private string _currentName;
        public string status => App.PersonRepo.StatusMessage;
        public ICommand AddPersonCommand { get; set; }
        public ICommand GetAllCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public string currentName
        {
            get => _currentName;
            set
            {
                if (_currentName != value)
                {
                    _currentName = value;
                    OnPropertyChanged();
                }
            }
        }
        public Person selectedPerson
        {
            get => _selectedPerson;
            set
            {
                if (_selectedPerson != value)
                {
                    _selectedPerson = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<Person> list
        {
            get => _list;
            set
            {
                if (_list != value)
                {
                    _list = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            GetAllCommand = new Command(GetPeople);
            AddPersonCommand = new Command(SavePerson);
            DeleteCommand = new Command<Person>(DeletePerson);
        }

        public void SavePerson()
        {
            App.PersonRepo.AddNewPerson(currentName);
            currentName = "";
            OnPropertyChanged(nameof(status));
            OnPropertyChanged(nameof(list));
        }

        public void GetPeople()
        {
            _list.Clear();
            foreach (var person in App.PersonRepo.GetAllPeople())
            {
                _list.Add(person);
            }

            OnPropertyChanged(nameof(status));
            OnPropertyChanged(nameof(list));
        }

        public void DeletePerson(Person person)
        {
            if (person != null)
            {
                App.PersonRepo.DeletePerson(person.Id);
                list.Remove(person);
                OnPropertyChanged(nameof(status));
                OnPropertyChanged(nameof(list));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
