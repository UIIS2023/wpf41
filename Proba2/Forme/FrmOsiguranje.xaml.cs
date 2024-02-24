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
    /// Interaction logic for FrmOsiguranje.xaml
    /// </summary>
    public partial class FrmOsiguranje : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView red;
        public FrmOsiguranje()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UnosTip.Focus();

            try
            {
                konekcija.Open();
                string vratiDestinaciju = @"select idDestinacije, naziv from Destinacija";
                SqlDataAdapter daDestinacija = new SqlDataAdapter(vratiDestinaciju, konekcija); // koju komandu i nad kojom bazom(koja je definisana u parametrima u konekcije)
                DataTable dtDestinacija = new DataTable();
                daDestinacija.Fill(dtDestinacija);
                dpDestinacija.ItemsSource = dtDestinacija.DefaultView;
                daDestinacija.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popnjene", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }

            }

        }
        public FrmOsiguranje(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UnosTip.Focus();
            this.azuriraj = azuriraj;
            this.red = red;

            try
            {
                konekcija.Open();
                string vratiDestinaciju = @"select idDestinacije, naziv from Destinacija";
                SqlDataAdapter daDestinacija = new SqlDataAdapter(vratiDestinaciju, konekcija); // koju komandu i nad kojom bazom(koja je definisana u parametrima u konekcije)
                DataTable dtDestinacija = new DataTable();
                daDestinacija.Fill(dtDestinacija);
                dpDestinacija.ItemsSource = dtDestinacija.DefaultView;
                daDestinacija.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popnjene", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }

            }

        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();

                SqlCommand cmd = new SqlCommand  // Kreiramo objekat klase sqlcomand
                {
                    Connection = konekcija

                };
                cmd.Parameters.Add("@tip", SqlDbType.NChar).Value = UnosTip.Text;

                cmd.Parameters.Add("@idDestinacije", SqlDbType.Int).Value = dpDestinacija.SelectedValue;

                if (azuriraj) {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE Osiguranje SET tip=@tip,idDestinacije=@idDestinacije WHERE idOsiguranje=@id";

                    red = null;

                }
                else
                {
                    cmd.CommandText = @"Insert into Osiguranje (tip, idDestinacije) values(@tip, @idDestinacije)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose(); //Oslobodimo 

                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos nekih parametra nije validan", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if(konekcija != null){
                    konekcija.Close();
                }

            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }
    }
}
