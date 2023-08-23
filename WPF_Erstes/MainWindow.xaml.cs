using System.Windows;
using System.Windows.Controls;

namespace WPF_Calculator
{
    public partial class MainWindow : Window
    {
        private double? _ersterWert = null;
        private string _ausgewaehlteOperation = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Verarbeitet die Klicks auf die Zahlentasten
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            var geklickteZahl = (sender as Button).Content.ToString();
            AnInputAnhaengen(geklickteZahl);
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            // Wenn wir bereits eine erste Nummer haben, berechnen wir das Ergebnis zuerst
            if (_ersterWert.HasValue)
            {
                ErgebnisBerechnen();
            }

            _ausgewaehlteOperation = (sender as Button).Content.ToString();
            _ersterWert = EingabewertAbrufen();
            EingabeLeeren();
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            ErgebnisBerechnen();
            _ausgewaehlteOperation = string.Empty; // Operation leeren
        }

        // Den angegebenen Text an die aktuelle Eingabe anhängen
        private void AnInputAnhaengen(string text)
        {
            inputTextBox.Text += text;
        }

        // Eingabe-TextBox leeren
        private void EingabeLeeren()
        {
            inputTextBox.Text = string.Empty;
        }

        private double EingabewertAbrufen()
        {
            return double.Parse(inputTextBox.Text);
        }

        private void ErgebnisBerechnen()
        {
            if (!_ersterWert.HasValue)
                return;

            double ergebnis = 0;
            double zweiterWert = EingabewertAbrufen();

            switch (_ausgewaehlteOperation)
            {
                case "+":
                    ergebnis = _ersterWert.Value + zweiterWert;
                    break;

                case "-":
                    ergebnis = _ersterWert.Value - zweiterWert;
                    break;

                case "*":
                    ergebnis = _ersterWert.Value * zweiterWert;
                    break;

                case "/":
                    // Behandlung von Division durch Null
                    if (zweiterWert == 0)
                    {
                        MessageBox.Show("Teilung durch Null ist nicht erlaubt.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    ergebnis = _ersterWert.Value / zweiterWert;
                    break;
            }

            inputTextBox.Text = ergebnis.ToString();
            _ersterWert = ergebnis; // Speichert das Ergebnis für nachfolgende Operationen
        }
    }
}