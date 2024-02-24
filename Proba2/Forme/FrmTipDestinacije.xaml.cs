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
    /// Interaction logic for FrmTipDestinacije.xaml
    /// </summary>
    public partial class FrmTipDestinacije : Window
    {

        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView red;
        public FrmTipDestinacije()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UnosTip.Focus();

            try
            {
                konekcija.Open();
                string vratiLokaciju = @"select idLokacija, naziv from Lokacija";
                SqlDataAdapter daLokacija = new SqlDataAdapter(vratiLokaciju, konekcija); // koju komandu i nad kojom bazom(koja je definisana u parametrima u konekcije)
                DataTable dtLokacija = new DataTable();                                 // datatable privremena varijabla ()
                daLokacija.Fill(dtLokacija);                                   // punimo tabelu podaci im adaptera 
                dpLokacija.ItemsSource = dtLokacija.DefaultView;            //za podatke koji ce se priakzati u drop down lisi stavlajmo podatke koji se nalazi u data tablu 
                daLokacija.Dispose();
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
        public FrmTipDestinacije(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UnosTip.Focus();
            this.azuriraj = azuriraj;
            this.red = red;

            try
            {
                konekcija.Open();
                string vratiLokaciju = @"select idLokacija, naziv from Lokacija";
                SqlDataAdapter daLokacija = new SqlDataAdapter(vratiLokaciju, konekcija); // koju komandu i nad kojom bazom(koja je definisana u parametrima u konekcije)
                DataTable dtLokacija = new DataTable();                                 // datatable privremena varijabla ()
                daLokacija.Fill(dtLokacija);                                   // punimo tabelu podaci im adaptera 
                dpLokacija.ItemsSource = dtLokacija.DefaultView;            //za podatke koji ce se priakzati u drop down lisi stavlajmo podatke koji se nalazi u data tablu 
                daLokacija.Dispose();
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
                cmd.Parameters.Add("@idTipDestinacije", SqlDbType.NChar).Value = UnosidTipDestinacije.Text;

                cmd.Parameters.Add("@tip", SqlDbType.NChar).Value = UnosTip.Text; //Definisime parametre svaki paramatar odgovara jedom atributu iz baze podataka

                cmd.Parameters.Add("@idLokacija", SqlDbType.Int).Value = dpLokacija.SelectedValue; // sleceted value 
                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE Tip_Destinacije SET tip=@tip,idLokacija=@idLokacija WHERE idTipDestinacije=@id";

                    red = null;

                }
                else {
                    cmd.CommandText = @"Insert into Tip_Destinacije (idTipDestinacije,tip,idLokacija) values(@idTipDestinacije,@tip,@idLokacija)";       // Unsimo parametre u bazu podataka    U koju kolonu hocemo i sta hocemo 

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
                if (konekcija != null)
                {
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
