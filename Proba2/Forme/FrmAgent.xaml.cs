using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proba2.Forme
{
    /// <summary>
    /// Interaction logic for FrmAgent.xaml
    /// </summary>
    public partial class FrmAgent : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public FrmAgent()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            unosIme.Focus();
        }


        public FrmAgent(bool azuriraj, DataRowView red)   
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            unosIme.Focus(); // Za postavljanje kurstora na prvu linuju
            this.azuriraj = azuriraj;
            this.red = red;
        }


        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@ime", SqlDbType.NChar).Value = unosIme.Text;
                cmd.Parameters.Add("@prezime", SqlDbType.NChar).Value = unosPrezime.Text;
                cmd.Parameters.Add("@JMBG", SqlDbType.NChar).Value = unosJmbg.Text;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE Agent SET ime=@ime, prezime=@prezime, JMBG=@JMBG WHERE idAgent=@id";

                    red = null;
                }
                else {
                    cmd.CommandText = @"INSERT INTO Agent (ime, prezime, JMBG) VALUES (@ime, @prezime, @JMBG)";

                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos odredjenih vrednosti nije validan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
