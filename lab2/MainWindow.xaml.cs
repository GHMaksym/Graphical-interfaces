using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace lab2
{
    public partial class MainWindow : Window
    {
        // власна команда "Стерти"
        public static readonly RoutedUICommand ClearCommand =
            new RoutedUICommand("Стерти", "ClearCommand", typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();

            // прив’язка вбудованих і власних команд
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, Save_Executed, Save_CanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, Open_Executed, Open_CanExecute));
            CommandBindings.Add(new CommandBinding(ClearCommand, Clear_Executed, Clear_CanExecute));
            // Copy/Paste обробляються автоматично для TextBox
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(textBox.Text);
            e.Handled = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new SaveFileDialog { Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*" };
            if (dlg.ShowDialog() == true)
                File.WriteAllText(dlg.FileName, textBox.Text);
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*" };
            if (dlg.ShowDialog() == true)
                textBox.Text = File.ReadAllText(dlg.FileName);
        }

        private void Clear_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(textBox.Text);
            e.Handled = true;
        }

        private void Clear_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            textBox.Clear();
        }
    }
}
