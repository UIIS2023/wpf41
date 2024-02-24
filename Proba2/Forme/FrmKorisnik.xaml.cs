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
    /// Interaction logic for FrmKorisnik.xaml
    /// </summary>
    public partial class FrmKorisnik : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public FrmKorisnik()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            unosIme.Focus();
        }
        public FrmKorisnik(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            unosIme.Focus();
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
                cmd.Parameters.Add("@godine", SqlDbType.NChar).Value = unosGodine.Text;
                cmd.Parameters.Add("@JMBG", SqlDbType.NChar).Value = unosJmbg.Text;
                cmd.Parameters.Add("@adresa", SqlDbType.NChar).Value = unosAdresa.Text;
                cmd.Parameters.Add("@grad", SqlDbType.NChar).Value = unosGrad.Text;
                cmd.Parameters.Add("@kontakt", SqlDbType.NChar).Value = unosKontakt.Text;
                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE Korisnik SET ime=@ime, prezime=@prezime, godine=@godine, JMBG=@JMBG, adresa=@adresa, grad=@grad, kontakt=@kontakt WHERE idKorisnik=@id";

                    red = null;

                }
                else {
                    cmd.CommandText = @"INSERT INTO Korisnik (ime, prezime, godine, JMBG, adresa, grad, kontakt) VALUES (@ime, @prezime, @godine, @JMBG, @adresa, @grad, @kontakt)";
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
