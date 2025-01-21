namespace People
{
    public partial class App : Application
    {
        public static PersonRepository PersonRepo { get; private set; }

        public App(PersonRepository repo)
        {
            DependencyService.Register<IMessageService, MessageService>();
            InitializeComponent();
            MainPage = new AppShell();
            PersonRepo = repo;  
        }
    }
}
